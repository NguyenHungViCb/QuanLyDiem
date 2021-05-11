using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Web.ViewModels
{
    public class SubjectManager
    {
        public Subject Subject { get; set; }
        public IEnumerable<Subject> SubjectList { get; set; }
        public IEnumerable<Semester> SemesterList { get; set; }
    }
}