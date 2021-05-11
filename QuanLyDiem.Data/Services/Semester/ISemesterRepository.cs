using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface ISemesterRepository
    {
        IEnumerable<Semester> SemesterList { get;}
        Models.Semester GetSemesterById(int semesterId);
    }
}