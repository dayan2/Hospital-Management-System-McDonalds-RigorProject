using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserManager : IUserManager
    {
        private UserRegistration userRegistration;
        private UserRoleRegistration userRoleRegistration;
        public UserManager()
        {
            userRegistration = new UserRegistration();
            userRoleRegistration = new UserRoleRegistration();

        }


        #region
        public void AddNewUser()
        {
            userRegistration.AddUser();
            //  return new NotImplementedException();
        }


        public void InsertUserRole(DTO.UserRoleDTO UserRole)
        {
            UserRole objUserRole = new UserRole()
            {
                Role = UserRole.Role,
            };

            userRoleRegistration.AddUserRole(objUserRole);

        }

        public void EditUserRole(DTO.UserRoleDTO UserRole)
        {
            UserRole objUserRole = new UserRole()
            {
                Id = UserRole.Id,
                Role = UserRole.Role,
            };

            userRoleRegistration.EditUserRole(objUserRole);
        }

        void IUserManager.DeleteUserRole(int UserRoleId)
        {
            UserRole objUserRole = new UserRole()
            {
                Id = UserRoleId
            };

            userRoleRegistration.DeleteRole(UserRoleId);

        }

        public IEnumerable<DTO.UserRoleDTO> ViewtUserRole()
        {

            List<UserRole> lstUserRole = userRoleRegistration.ViewAllUserRole();

            Mapper.CreateMap<UserRole, UserRoleDTO>();
            var modelList = Mapper.Map<IEnumerable<UserRole>, IEnumerable<UserRoleDTO>>(lstUserRole);

            return modelList;
        }
        UserRoleDTO IUserManager.ViewtUserRoleById(int UserRoleId)
        {
            UserRole usrRole = userRoleRegistration.ViewUserRoleDetailsById(UserRoleId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Role = usrRole.Role
            };

            return objUser;
        }

        UserRoleDTO IUserManager.SelectUserRoleById(int UserRoleId)
        {
            UserRole usrRole = userRoleRegistration.ViewUserRoleDetailsById(UserRoleId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Role = usrRole.Role
            };

            return objUser;
        }

        public UserRoleDTO SelectToDeleteUserRole(int UserRoleId)
        {
            UserRole usrRole = userRoleRegistration.ViewUserRoleDetailsById(UserRoleId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Role = usrRole.Role
            };

            return objUser;
        }


        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>


        #region
        public void InsertUser(DTO.UserDTO User)
        {
            throw new NotImplementedException();
        }

        public void EditUser(DTO.UserDTO User)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DTO.UserRoleDTO> ViewtUser()
        {
            throw new NotImplementedException();
        }

        public DTO.UserRoleDTO ViewtUserById(int UserId)
        {
            throw new NotImplementedException();
        }

        void IUserManager.AddNewUser()
        {
            throw new NotImplementedException();
        }

        #endregion




        
    }
}
