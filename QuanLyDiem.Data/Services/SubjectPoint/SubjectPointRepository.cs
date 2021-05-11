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

        public IEnumerable<SubjectPoint> SubjectList
        {
            get
            {
                return _appDbContext.SubjectPoints.Include(sp => sp.Student);
            }
        }

        public SubjectPoint GetSubjectPointById(int subjectPointId)
        {
            return _appDbContext.SubjectPoints.Where(sp => sp.SubjectPointId == subjectPointId)
                .Include(sp => sp.Subject).FirstOrDefault();
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