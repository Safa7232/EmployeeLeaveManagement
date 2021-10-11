using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EmployeeLeaveManagement.DomainModels
{
    public class EmployeeLeaveManagementDatabaseDbContext : DbContext
    {
        public EmployeeLeaveManagementDatabaseDbContext(): base("MyConnectionString")
        {

        }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Leave> Leave { get; set; }
    }
}
