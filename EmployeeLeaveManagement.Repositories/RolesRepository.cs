using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveManagement.DomainModels;

namespace EmployeeLeaveManagement.Repositories
{
    public interface IRolesRepository
    {
        void InsertRole(Roles role);
        void DeleteRole(int RoleId);
        List<Roles> GetRoles();
        List<Roles> GetRolesByRoleID(int RoleID);

    }


    public class RolesRepository :IRolesRepository
    {
        EmployeeLeaveManagementDatabaseDbContext db;

        public RolesRepository()
        {
            db = new EmployeeLeaveManagementDatabaseDbContext();
        }

        public void InsertRole(Roles role)
        {
            db.Roles.Add(role);
            db.SaveChanges();
        }

        public void DeleteRole(int Roleid)
        {
            Roles role = db.Roles.Where(temp => temp.RoleID == Roleid).FirstOrDefault();
            if (role != null)
            {
                db.Roles.Remove(role);
                db.SaveChanges();
            }
        }

        public List<Roles> GetRoles()
        {
            List<Roles> role = db.Roles.ToList();
            return role;
        }

        public List<Roles> GetRolesByRoleID(int RoleID)
        {
            List<Roles> role = db.Roles.Where(temp => temp.RoleID == RoleID).ToList();
            return role;
        }
    }
}
