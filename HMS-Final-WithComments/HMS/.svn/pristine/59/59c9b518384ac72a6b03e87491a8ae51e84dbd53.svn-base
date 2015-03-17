using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserRoleRegistration
    {
        private LP_HMSDbEntities db = new LP_HMSDbEntities();

        public void AddUserRole(UserRole usrRole)
        {
            db.UserRoles.Add(usrRole);
            db.SaveChanges();
        }

        public void EditUserRole(UserRole usrRole)
        {
            db.Entry(usrRole).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteRole(int usrRoleId)
        {
            UserRole userrole = db.UserRoles.Find(usrRoleId);
            db.UserRoles.Remove(userrole);
            db.SaveChanges();
        }

        public List<UserRole> ViewAllUserRole()
        {
             List<UserRole> objUserRole = db.UserRoles.ToList();
             return objUserRole;
        }

        public UserRole ViewUserRoleDetailsById(int UserId)
        {
            UserRole objUserRole = db.UserRoles.Find(UserId);
            return objUserRole;
        }

    }
}
