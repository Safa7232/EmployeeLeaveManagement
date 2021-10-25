using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagement.DomainModels
{
    public class Leave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveID { get; set; }

        public int EmployeeID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        
       
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee EmployeeDetails { get; set; }


    }
}
