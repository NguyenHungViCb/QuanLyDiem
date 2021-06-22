using System;
using System.Collections;
using System.Collections.Generic;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Web.ViewModels
{
    public class StudentDetails
    {
        public Student Student { get; set; }
        public IEnumerable<Student> StudentList { get; set; }

        public IEnumerable<Class> ClassList { get; set; }
        
        public Boolean success { get; set; }
        public string Role { get; set; }
    }
}