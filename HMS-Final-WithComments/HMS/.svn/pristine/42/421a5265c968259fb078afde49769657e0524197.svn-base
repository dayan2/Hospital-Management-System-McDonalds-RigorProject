using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business.DTO;
using Mcd.HospitaManagementSystem.Business.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class BusinessUserRole : IBusinessUserRole
    {
        private LP_HMSDbEntities db = new LP_HMSDbEntities();
        public void InsertUserRole(UserRoleDTO UserRole)
        {
        
            UserRole objUserRole = new UserRole()
            {
                Role = UserRole.Role,
            };

            
            db.UserRoles.Add(objUserRole);
            db.SaveChanges();
          
        }

        public void EditUserRole(UserRoleDTO UserRole)
        {
            UserRole objUserRole = new UserRole()
            {
                Id = UserRole.Id,
                Role = UserRole.Role,
            };

            db.Entry(objUserRole).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUserRole(int UserId)
        {
            UserRole objUserRole = new UserRole()
            {
                Id = UserId
            };

            UserRole userrole = db.UserRoles.Find(objUserRole.Id);
            db.UserRoles.Remove(userrole);
            db.SaveChanges();
        }

        public UserRoleDTO ViewtUserRoleById(int UserId)
        {
            UserRole objUserRole=db.UserRoles.Find(UserId);

            UserRoleDTO objUser = new UserRoleDTO()
            {
                Role=objUserRole.Role
            };

            return objUser;
        }




        public IEnumerable<UserRoleDTO> ViewtUserRole()
        {
            List<UserRole> objUserRole = db.UserRoles.ToList();
            Mapper.CreateMap<UserRole, UserRoleDTO>();
            var modelList = Mapper.Map<IEnumerable<UserRole>, IEnumerable<UserRoleDTO>>(objUserRole);

            return modelList;
        }
    }
}
