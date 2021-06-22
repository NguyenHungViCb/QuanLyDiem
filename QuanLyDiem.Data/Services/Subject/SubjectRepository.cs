using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _appDbContext;

        public SubjectRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public IEnumerable<Subject> SubjectList
        {
            get
            {
                return _appDbContext.Subjects.Include(s => s.Semester);
            }
        }

        public Subject GetSubjectById(int subjectId)
        {
            return _appDbContext.Subjects.Where(s => s.SubjectId == subjectId)
                .Include(s => s.Semester).FirstOrDefault();
        }

        public void CreateSubject(Subject subject)
        {
            _appDbContext.Subjects.Add(subject);
            _appDbContext.SaveChanges();
        }

        public void UpdateSubject(Subject subject)
        {
            Subject existing = GetSubjectById(subject.SubjectId);
            if (existing != null)
            {
                existing.SubjectName = subject.SubjectName;
                existing.CourseLoad = subject.CourseLoad;
                existing.SemesterId = subject.SemesterId;
                _appDbContext.SaveChanges();
            }
        }

        public void DeleteSubject(int subjectId)
        {
            Subject existing = GetSubjectById(subjectId);
            if (existing != null)
            {
                _appDbContext.Remove(existing);
                _appDbContext.SaveChanges();
            }
        }
    }
}