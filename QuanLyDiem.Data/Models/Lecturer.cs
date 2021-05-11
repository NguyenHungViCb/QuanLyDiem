using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDiem.Data.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        [Required(ErrorMessage = "First name must not empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name must not empty")]
        public string LastName { get; set; }
        [Required]
        public Boolean Gender { get; set; }
        [Required(ErrorMessage = "Email must not empty")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must not empty")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}