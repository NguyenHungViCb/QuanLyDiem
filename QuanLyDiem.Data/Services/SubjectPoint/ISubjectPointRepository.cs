using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface ISubjectPointRepository
    {
        IEnumerable<SubjectPoint> SubjectPointList { get; }
        IEnumerable<SubjectPoint> SubjectPointListBySubject(int subjectId);
        IEnumerable<SubjectPoint> SubjectPointListByStudentAndSemester(int studentId, int semesterId);
        SubjectPoint GetSubjectPointById(int subjectPoint);
        IEnumerable<SubjectPoint> GetSubjectPointByStudentId(int studentId);
        void UpdateSubjectPoints(SubjectPoint subjectPoint);
    }
}