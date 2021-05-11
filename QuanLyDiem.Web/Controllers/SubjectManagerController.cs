using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data;
using QuanLyDiem.Data.Services;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class SubjectManagerController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectManagerController(ISubjectRepository subjectRepository)
        {
            this._subjectRepository = subjectRepository;
        }
        // GET
        public IActionResult Index()
        {
            return View(new SubjectManager{SubjectList = _subjectRepository.SubjectList});
        }
    }
}