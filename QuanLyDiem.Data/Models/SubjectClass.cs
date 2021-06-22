namespace QuanLyDiem.Data.Models
{
    public class SubjectClass
    {
        public int SubjectClassId { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}