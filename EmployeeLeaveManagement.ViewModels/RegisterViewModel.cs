using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string ContactNumber { get; set; }

        public string Designation { get; set; }

        public int RoleID { get; set; }

        public string ImageURL { get; set; }
    }
}
