using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public StudentManagerController(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            this._studentRepositories = studentRepository;
            this._classRepository = classRepository;
        }
        // GET
        public IActionResult Index()
        {
            return RedirectToAction("StudentList");
        }

        public IActionResult StudentList()
        {
            IEnumerable<Student> studentList = _studentRepositories.StudentList;
            IEnumerable<Class> classList = _classRepository.ClassList;
            ViewBag.Deleted = TempData["Deleted"] as string;
            return View(new StudentDetails{StudentList = studentList,ClassList = classList});
        }
        
        public IActionResult DeleteStudent(int id)
        {
            _studentRepositories.DeleteStudent(id);
            TempData["Deleted"] = "true";
            return RedirectToAction("StudentList");
        }

        public IActionResult CreateStudent()
        {
            ViewBag.Success = TempData["Success"] as string;
            ViewBag.StudentId = TempData["StudentId"] as string;
            IEnumerable<Class> classList = _classRepository.ClassList;
            return View(new StudentDetails{ClassList = classList});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(Student student)
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
                _studentRepositories.CreateStudent(student);
                ModelState.Clear();
                TempData["Success"] = "true";
                TempData["StudentId"] = student.StudentId.ToString();
                return RedirectToAction("CreateStudent");
            }
            return View(new StudentDetails{ClassList = classList});
        }

        [HttpGet]
        public IActionResult StudentDetails(int id)
        {
            Student student = _studentRepositories.GetStudentById(id);
            if(student == null)
            {
                return PartialView("_NotFound");
            }
            
            return View(new StudentDetails{Student = student, ClassList = _classRepository.ClassList});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentDetails(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepositories.UpdateStudent(student);
                return RedirectToAction("StudentDetails", new { id = student.StudentId });
            }
            return View(new StudentDetails { Student = student, ClassList = _classRepository.ClassList });
        }



}
}