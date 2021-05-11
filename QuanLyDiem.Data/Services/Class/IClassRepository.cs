using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface IClassRepository
    {
        IEnumerable<Class> ClassList { get; }
        Class GetClassById(int classId);
    }
}