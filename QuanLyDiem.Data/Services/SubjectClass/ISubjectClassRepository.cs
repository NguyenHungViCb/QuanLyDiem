using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface ISubjectClassRepository
    {
        IEnumerable<SubjectClass> SubjectClassList { get; }
        SubjectClass GetSubjectClassById(int subjectClassId);
    }
}