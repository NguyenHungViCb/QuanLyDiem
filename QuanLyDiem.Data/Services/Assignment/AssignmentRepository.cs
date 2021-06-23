﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _appDbContext;

        public AssignmentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        
        public IEnumerable<Assignment> AssignmentLList
        {
            get
            {
                return _appDbContext.Assignments
                    .Include(s => s.Lecturer)
                    .Include(s => s.Subject);
            }
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            return _appDbContext.Assignments.FirstOrDefault(a => a.AssignmentId == assignmentId);
        }

        public IEnumerable<Assignment> GetAssignmentListByLecturer(int lecturerId, int semesterId)
        {
            return _appDbContext.Assignments
                .Include(a => a.Subject)
                .Include(a => a.Subject.Semester)
                .Where(a => a.LecturerId == lecturerId && a.Subject.Semester.SemesterId == semesterId)
                .Select(a => a);
        }
    }
}