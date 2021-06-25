using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyDiem.Data;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class StudentManagerController : Controller
    {
        private readonly IStudentRepository _studentRepositories;
        private readonly IClassRepository _classRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public StudentManagerController(IStudentRepository studentRepository, IClassRepository classRepository, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
            SignInManager<IdentityUser> signInManager)
        {
            this._studentRepositories = studentRepository;
            this._classRepository = classRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        // GET
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return RedirectToAction("StudentList");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult StudentList(string searchId)
        {
                IEnumerable<Student> studentList = _studentRepositories.StudentList;
                IEnumerable<Class> classList = _classRepository.ClassList;
            if (string.IsNullOrEmpty(searchId))
            {
                ViewBag.Deleted = TempData["Deleted"] as string;
                return View(new StudentDetails{StudentList = studentList,ClassList = classList});
            }
            IEnumerable<Student> matchStudent =
                studentList.Where(s => s.StudentId.ToString().Contains(searchId))
                    .Select(s => s);
            return View("StudentList", new StudentDetails{StudentList =matchStudent, ClassList = classList});
        }
        
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            Student student = _studentRepositories.GetStudentById(id);
            var user = await _userManager.FindByEmailAsync(student.Email);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Student");
                    _studentRepositories.DeleteStudent(id);
                    TempData["Deleted"] = "true";
                    return RedirectToAction("StudentList");
                }
                else
                {
                    ModelState.AddModelError("","Something went wrong while delete student");
                }
            }
            else
            {
                ModelState.AddModelError("","This user can't be found");
            }
            return RedirectToAction("StudentList");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateStudent()
        {
            ViewBag.Success = TempData["Success"] as string;
            ViewBag.StudentId = TempData["StudentId"] as string;
            IEnumerable<Class> classList = _classRepository.ClassList;
            return View(new StudentDetails{ClassList = classList});
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            IEnumerable<Class> classList = _classRepository.ClassList;
            IEnumerable<Student> studentList = _studentRepositories.StudentList;
            
            foreach (var s in studentList)
            {
                if (s.Email == student.Email)
                {
                    ModelState.AddModelError("Student.Email","An account with this email already exist");
                }

                if (s.PhoneNumber == student.PhoneNumber)
                {
                    ModelState.AddModelError("Student.PhoneNumber","An account with this phone number already exist");
                }
            }
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = $"st-{student.Email.Split("@")[0]}",
                    Email = student.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, student.Password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("Student");
                    if (role == null)
                    {
                        ModelState.AddModelError("","Something went wrong while create user");
                        return RedirectToAction("CreateStudent");
                    }

                    await _userManager.AddToRoleAsync(user, "Student");
                    
                    _studentRepositories.CreateStudent(student);
                    ModelState.Clear();
                    TempData["Success"] = "true";
                    TempData["StudentId"] = student.StudentId.ToString();
                    return RedirectToAction("CreateStudent");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(new StudentDetails{ClassList = classList});
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Student")]
        public IActionResult StudentDetails(int id)
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            Student student = _studentRepositories.GetStudentById(id);
            if (email != null && role == "Student")
            {
                student = _studentRepositories.GetStudentByEmail(email);
            }
            if(student == null)
            {
                return PartialView("_NotFound");
            }
            
            return View(new StudentDetails{Student = student, ClassList = _classRepository.ClassList, Role = role});
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentDetails(Student student)
        {
            Student existing = _studentRepositories.GetStudentById(student.StudentId);
            var user = await _userManager.FindByEmailAsync(existing.Email);
            if (user != null)
            {
                user.Email = student.Email;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (ModelState.IsValid && result.Succeeded)
                {
                    _studentRepositories.UpdateStudent(student);
                    await _signInManager.PasswordSignInAsync(user, student.Password, false, false);
                    return RedirectToAction("StudentDetails", student.StudentId);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            ModelState.AddModelError("","User not found");
            return View(new StudentDetails { Student = student, ClassList = _classRepository.ClassList });
        }
        //[HttpPost]
        //[Authorize(Roles ="Administrator, Student")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(int studentId,string password, string newPassword)
        //{
        //    var role = this.User.FindFirstValue(ClaimTypes.Role);
        //    Student existing = null;
        //    IdentityUser user = null;
        //    if(role == "Student")
        //    {
        //        var email = this.User.FindFirstValue(ClaimTypes.Email);
        //        existing = _studentRepositories.GetStudentByEmail(email);
        //        user = await _userManager.FindByEmailAsync(existing.Email);
        //    }
        //    else
        //    {
        //        existing = _studentRepositories.GetStudentById(studentId);
        //        user = await _userManager.FindByEmailAsync(existing.Email);
        //    }
        //    if(user != null && existing != null)
        //    {
        //        if(await _userManager.CheckPasswordAsync(user, password))
        //        {
        //            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        //            _studentRepositories.ResetPassword(existing.StudentId, newPassword);
        //            return RedirectToAction("StudentDetails",existing.StudentId);
        //        }
        //        ModelState.AddModelError("", "Wrong Password");
        //    }
        //            return RedirectToAction("StudentDetails",existing.StudentId);
        //}
}
}