using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagement.DomainModels
{
    public class Employee
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        
        public long ContactNumber { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int RoleID { get; set; }

        public string Designation { get; set; }

        public string ImageURL { get; set; }

        public bool IsSpecialPermission { get; set; }

        [ForeignKey("RoleID")]
        public virtual Roles Role { get; set; }

    }
}
