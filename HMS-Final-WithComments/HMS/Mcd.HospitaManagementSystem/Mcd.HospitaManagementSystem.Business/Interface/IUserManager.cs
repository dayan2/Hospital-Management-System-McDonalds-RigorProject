﻿using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public interface IUserManager
    {
        #region UserRole Function
        bool InsertUserRole(UserRoleDTO UserRole);

        bool EditUserRole(UserRoleDTO UserRole);

        UserRoleDTO SelectUserRoleById(int UserRoleId);

        UserRoleDTO SelectToDeleteUserRole(int UserRoleId);


        bool DeleteUserRole(int UserRoleId);

        IEnumerable<UserRoleDTO> ViewtUserRole();

        UserRoleDTO ViewtUserRoleById(int UserRoleId);

        #endregion 

        #region User Function
        bool InsertUser(UserDTO userDto);

        bool EditUser(UserDTO userdto);

        bool DeleteUser(int UserId);

        IEnumerable<UserDTO> ViewtAllUser();

        IEnumerable<UserDTO> ViewtUserByUserRoleId(string userRole);


        UserDTO ViewtUserById(int id);

        IEnumerable<UserDTO> SelectToCreateUser();

        #endregion

        #region Login Function

        int CheckUserAuthentication(UserDTO user);

        #endregion

    }
}