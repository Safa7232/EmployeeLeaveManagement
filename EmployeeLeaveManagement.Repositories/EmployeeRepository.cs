using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveManagement.DomainModels;

namespace EmployeeLeaveManagement.Repositories
{
    public interface IEmployeeRepository
    {
        void InsertEmployee(Employee emp);
        void UpdateEmployeeDetails(Employee emp);
        void UpdateEmployeePassword(Employee emp);
        void DeleteEmployee(int empId);
        List<Employee> GetEmployee();
        List<Employee> GetEmployeesByEmailAndPassword(string Email, string Password);
        List<Employee> GetEmployeesByEmail(string Email);
        List<Employee> GetEmployeesByEmployeeID(int EmployeeID);

        List<Employee> GetEmployeesByRoleID(int RoleID);

        int GetLatestEmployeeID();

    }
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeLeaveManagementDatabaseDbContext db;

        public EmployeeRepository()
        {
            db = new EmployeeLeaveManagementDatabaseDbContext();
        }

        public void InsertEmployee(Employee emp)
        {
            db.Employee.Add(emp);
            db.SaveChanges();
        }

        public void UpdateEmployeeDetails(Employee emp)
        {
            Employee emps = db.Employee.Where(temp => temp.EmployeeID == emp.EmployeeID).FirstOrDefault();
            if (emps != null)
            {
                emps.FirstName = emp.FirstName;
                emps.LastName = emp.LastName;
                emps.ContactNumber = emp.ContactNumber;
                emps.Designation = emp.Designation;
                db.SaveChanges();
            }
        }

        public void UpdateEmployeePassword(Employee emp)
        {
            Employee emps = db.Employee.Where(temp => temp.EmployeeID == emp.EmployeeID).FirstOrDefault();
            if (emps != null)
            {
                emps.PasswordHash = emp.PasswordHash;
                db.SaveChanges();
            }
        }

        public void DeleteEmployee(int empId)
        {
            Employee emps = db.Employee.Where(temp => temp.EmployeeID == empId).FirstOrDefault();
            if (emps != null)
            {
                db.Employee.Remove(emps);
                db.SaveChanges();
            }
        }

        public List<Employee> GetEmployee()
        {
            List<Employee> emps = db.Employee.OrderBy(temp => temp.FirstName).ToList();
            return emps;
        }

        public List<Employee> GetEmployeesByEmailAndPassword(string Email, string PasswordHash)
        {
             List<Employee> emps = db.Employee.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
            return emps;
        }

        public List<Employee> GetEmployeesByEmail(string Email)
        {
            List<Employee> emps = db.Employee.Where(temp => temp.Email == Email).ToList();
            return emps;
        }

        public List<Employee> GetEmployeesByEmployeeID(int EmployeeID)
        {
            List<Employee> emps = db.Employee.Where(temp => temp.EmployeeID == EmployeeID).ToList();
            return emps;
        }

        public List<Employee> GetEmployeesByRoleID(int RoleID)
        {
            List<Employee> emps = db.Employee.Where(temp => temp.RoleID == RoleID).ToList();
            return emps;
        }

        public int GetLatestEmployeeID()
        {
            int emId = db.Employee.Select(temp => temp.EmployeeID).Max();
            return emId;
        }
    }
}

