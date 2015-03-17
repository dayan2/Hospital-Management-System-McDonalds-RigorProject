using Mcd.HospitaManagementSystem.Business.CustomRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mcd.HospitalManagement.Web.UserIdentityScope
{
    public class CustomRoleProvider :RoleProvider 
    {
        CustomRoleManager customUserRole;

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            customUserRole = new CustomRoleManager();

            return customUserRole.GetAllUserRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            customUserRole = new CustomRoleManager();

            return customUserRole.GetAllRolesForUser(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            customUserRole = new CustomRoleManager();

            return customUserRole.CheckUserInRole(username, roleName);
                       
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}