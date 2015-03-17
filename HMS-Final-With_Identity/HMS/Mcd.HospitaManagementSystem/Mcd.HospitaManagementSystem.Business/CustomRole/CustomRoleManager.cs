using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business.CustomRole
{
    public class CustomRoleManager
    {
        public bool CheckUserInRole(string username, string roleName)
        {
            using (var dbContext = new LP_HMSDbEntities())
            {
                var user = dbContext.Users;
                var userRole = dbContext.UserRoles;

                var userRoleData = from us in user
                                   from ur in userRole
                                   where us.UserRoleId == ur.Id
                                   select ur.Role;

                if (userRoleData == null)
                    return false;
                return true;

            }
        }

        public string[] GetAllRolesForUser(string username)
        {
            using(var dbContext=new LP_HMSDbEntities())
            {               
                    int i = 0;
                    string[] arrUserRoles = new string[1];
                    var user = dbContext.Users;
                    var userRole = dbContext.UserRoles;

                    var userRoleData = from us in user
                                       from ur in userRole
                                       where us.UserRoleId == ur.Id && us.UserName == username
                                       select ur.Role;
                    if (userRoleData == null)
                        return null;
                    else

                        foreach (string s in userRoleData)
                        {
                            arrUserRoles[i] = s;
                            i++;
                        }
                    return arrUserRoles;
            }
        }

        public  string[] GetAllUserRoles()
        {
            using (var usersContext = new LP_HMSDbEntities())
            {
                return usersContext.UserRoles.Select(r => r.Role).ToArray();
            }
        }
    }
}
