using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class GradesManagerController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectPointRepository _subjectPointRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly ISubjectRepository _subjectRepository;

        public GradesManagerController(ISubjectPointRepository subjectPointRepository, 
            IStudentRepository studentRepository,
            ISemesterRepository semesterRepository,
            ISubjectRepository subjectRepository,
            IAssignmentRepository assignmentRepository,
            ILecturerRepository lecturerRepository)
        {
            this._studentRepository = studentRepository;
            this._subjectPointRepository = subjectPointRepository;
            this._semesterRepository = semesterRepository;
            this._subjectRepository = subjectRepository;
            this._assignmentRepository = assignmentRepository;
            this._lecturerRepository = lecturerRepository;
        }
        // GET
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(string searchId)
        {
            IEnumerable<SubjectPoint> subjectPoints = _subjectPointRepository.SubjectPointList;
            if (string.IsNullOrEmpty(searchId))
            {
                IEnumerable<Student> studentList = _studentRepository.StudentList;
                return View(new GradesManager{StudentList = studentList, SubjectPointList = subjectPoints});
            }

            IEnumerable<Student> matchStudent = _studentRepository.StudentList
                .Where(s => s.StudentId.ToString().Contains(searchId)).Select(s => s);
            return View(new GradesManager {StudentList = matchStudent, SubjectPointList = subjectPoints});
        }

        [Authorize(Roles = "Administrator,Lecturer")]
        public IActionResult LecturerGMIndex(int semesterId=1)
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            if (email != null && role == "Lecturer")
            {
                Lecturer lecturer = _lecturerRepository.GetLecturerByEmail(email);

                IEnumerable<Assignment> assignmentList =
                    _assignmentRepository.GetAssignmentListByLecturer(lecturer.LecturerId);
                
                return View(new GradesManager{AssignmentList = assignmentList});
            }
            return View(null);
        }

        [Authorize(Roles = "Administrator,Lecturer")]
        public IActionResult LecturerGMList(int subjectId = 1)
        {
            IEnumerable<SubjectPoint> subjectPoints = _subjectPointRepository.SubjectPointListBySubject(subjectId);
            
            return View(new GradesManager{SubjectPointList =  subjectPoints});
        }

        [Authorize(Roles = "Administrator,Student")]
        public IActionResult StudentGMIndex()
        {
            int semesterId = 1;
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            Student student = _studentRepository.GetStudentByEmail(email);
            IEnumerable<SubjectPoint> subjectPoints =
                _subjectPointRepository.SubjectPointListByStudentAndSemester(student.StudentId, semesterId);
            return View(new GradesManager{SubjectPointList = subjectPoints});
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Student")]
        public IActionResult StudentSubjectPointList(int semesterId)
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);
                        Student student = _studentRepository.GetStudentByEmail(email);
                        IEnumerable<SubjectPoint> subjectPoints =
                            _subjectPointRepository.SubjectPointListByStudentAndSemester(student.StudentId, semesterId);
                        return PartialView("_studentSubjectPointList",
                            new GradesManager {SubjectPointList = subjectPoints});
        }
        
        [HttpPost]
        [Authorize(Roles = "Administrator,Lecturer")]
        public IActionResult LecturerGMList(SubjectPoint subjectPoint)
        {
            if (ModelState.IsValid)
            {
                _subjectPointRepository.UpdateSubjectPoints(subjectPoint);
            }
            // IEnumerable<SubjectPoint> subjectPoints = _subjectPointRepository.SubjectPointListBySubject(subjectPoint.SubjectId);

            return RedirectToAction("LecturerGMList", new { subjectId = subjectPoint.SubjectId});
        }
        
        //[Authorize(Roles = "Administrator")]
        //public IActionResult OverView()
        //{
        //    Student firstStudent = _studentRepository.StudentList.First();
        //    if (firstStudent != null)
        //    {
        //        return RedirectToAction("Overview", new {id = firstStudent.StudentId});
        //    }
        //    return PartialView("_NotFound");
        //}

        [Authorize(Roles = "Administrator")]
        public IActionResult Overview(int id)
        {
            Student student = _studentRepository.GetStudentById(id);
            if (student != null)
            {
                IEnumerable<SubjectPoint> subjectPointList =
                    _subjectPointRepository.GetSubjectPointByStudentId(id);
                return View( new GradesManager{Student = student,SubjectPointList = subjectPointList});
            }

            return PartialView("_NotFound");
        }

        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public IActionResult Details(int studentId, int subjectPointId)
        //{
        //    Student student = _studentRepository.GetStudentById(studentId);
        //    SubjectPoint subjectPoint = _subjectPointRepository.GetSubjectPointById(subjectPointId);
        //    foreach (var item in _semesterRepository.SemesterList)
        //    {
        //        if (item.SemesterId == subjectPoint.Subject.SemesterId)
        //        {
        //            subjectPoint.Subject.Semester = item;
        //        }
        //    }

        //    ViewBag.Success = TempData["Success"] as string;
        //    return View(new GradesManager {Student = student, SubjectPoint = subjectPoint});
        //}

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Details(SubjectPoint subjectPoint)
        {
            Student student = _studentRepository.GetStudentById(subjectPoint.StudentId);
            //Subject subject = _subjectRepository.GetSubjectById(subjectPoint.SubjectId);
            //foreach (var item in _semesterRepository.SemesterList)
            //{
            //    if (item.SemesterId == subject.SemesterId)
            //    {
            //        subjectPoint.Subject = subject;
            //        subjectPoint.Subject.Semester = item;
            //    }
            //}

            if (ModelState.IsValid)
            {
                _subjectPointRepository.UpdateSubjectPoints(subjectPoint);
                ModelState.Clear();
                TempData["Success"] = "true";
                //return RedirectToAction("Overview",
                //    new {studentId = subjectPoint.StudentId, subjectPointId = subjectPoint.SubjectPointId});
            }

            //return View(new GradesManager{Student = student, SubjectPoint = subjectPoint});
            return RedirectToAction("Overview",new { id = subjectPoint.StudentId });
        }
    }
}