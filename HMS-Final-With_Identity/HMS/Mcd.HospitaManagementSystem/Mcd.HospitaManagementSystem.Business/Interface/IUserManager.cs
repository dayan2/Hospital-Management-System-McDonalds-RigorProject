#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace Mcd.HospitaManagementSystem.Business
{
    public interface IUserManager
    {
        #region UserRole Function
        bool InsertUserRole(UserRoleDTO userRole);

        bool EditUserRole(UserRoleDTO userRole);

        UserRoleDTO SelectUserRoleById(int? userRoleId);

        UserRoleDTO SelectToDeleteUserRole(int userRoleId);


        bool DeleteUserRole(int userRoleId);

        IEnumerable<UserRoleDTO> ViewtUserRole();

        UserRoleDTO ViewtUserRoleById(int? userRoleId);

        #endregion 

        #region User Function
        bool InsertUser(UserDTO userDto);

        bool EditUser(UserDTO userdto);

        bool DeleteUser(int userId);

        IEnumerable<UserDTO> ViewtAllUser();

        IEnumerable<UserDTO> ViewtUserByUserRoleId(string userRole);


        UserDTO ViewtUserById(int? id);

        IEnumerable<UserDTO> SelectToCreateUser();

        #endregion

        #region Login Function

        int CheckUserAuthentication(UserDTO user);

        #endregion

    }
}
