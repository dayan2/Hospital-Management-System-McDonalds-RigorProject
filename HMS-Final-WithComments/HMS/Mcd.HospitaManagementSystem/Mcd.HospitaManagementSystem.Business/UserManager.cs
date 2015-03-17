using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserManager : IUserManager
    {
        #region Private Fields
        private UserRegistration userRegistration;
        private UserRoleRegistration userRoleRegistration;
        private UserLogin userLogin;
        #endregion

        #region Public User Role Methods
        
        /// <summary>
        /// Add user role to table
        /// </summary>
        /// <param name="UserRole">User role model data object</param>
        /// <returns>Return insertetion ok or not</returns>
        public bool InsertUserRole(UserRoleDTO UserRole)
        {

            userRoleRegistration = new UserRoleRegistration();
            UserRole objUserRole = new UserRole()
            {
                Role = UserRole.Role,
            };

            return userRoleRegistration.AddUserRole(objUserRole);

        }

        /// <summary>
        /// Modified User role data
        /// </summary>
        /// <param name="UserRole">Modified user role object</param>
        /// <returns>returns modification ok or not</returns>
        public bool EditUserRole(UserRoleDTO UserRole)
        {
            userRoleRegistration = new UserRoleRegistration();
            UserRole objUserRole = new UserRole()
            {
                Id = UserRole.Id,
                Role = UserRole.Role,
            };

            return userRoleRegistration.EditUserRole(objUserRole);
        }

        /// <summary>
        /// Delete user role data
        /// </summary>
        /// <param name="UserRoleId">user role id</param>
        /// <returns>retirns deletion ok or not</returns>
        public bool DeleteUserRole(int UserRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();
            UserRole objUserRole = new UserRole()
            {
                Id = UserRoleId
            };

            return userRoleRegistration.DeleteRole(UserRoleId);
        }

        /// <summary>
        /// View all user role data in table
        /// </summary>
        /// <returns>all user role data with object</returns>
        public IEnumerable<UserRoleDTO> ViewtUserRole()
        {
            userRoleRegistration = new UserRoleRegistration();
            List<UserRole> lstUserRole = userRoleRegistration.ViewAllUserRole();

            Mapper.CreateMap<UserRole, UserRoleDTO>();
            var modelList = Mapper.Map<IEnumerable<UserRole>, IEnumerable<UserRoleDTO>>(lstUserRole);

            return modelList;
        }

        /// <summary>
        /// View specific user role type
        /// </summary>
        /// <param name="UserRoleId">Specific user role by id</param>
        /// <returns>specific user role object</returns>
        public UserRoleDTO ViewtUserRoleById(int UserRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();
            UserRole usrRole = userRoleRegistration.ViewUserRoleDetailsById(UserRoleId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Id = usrRole.Id,
                Role = usrRole.Role
            };

            return objUser;
        }

        /// <summary>
        /// Select to edit specific user role
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public UserRoleDTO SelectUserRoleById(int UserRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();
            UserRole usrRole = userRoleRegistration.ViewUserRoleDetailsById(UserRoleId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Id = usrRole.Id,
                Role = usrRole.Role
            };

            return objUser;
        }

        /// <summary>
        /// Select To Delete specific UserRole
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public UserRoleDTO SelectToDeleteUserRole(int UserRoleId)
        {
            userRoleRegistration = new UserRoleRegistration();
            UserRole usrRole = userRoleRegistration.ViewUserRoleDetailsById(UserRoleId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Role = usrRole.Role
            };

            return objUser;
        }
        #endregion

        #region Public User Methods

        /// <summary>
        /// View all users ,user role type wise 
        /// </summary>
        /// <param name="userRole">User role view model data</param>
        /// <returns>Returns specific user according to user role wise</returns>
        public IEnumerable<UserDTO> ViewtUserByUserRoleId(string userRole)
        {
            userRegistration = new UserRegistration();
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
            User user = new User()
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Password = userDto.Password,
                UserRoleId = userDto.UserRoleId
            };

            userRegistration = new UserRegistration();
            return userRegistration.AddNewUser(user);
        }

        /// <summary>
        /// Modified selected user
        /// </summary>
        /// <param name="userdto">User type data access object</param>
        /// <returns>Returns users modified or not</returns>
        public bool EditUser(UserDTO userdto)
        {
            User user = new User()
            {
                Id = userdto.Id,
                UserName = userdto.UserName,
                Password = userdto.Password,
                UserRoleId = userdto.UserRoleId
            };

            userRegistration = new UserRegistration();
            return userRegistration.EditUser(user);
        }

        /// <summary>
        /// Delete selected users from the table 
        /// </summary>
        /// <param name="UserId">Selected user to delete</param>
        /// <returns>Returns deletetion successfull or not</returns>
        public bool DeleteUser(int UserId)
        {
            userRegistration = new UserRegistration();
            return userRegistration.DeleteUser(UserId);
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
                IEnumerable<UserDTO> lstUser = userRegistration.ViewAllUser();

                return lstUser;
            }
            catch (Exception ex)
            {
                throw (new ArgumentOutOfRangeException());
            }

        }


        /// <summary>
        /// View specific User by id
        /// </summary>
        /// <param name="id">selectted User id to view</param>
        /// <returns>User transfer object type data</returns>
        public UserDTO ViewtUserById(int id)
        {
            userRegistration = new UserRegistration();
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
            int userId = userLogin.CheckAuthentication(user);

            return userId;
        }
        #endregion


      
    }
}