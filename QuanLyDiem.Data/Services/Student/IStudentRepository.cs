using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface IStudentRepository
    {
        IEnumerable<Student> StudentList { get; }
        Student GetStudentById(int studentId);
        void CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
    }
}