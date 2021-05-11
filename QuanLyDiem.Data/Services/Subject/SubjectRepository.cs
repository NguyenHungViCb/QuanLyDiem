using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }
    }
}