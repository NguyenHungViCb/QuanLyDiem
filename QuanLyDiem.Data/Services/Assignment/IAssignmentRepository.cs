using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment>  AssignmentLList { get; }
        Assignment GetAssignmentById(int assignmentId);
        IEnumerable<Assignment> GetAssignmentListByLecturer(int lecturerId, int semesterId);
    }
}