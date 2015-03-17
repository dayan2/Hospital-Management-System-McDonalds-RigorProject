#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    class UserRegistration
    {


        #region Public Methods

        /// <summary>
        /// Add new user to the system
        /// </summary>
        /// <param name="user">User Details</param>
        /// <returns>Return success or failure</returns>
        public bool AddNewUser(User user)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get first or defualt that has the same user details 
                    User users = db.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();

                    //Insert user object to the table if not exists same user in db
                    if (users == null)
                    {
                        db.Users.Add(user);
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
        /// Get all users in system 
        /// </summary>
        /// <returns>All user IEnumerable type object</returns>
        public IEnumerable<UserDTO> ViewAllUser()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get all users from the db
                    var userList = db.Users.ToList();
                    var userRoleList = db.UserRoles.ToList();

                    //Mapping User entity type object to a userdto type object
                    IEnumerable<UserDTO> userSelectedList = from ul in userList
                                                            from ur in userRoleList
                                                            where ul.UserRoleId == ur.Id
                                                            select new UserDTO
                                                            {
                                                                Id = ul.Id,
                                                                UserName = ul.UserName,
                                                                Password = ul.Password,
                                                                UserRole = ul.UserRole,
                                                                UserRoleId = ul.UserRoleId,
                                                                UserRoleType = ur.Role,

                                                            };

                    return userSelectedList;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Modified already exists user
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns>Returns success or failuer</returns>
        public bool EditUser(User user)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get first or defualt that has the same user type 
                    User users = db.Users.Where(x => x.UserName == user.UserName && x.Id != user.Id).FirstOrDefault();

                    //Modified user object to the table if not exists same user in db
                    if (users == null)
                    {
                        db.Entry(user).State = EntityState.Modified;

                        //Save modified object
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
        /// Get specific user object
        /// </summary>
        /// <param name="Id">User Id</param>
        /// <returns>Specific User type object</returns>
        public UserDTO ViewUserById(int? Id)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Find specified user
                    User user = db.Users.Find(Id);

                    //Mapping User entity type object to a userdto type object
                    UserDTO userdto = new UserDTO()
                    {
                        Id = user.Id,
                        Password = user.Password,
                        UserName = user.UserName,
                        UserRoleId = user.UserRoleId,
                        UserRole = user.UserRole,
                        UserRoleType =user.UserRole.Role
                    };

                    return userdto;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Get User details By UserRole
        /// </summary>
        /// <param name="userRole">User role that need to get user details</param>
        /// <returns>IEnumerable type user dto object</returns>
        public IEnumerable<UserDTO> SelectUsersByUserRole(string userRole)
        {

            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                //Get all users from the db
                var userList = db.Users.ToList();

                //Get all user role type object from the db
                var userRoleList = db.UserRoles.ToList();

                //Mapping User entity type object to a userdto IEnumerable type object
                IEnumerable<UserDTO> userSelectedList = from ul in userList
                                                        from ur in userRoleList
                                                        where ul.UserRoleId == ur.Id && ur.Role == userRole
                                                        select new UserDTO
                                                        {
                                                            Id = ul.Id,
                                                            UserName = ul.UserName,
                                                            Password = ul.Password,
                                                            UserRoleId = ul.UserRoleId,
                                                            UserRoleType = ur.Role

                                                        };

                return userSelectedList;
            }
        }
        /// <summary>
        /// Get user to create
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDTO> SelectToCreate()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get all users of the system
                    var usersList = db.Users.ToList();

                    //Mapping User entity type object to a userdto IEnumerable type object
                    IEnumerable<UserDTO> users = from ul in usersList
                                                 select new UserDTO
                                                 {
                                                     Id = ul.Id,
                                                     UserName = ul.UserName,
                                                     Password = ul.Password,
                                                     ConfirmPassword = ""
                                                 };
                    return users;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Delete user object by user id
        /// </summary>
        /// <param name="Id">User id</param>
        /// <returns>Return deletion is success or failure </returns>
        public bool DeleteUser(int Id)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Find user need to be deleted
                    User user = db.Users.Find(Id);

                    //Delete user from the system
                    db.Users.Remove(user);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        #endregion


    }
}
