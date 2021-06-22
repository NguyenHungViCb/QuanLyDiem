using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public interface ILecturerRepository
    {
        public IEnumerable<Lecturer> LecturerList { get; }
        public Lecturer GetLecturerById(int lecturerId);
        public Lecturer GetLecturerByEmail(string lecturerEmail);
        void CreateLecturer(Lecturer lecturer);
        void UpdateLecturer(Lecturer lecturer);
        void DeleteLecturer(int lecturerId);
    }
}