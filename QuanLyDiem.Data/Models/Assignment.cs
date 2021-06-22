namespace QuanLyDiem.Data.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int LecturerId { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public Lecturer Lecturer { get; set; }
        public Subject Subject { get; set; }
        public Class Class { get; set; }
    }
}