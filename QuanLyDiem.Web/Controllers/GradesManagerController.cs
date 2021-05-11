using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class GradesManagerController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectPointRepository _subjectPointPointRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISubjectRepository _subjectRepository;

        public GradesManagerController(ISubjectPointRepository subjectPointRepository, 
            IStudentRepository studentRepository,
            ISemesterRepository semesterRepository,
            ISubjectRepository subjectRepository)
        {
            this._studentRepository = studentRepository;
            this._subjectPointPointRepository = subjectPointRepository;
            this._semesterRepository = semesterRepository;
            this._subjectRepository = subjectRepository;
        }
        // GET
        public IActionResult Index()
        {
            IEnumerable<Student> studentList = _studentRepository.StudentList;
            return View(new GradesManager{StudentList = studentList});
        }

        public IActionResult OverView()
        {
            Student firstStudent = _studentRepository.StudentList.First();
            if (firstStudent != null)
            {
                return RedirectToAction("Overview", new {id = firstStudent.StudentId});
            }
            return PartialView("_NotFound");
        }
        [HttpGet]
        public IActionResult Overview(int id)
        {
            Student student = _studentRepository.GetStudentById(id);
            if (student != null)
            {
                IEnumerable<SubjectPoint> subjectPointList =
                    _subjectPointPointRepository.GetSubjectPointByStudentId(id);
                return View( new GradesManager{Student = student,SubjectPointList = subjectPointList});
            }

            return PartialView("_NotFound");
        }

        [HttpGet]
        public IActionResult Details(int studentId, int subjectPointId)
        {
            Student student = _studentRepository.GetStudentById(studentId);
            SubjectPoint subjectPoint = _subjectPointPointRepository.GetSubjectPointById(subjectPointId);
            foreach (var item in _semesterRepository.SemesterList)
            {
                if (item.SemesterId == subjectPoint.Subject.SemesterId)
                {
                    subjectPoint.Subject.Semester = item;
                }
            }

            ViewBag.Success = TempData["Success"] as string;
            return View(new GradesManager {Student = student, SubjectPoint = subjectPoint});
        }

        [HttpPost]
        public IActionResult Details(SubjectPoint subjectPoint)
        {
            // if (ModelState.IsValid)
            // {
                _subjectPointPointRepository.UpdateSubjectPoints(subjectPoint);
                ModelState.Clear();
                TempData["Success"] = "true";
                return RedirectToAction("Details",
                    new {studentId = subjectPoint.StudentId, SubjectPointId = subjectPoint.SubjectPointId});
            // }
            // Student student = _studentRepository.GetStudentById(subjectPoint.StudentId);
            // Subject subject = _subjectRepository.GetSubjectById(subjectPoint.SubjectId);
            // foreach (var item in _semesterRepository.SemesterList)
            // {
            //     if (item.SemesterId == subject.SemesterId)
            //     {
            //         subjectPoint.Subject = subject;
            //         subjectPoint.Subject.Semester = item;
            //     }
            // }
            // return View(new GradesManager{Student = student, SubjectPoint = subjectPoint});
        }
    }
}