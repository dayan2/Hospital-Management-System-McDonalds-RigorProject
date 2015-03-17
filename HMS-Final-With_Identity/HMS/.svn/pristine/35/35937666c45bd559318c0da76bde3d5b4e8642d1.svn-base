using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public interface IUserManager
    {
        void InsertUserRole(UserRoleDTO UserRole);

        void EditUserRole(UserRoleDTO UserRole);

        UserRoleDTO SelectUserRoleById(int UserRoleId);

        UserRoleDTO SelectToDeleteUserRole(int UserRoleId);

        void DeleteUserRole(int UserRoleId);

        IEnumerable<UserRoleDTO> ViewtUserRole();

        UserRoleDTO ViewtUserRoleById(int UserRoleId);

        void InsertUser(UserDTO User);

        void EditUser(UserDTO User);

        void DeleteUser(int UserId);

        IEnumerable<UserRoleDTO> ViewtUser();

        UserRoleDTO ViewtUserById(int UserId);
        void AddNewUser();
    }
}
