using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Web.ViewModels
{
    public class GradesManager
    {
        public IEnumerable<Student> StudentList { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Assignment> AssignmentList { get; set; }
        public IEnumerable<SubjectPoint> SubjectPointList { get; set; }
        public Subject Subject { get; set; }
        public SubjectPoint SubjectPoint { get; set; }
        public IEnumerable<Semester> SemesterList { get; set; }
        public IEnumerable<Subject> SubjectList { get; set; }
        public Semester Semester { get; set; }
    }
}