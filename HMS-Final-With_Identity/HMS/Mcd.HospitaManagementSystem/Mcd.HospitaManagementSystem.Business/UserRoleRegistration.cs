﻿#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserRoleRegistration
    {
        #region Public Methods

        /// <summary>
        /// Add user role detail to the db  
        /// </summary>
        /// <param name="usrRole">User Role object</param>
        /// <returns></returns>
        public bool AddUserRole(UserRole usrRole)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get first or defualt that has the same user role type 
                    UserRole usrrole = db.UserRoles.Where(x => x.Role == usrRole.Role).FirstOrDefault();

                    //Insert user role object to the table if not exists same user role in db
                    if (usrrole == null)
                    {
                        db.UserRoles.Add(usrRole);
                        db.SaveChanges();

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }
        /// <summary>
        /// Modified already exists user role
        /// </summary>
        /// <param name="usrRole">User Role object</param>
        /// <returns></returns>
        public bool EditUserRole(UserRole usrRole)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get first or defualt that has the same user role type 
                    UserRole userrole = db.UserRoles.Where(x => x.Role == usrRole.Role && x.Id != usrRole.Id).FirstOrDefault();

                    //Modified user role object to the table if not exists same user role in db
                    if (userrole == null)
                    {
                        db.Entry(usrRole).State = EntityState.Modified;
                        db.SaveChanges();

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }
        /// <summary>
        /// Delete user role by id 
        /// </summary>
        /// <param name="usrRoleId">User role id</param>
        /// <returns></returns>
        public bool DeleteRole(int usrRoleId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Delete User role
                    UserRole userrole = db.UserRoles.Find(usrRoleId);
                    db.UserRoles.Remove(userrole);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        /// <summary>
        /// Select All User Role Details
        /// </summary>
        /// <returns> Returns user role details</returns>
        public List<UserRole> ViewAllUserRole()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Select all user role details
                    List<UserRole> objUserRole = db.UserRoles.ToList();
                    return objUserRole;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Select distinct user role
        /// </summary>
        /// <returns>Returns distinct user role details</returns>
        public List<UserRole> DistinctUserRole()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get distinct user role details
                    List<UserRole> objUserRole = db.UserRoles.ToList();
                    return objUserRole;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Select User Role Details By Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserRole ViewUserRoleDetailsById(int? UserId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Select user role details by id
                    UserRole objUserRole = db.UserRoles.Find(UserId);
                    return objUserRole;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

    }
}
