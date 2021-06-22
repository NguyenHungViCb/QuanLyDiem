using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class LecturersManagerController : Controller
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LecturersManagerController(ILecturerRepository lecturerRepository, UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this._lecturerRepository = lecturerRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET
        [HttpGet]
    [Authorize(Roles = "Administrator")]
        public IActionResult Index(string searchId)
        {
            ViewBag.Deleted = TempData["Deleted"] as string;
            if (string.IsNullOrEmpty(searchId))
            {
                return View(new LecturersManager{LecturerList = _lecturerRepository.LecturerList});
            }

            IEnumerable<Lecturer> matchLecturer = _lecturerRepository.LecturerList
                .Where(l => l.LecturerId.ToString().Contains(searchId)).Select(l => l);
            return View(new LecturersManager {LecturerList = matchLecturer});
        }

    [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.Success = TempData["Success"] as string;
            ViewBag.StudentId = TempData["LecturerId"] as string;
            return View(new LecturersManager());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            foreach (var s in _lecturerRepository.LecturerList)
            {
                if (s.Email == lecturer.Email)
                {
                    ModelState.AddModelError("Student.Email","An account with this email already exist");
                }
         
                if (s.PhoneNumber == lecturer.PhoneNumber)
                {
                    ModelState.AddModelError("Student.PhoneNumber","An account with this phone number already exist");
                }
            }
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = $"l-{lecturer.Email.Split("@")[0]}",
                    Email = lecturer.Email
                };
                
                IdentityResult result = await _userManager.CreateAsync(user, lecturer.Password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("Lecturer");
                    if (role == null)
                    {
                        ModelState.AddModelError("","Something went wrong while create user");
                        return RedirectToAction("Create");
                    }

                    var roleResult = await _userManager.AddToRoleAsync(user,"Lecturer");
                    
                    _lecturerRepository.CreateLecturer(lecturer);
                    ModelState.Clear();
                    TempData["Success"] = "true";
                    TempData["StudentId"] = lecturer.LecturerId.ToString();
                    return RedirectToAction("Create");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(new LecturersManager{Lecturer = lecturer});
        }

        [Authorize(Roles = "Administrator,Lecturer")]
        public IActionResult Details(int id = 1)
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            Lecturer lecturer = _lecturerRepository.GetLecturerById(id);
            if (email != null && role == "Lecturer")
            {
                lecturer = _lecturerRepository.GetLecturerByEmail(email);
            }
            if (lecturer != null)
                return View(new LecturersManager {Lecturer = lecturer,Role = role});
            return PartialView("_NotFound");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Lecturer")]
        public IActionResult Details(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _lecturerRepository.UpdateLecturer(lecturer);
                return RedirectToAction("Details", new {id = lecturer.LecturerId});
            }

            return View(new LecturersManager {Lecturer = lecturer});
        }

    [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            Lecturer lecturer = _lecturerRepository.GetLecturerById(id);
            var user = await _userManager.FindByEmailAsync(lecturer.Email);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Lecturer");
                    _lecturerRepository.DeleteLecturer(id);
                    TempData["Deleted"] = "true";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong while delete user");
                }
            }
            else
            {
                ModelState.AddModelError("","This user can't be found");
            }

            return RedirectToAction("Index");
        }
    }
}