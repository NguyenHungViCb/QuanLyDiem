using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClassRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IEnumerable<Class> ClassList => _appDbContext.Classes;

        public Class GetClassById(int classId)
        {
           return _appDbContext.Classes.FirstOrDefault(c => c.ClassId == classId);
        }
    }
}