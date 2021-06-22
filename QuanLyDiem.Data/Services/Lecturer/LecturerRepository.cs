using System.Collections.Generic;
using System.Linq;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data.Services
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly AppDbContext _appDbContext;

        public LecturerRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public IEnumerable<Lecturer> LecturerList => _appDbContext.Lecturers;

        public Lecturer GetLecturerById(int lecturerId)
        {
            return _appDbContext.Lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);
        }

        public Lecturer GetLecturerByEmail(string lecturerEmail)
        {
            return _appDbContext.Lecturers.FirstOrDefault(l => l.Email == lecturerEmail);
        }

        public void CreateLecturer(Lecturer lecturer)
        {
            _appDbContext.Add(lecturer);
            _appDbContext.SaveChanges();
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            Lecturer exiting = GetLecturerById(lecturer.LecturerId);
            if (exiting != null)
            {
                exiting.FirstName = lecturer.FirstName;
                exiting.LastName = lecturer.LastName;
                exiting.Email = lecturer.Email;
                exiting.Password = lecturer.Password;
                exiting.Gender = lecturer.Gender;
                exiting.Address = lecturer.Address;
                exiting.PhoneNumber = lecturer.PhoneNumber;
                _appDbContext.SaveChanges();
            }
        }

        public void DeleteLecturer(int lecturerId)
        {
            Lecturer existing = GetLecturerById(lecturerId);
            if (existing != null)
            {
                _appDbContext.Lecturers.Remove(existing);
                _appDbContext.SaveChanges();
            }
        }
    }
}