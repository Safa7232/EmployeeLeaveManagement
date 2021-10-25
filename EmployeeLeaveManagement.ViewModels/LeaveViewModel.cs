using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagement.ViewModels
{
    public class LeaveViewModel
    {
        public int LeaveID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime TimeSpan { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string LeaveReason { get; set; }


        public string LeaveStatus { get; set; }
    }
}
