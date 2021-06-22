using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDiem.Data.Models
{
    public class SubjectPoint
    {
        public int SubjectPointId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [Required(ErrorMessage = "First Exam must not be empty")]
        public Double FirstExam { get; set; }
        [Required(ErrorMessage = "Second Exam must not be empty")]
        public Double SecondExam { get; set; }
    }
}