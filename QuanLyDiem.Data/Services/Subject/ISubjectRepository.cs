using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> SubjectList { get;}
        Subject GetSubjectById(int subjectId);
    }
}