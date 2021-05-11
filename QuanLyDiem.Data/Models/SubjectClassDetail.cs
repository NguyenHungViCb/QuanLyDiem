namespace QuanLyDiem.Data.Models
{
    public class SubjectClassDetail
    {
        public int SubjectClassDetailId { get; set; }
        public int SubjectClassId { get; set; }
        public SubjectClass SubjectClass { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}