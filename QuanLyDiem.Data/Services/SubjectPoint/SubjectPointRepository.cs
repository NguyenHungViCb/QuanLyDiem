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

        public SubjectPoint GetSubjectPointById(int subjectPoint)
        {
            return _appDbContext.SubjectPoints.FirstOrDefault(sp => sp.SubjectPointId == subjectPoint);
        }

        public IEnumerable<SubjectPoint> GetSubjectPointByStudentId(int studentId)
        {
            return _appDbContext.SubjectPoints.Where(sp => sp.StudentId == studentId).Select(sp => sp);
        }
    }
}