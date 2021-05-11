using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class SubjectClassRepository : ISubjectClassRepository
    {
        private readonly AppDbContext _appDbContext;

        public SubjectClassRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public IEnumerable<SubjectClass> SubjectClassList
        {
            get
            {
                return _appDbContext.SubjectClasses.Include(s => s.Subject)
                    .Include(s => s.Lecturer);
            }
        }

        public SubjectClass GetSubjectClassById(int subjectClassId)
        {
            throw new System.NotImplementedException();
        }
    }
}