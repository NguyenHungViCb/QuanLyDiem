using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class StudentRepository: IStudentRepository
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IEnumerable<Student> StudentList
        {
            get
            {
                return _appDbContext.Students
                    .Include(s => s.Class);
            }
        }

        public Student GetStudentById(int studentId)
        {
            return _appDbContext.Students.Include(s =>s.Class)
                .FirstOrDefault(s => s.StudentId == studentId);
        }

        public Student GetStudentByEmail(string email)
        {
            return _appDbContext.Students.Include(s => s.Class)
                .FirstOrDefault(s => s.Email == email);
        }

        public void CreateStudent(Student student)
        {
            _appDbContext.Students.Add(student);
            _appDbContext.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            Student existing = GetStudentById(student.StudentId);
            if(existing != null)
            {
                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                existing.Gender = student.Gender;
                existing.Email = student.Email;
                existing.Password = student.Password;
                existing.PhoneNumber = student.PhoneNumber;
                existing.Address = student.Address;
                existing.ClassId = student.ClassId;
                _appDbContext.SaveChanges();
            }
        }

        public void DeleteStudent(int studentId)
        {
            Student existing = GetStudentById(studentId);
            if (existing != null)
            {
                _appDbContext.Remove(existing);
                _appDbContext.SaveChanges();
            }
        }

        public void ResetPassword(int studentId,string password)
        {
            Student existing = GetStudentById(studentId);
            if(existing != null)
            {
                existing.Password = password;
                _appDbContext.SaveChanges();
            }
        }
    }
}