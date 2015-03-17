#region Using Directives
using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;

#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserManager : IUserManager
    {
        #region Private Fields
        private UserRegistration userRegistration;
        private UserRoleRegistration userRoleRegistration;
        private UserLogin userLogin;
        #endregion

        #region Public User Role Related Methods
        
        /// <summary>
        /// Add user role to table
        /// </summary>
        /// <param name="userRole">User role model data object</param>
        /// <returns>Return insertetion ok or not</returns>
        public bool InsertUserRole(UserRoleDTO userRoledto)
        {

            userRoleRegistration = new UserRoleRegistration();

            //Map user role dto objects to User role entity objects
            UserRole userRole = new UserRole()
            {
                Role = userRoledto.Role,
            };

            //Add user role details
            return userRoleRegistration.AddUserRole(userRole);

        }

        /// <summary>
        /// Modified User role data
        /// </summary>
        /// <param name="userRole">Modified user role object</param>
        /// <returns>returns modification ok or not</returns>
        public bool EditUserRole(UserRoleDTO userRoledto)
        {
            userRoleRegistration = new UserRoleRegistration();

            //Map user role dto objects to User role entity objects
            UserRole userRole = new UserRole()
            {
                Id = userRoledto.Id,
                Role = userRoledto.Role,
            };

            //Edit user role details
            return userRoleRegistration.EditUserRole(userRole);
        }

        /// <summary>
        /// Delete user role data
        /// </summary>
        /// <param name="userRoleId">user role id</param>
        /// <returns>retirns deletion ok or not</returns>
        public bool DeleteUserRole(int userRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();

            //Map user role dto objects to User role entity objects
            UserRole userRole = new UserRole()
            {
                Id = userRoleId
            };

            //Delete user role detail by id
            return userRoleRegistration.DeleteRole(userRoleId);
        }

        /// <summary>
        /// View all user role data in table
        /// </summary>
        /// <returns>all user role data with object</returns>
        public IEnumerable<UserRoleDTO> ViewtUserRole()
        {
            userRoleRegistration = new UserRoleRegistration();
            //Get all user role details
            List<UserRole> listUserRole = userRoleRegistration.ViewAllUserRole();

            //Map user role entity objects to user role dto objects  
            Mapper.CreateMap<UserRole, UserRoleDTO>();
            var modelList = Mapper.Map<IEnumerable<UserRole>, IEnumerable<UserRoleDTO>>(listUserRole);

            return modelList;
        }

        /// <summary>
        /// View specific user role type
        /// </summary>
        /// <param name="userRoleId">Specific user role by id</param>
        /// <returns>specific user role object</returns>
        public UserRoleDTO ViewtUserRoleById(int? userRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();

            //Get specific user role details by id
            UserRole userRole = userRoleRegistration.ViewUserRoleDetailsById(userRoleId);

            //Map user role entity objects to user role dto objects 
            UserRoleDTO user = new UserRoleDTO()
            {
                Id = userRole.Id,
                Role = userRole.Role
            };

            return user;
        }

        /// <summary>
        /// Select to edit specific user role
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public UserRoleDTO SelectUserRoleById(int? userRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();
            //Get user role details by user role id
            UserRole userRole = userRoleRegistration.ViewUserRoleDetailsById(userRoleId);

            //Map user role entity objects to user role dto objects 
            UserRoleDTO userRoledto = new UserRoleDTO()
            {
                Id = userRole.Id,
                Role = userRole.Role
            };

            return userRoledto;
        }

        /// <summary>
        /// Select To Delete specific UserRole
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public UserRoleDTO SelectToDeleteUserRole(int userRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();

            //Get user role details by user role id
            UserRole userRole = userRoleRegistration.ViewUserRoleDetailsById(userRoleId);

            //Map user role entity objects to user role dto objects 
            UserRoleDTO user = new UserRoleDTO()
            {
                Role = userRole.Role,
                Id = userRoleId
            };

            return user;
        }
        #endregion

        #region Public User Related Methods

        /// <summary>
        /// View all users,user role type wise 
        /// </summary>
        /// <param name="userRole">User role view model data</param>
        /// <returns>Returns specific user according to user role wise</returns>
        public IEnumerable<UserDTO> ViewtUserByUserRoleId(string userRole)
        {
            userRegistration = new UserRegistration();
            //Select user role details by user role name
            return userRegistration.SelectUsersByUserRole(userRole);
        }

        /// <summary>
        /// Select user to create
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDTO> SelectToCreateUser()
        {
             try
             {
                 userRegistration = new UserRegistration();
                 //Select user details to create user
                 return userRegistration.SelectToCreate();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
        }
      

        /// <summary>
        /// Adding new user to user table 
        /// </summary>
        /// <param name="userDto">User data access object</param>
        /// <returns>Returns users inserted or not</returns>
        public bool InsertUser(UserDTO userDto)
        {

            //Map user dto type objects to user entity type objects
            User user = new User()
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Password = userDto.Password,
                UserRoleId = userDto.UserRoleId
            };

            userRegistration = new UserRegistration();

            //Add new user type object
            return userRegistration.AddNewUser(user);
        }

        /// <summary>
        /// Modifying selected user
        /// </summary>
        /// <param name="userdto">User type data access object</param>
        /// <returns>Returns users modified or not</returns>
        public bool EditUser(UserDTO userdto)
        {
            //Map user dto type objects to user entity type objects
            User user = new User()
            {
                Id = userdto.Id,
                UserName = userdto.UserName,
                Password = userdto.Password,
                UserRoleId = userdto.UserRoleId
            };

            userRegistration = new UserRegistration();

            //Add new user type modified object
            return userRegistration.EditUser(user);
        }

        /// <summary>
        /// Delete selected users from the table 
        /// </summary>
        /// <param name="userId">Selected user to delete</param>
        /// <returns>Returns deletetion successfull or not</returns>
        public bool DeleteUser(int userId)
        {
            userRegistration = new UserRegistration();

            //Delete user by user id
            return userRegistration.DeleteUser(userId);
        }
        
        /// <summary>
        /// Select all users to display
        /// </summary>
        /// <returns>All user IEnumerable type data access object</returns>
        public IEnumerable<UserDTO> ViewtAllUser()
        {
            try
            {
                userRegistration = new UserRegistration();

                //Get all user details
                IEnumerable<UserDTO> listUser = userRegistration.ViewAllUser();

                return listUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// View specific User by id
        /// </summary>
        /// <param name="id">selectted User id to view</param>
        /// <returns>User transfer object type data</returns>
        public UserDTO ViewtUserById(int? id)
        {
            userRegistration = new UserRegistration();
            //Get all user details by id
            return userRegistration.ViewUserById(id);

        }

     
        #endregion

        #region Public Check user Authentication Methods

        /// <summary>
        /// Method for check user authentication on login
        /// </summary>
        /// <param name="user">User data tranfer object</param>
        /// <returns>Authenticated user id</returns>
        public int CheckUserAuthentication(UserDTO user)
        {
            userLogin = new UserLogin();

            //Get already exsits user id by their username and password
            int userId = userLogin.CheckAuthentication(user);

            return userId;
        }
        #endregion

    }
}