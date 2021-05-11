using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class LecturersManagerController : Controller
    {
        private readonly ILecturerRepository _lecturerRepository;

        public LecturersManagerController(ILecturerRepository lecturerRepository)
        {
            this._lecturerRepository = lecturerRepository;
        }
        // GET
        public IActionResult Index()
        {
            ViewBag.Deleted = TempData["Deleted"] as string;
            return View(new LecturersManager{LecturerList = _lecturerRepository.LecturerList});
        }

        public IActionResult Create()
        {
            ViewBag.Success = TempData["Success"] as string;
            ViewBag.StudentId = TempData["LecturerId"] as string;
            return View(new LecturersManager());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lecturer lecturer)
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
                _lecturerRepository.CreateLecturer(lecturer);
                ModelState.Clear();
                TempData["Success"] = "true";
                TempData["StudentId"] = lecturer.LecturerId.ToString();
                return RedirectToAction("Create");
            }

            return View(new LecturersManager{Lecturer = lecturer});
        }

        public IActionResult Details(int id)
        {
            Lecturer lecturer = _lecturerRepository.GetLecturerById(id);
            if (lecturer != null)
                return View(new LecturersManager {Lecturer = lecturer});
            return PartialView("_NotFound");
        }

        [HttpPost]
        public IActionResult Details(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _lecturerRepository.UpdateLecturer(lecturer);
                return RedirectToAction("Details", new {id = lecturer.LecturerId});
            }

            return View(new LecturersManager {Lecturer = lecturer});
        }

        public IActionResult Delete(int id)
        {
            _lecturerRepository.DeleteLecturer(id);
            TempData["Deleted"] = "true";
            return RedirectToAction("Index");
        }
    }
}