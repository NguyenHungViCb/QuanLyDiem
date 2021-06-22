using System.ComponentModel.DataAnnotations;

namespace QuanLyDiem.Data.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Name must not be empty")]
        public string SubjectName { get; set; }
        public int CourseLoad { get; set; }
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}