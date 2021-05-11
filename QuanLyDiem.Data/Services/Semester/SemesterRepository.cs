using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly AppDbContext _appDbContext;

        public SemesterRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IEnumerable<Semester> SemesterList => _appDbContext.Semesters;

        public Models.Semester GetSemesterById(int semesterId)
        {
            return _appDbContext.Semesters.FirstOrDefault(s => s.SemesterId == semesterId);
        }
    }
}