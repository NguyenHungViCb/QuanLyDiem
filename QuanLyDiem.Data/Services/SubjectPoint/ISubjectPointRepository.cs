using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface ISubjectPointRepository
    {
        IEnumerable<SubjectPoint> SubjectList { get; }
        SubjectPoint GetSubjectPointById(int subjectPoint);
        IEnumerable<SubjectPoint> GetSubjectPointByStudentId(int studentId);
        void UpdateSubjectPoints(SubjectPoint subjectPoint);
    }
}