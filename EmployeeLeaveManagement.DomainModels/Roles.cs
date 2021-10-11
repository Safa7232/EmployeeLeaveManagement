using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagement.DomainModels
{
    public class Roles
    {
        [Key]
        
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }
}
