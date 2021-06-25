using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface IStudentRepository
    {
        IEnumerable<Student> StudentList { get; }
        Student GetStudentById(int studentId);
        Student GetStudentByEmail(string email);
        void CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        void ResetPassword(int studentId,string password);
    }
}