using System;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Web.ViewModels
{
    public class LecturersManager
    {

        public Lecturer Lecturer { get; set; }
        public IEnumerable<Lecturer> LecturerList { get; set; }
        public string Role { get; set; }
    }
}