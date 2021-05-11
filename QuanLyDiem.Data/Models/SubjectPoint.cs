using System;

namespace QuanLyDiem.Data.Models
{
    public class SubjectPoint
    {
        public int SubjectPointId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Double FirstExam { get; set; }
        public Double SecondExam { get; set; }
    }
}