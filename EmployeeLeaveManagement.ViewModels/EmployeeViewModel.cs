using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveManagement.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public long ContactNumber { get; set; }

        public string Designation { get; set; }

        public int RoleID { get; set; }

        public string ImageURL { get; set; }

        public bool IsSpecialPermission { get; set; }
        
    }
}
