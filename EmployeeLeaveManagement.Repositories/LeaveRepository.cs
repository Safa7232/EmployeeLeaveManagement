using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveManagement.DomainModels;

namespace EmployeeLeaveManagement.Repositories
{
    public interface ILeaveRepository
    {
        void InsertLeaveRequest(Leave leaveReq);
        Employee UpdateLeaveStatusByLeaveID(Leave leaveReq);

        List<Leave> GetLeaves();
        List<Leave> GetLeaveByEmployeeID(int EmployeeID);

         int GetLatestLeaveID();


    }
    public class LeaveRepository : ILeaveRepository
    {
        EmployeeLeaveManagementDatabaseDbContext db;

        public LeaveRepository()
        {
            db = new EmployeeLeaveManagementDatabaseDbContext();
        }

        public  void InsertLeaveRequest(Leave leaveReq)
        {
            db.Leave.Add(leaveReq);
            db.SaveChanges();
        }
        public Employee UpdateLeaveStatusByLeaveID(Leave leaveReq)
        {
            Leave leave = db.Leave.Where(temp => temp.LeaveID == leaveReq.LeaveID).FirstOrDefault();

            if (leave != null)
            {
                leave.LeaveStatus = leaveReq.LeaveStatus;
                db.SaveChanges();
            }

            Employee emp = db.Employee.Where(x => x.EmployeeID == leave.EmployeeID).ToList().FirstOrDefault();
            return emp;

        }



        public List<Leave> GetLeaveByEmployeeID(int EmployeeID)
        {
            List<Leave> leave = db.Leave.Where(temp => temp.EmployeeID == EmployeeID).ToList();
            return leave;
        }

        public int GetLatestLeaveID()
        {
            int leaveId = db.Leave.Select(temp => temp.LeaveID).Max();
            return leaveId;
        }

        public List<Leave> GetLeaves()
        {
            List<Leave> leave = db.Leave.OrderByDescending(temp => temp.StartDate).ToList();
            return leave;
        }
    }
}
