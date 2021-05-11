using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class SubjectManagerController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISemesterRepository _semesterRepository;

        public SubjectManagerController(ISubjectRepository subjectRepository, ISemesterRepository semesterRepository)
        {
            this._subjectRepository = subjectRepository;
            this._semesterRepository = semesterRepository;
        }
        // GET
        public IActionResult Index()
        {
            ViewBag.Deleted = TempData["Deleted"] as string;
            return View(new SubjectManager{SubjectList = _subjectRepository.SubjectList});
        }

        public IActionResult Details(int subjectId)
        {
            Subject existing = _subjectRepository.GetSubjectById(subjectId);
            if (existing != null)
            {
                return View(new SubjectManager{Subject = existing,
                    SemesterList = _semesterRepository.SemesterList });
            }

            return PartialView("_NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _subjectRepository.UpdateSubject(subject);
                return RedirectToAction("Details", new {subjectId = subject.SubjectId});
            }

            return View(new SubjectManager {Subject = subject, SemesterList = _semesterRepository.SemesterList});
        }

        public IActionResult Create()
        {
            ViewBag.Success = TempData["Success"] as string;
            ViewBag.SubjectId = TempData["SubjectId"] as string;
            return View(new SubjectManager {Subject = new Subject(), 
                SemesterList = _semesterRepository.SemesterList});
        }

        [HttpPost]
        public IActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _subjectRepository.CreateSubject(subject);
                ModelState.Clear();
                TempData["Success"] = "true";
                TempData["SubjectId"] = subject.SubjectId.ToString();
                return RedirectToAction("Create");
            }

            return View(new SubjectManager {Subject = subject, SemesterList = _semesterRepository.SemesterList});
        }

        public IActionResult Delete(int subjectId)
        {
            _subjectRepository.DeleteSubject(subjectId);
            TempData["Deleted"] = "true";
            return RedirectToAction("Index");
        }

    }
}