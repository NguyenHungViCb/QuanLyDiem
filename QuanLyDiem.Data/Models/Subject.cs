namespace QuanLyDiem.Data.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int CourseLoad { get; set; }
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}