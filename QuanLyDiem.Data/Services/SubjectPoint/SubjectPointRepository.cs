using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class SubjectPointRepository : ISubjectPointRepository
    {
        private readonly AppDbContext _appDbContext;

        public SubjectPointRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IEnumerable<SubjectPoint> SubjectPointList
        {
            get
            {
                return _appDbContext.SubjectPoints.Include(sp => sp.Student);
            }
        }


        public IEnumerable<SubjectPoint> SubjectPointListBySubject(int subjectId)
        {
            return _appDbContext.SubjectPoints.Include(sp => sp.Student)
                .Include(sp=> sp.Student.Class)
                .Include(sp=> sp.Subject)
                .Where(sp=>sp.Subject.SubjectId == subjectId);
        }

        public IEnumerable<SubjectPoint> SubjectPointListByStudentAndSemester(int studentId, int semesterId)
        {
            return _appDbContext.SubjectPoints.Include(sp => sp.Student)
                .Include(sp => sp.Subject)
                .Include(sp => sp.Subject.Semester)
                .Where(sp => sp.StudentId == studentId && sp.Subject.Semester.SemesterId == semesterId);
        }

        public SubjectPoint GetSubjectPointById(int subjectPointId)
        {
            return _appDbContext.SubjectPoints.Where(sp => sp.SubjectPointId == subjectPointId)
                .Include(sp => sp.Subject)
                .FirstOrDefault();
        }

        public IEnumerable<SubjectPoint> GetSubjectPointByStudentId(int studentId)
        {
            return _appDbContext.SubjectPoints.Where(sp => sp.StudentId == studentId)
                .Include(sp => sp.Subject).Select(sp => sp);
        }

        public void UpdateSubjectPoints(SubjectPoint subjectPoint)
        {
            SubjectPoint existing = GetSubjectPointById(subjectPoint.SubjectPointId);
            if (existing != null)
            {
                existing.FirstExam = subjectPoint.FirstExam;
                existing.SecondExam = subjectPoint.SecondExam;
                _appDbContext.SaveChanges();
            }
        }
    }
}