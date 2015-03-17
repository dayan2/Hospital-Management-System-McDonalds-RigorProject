using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class UserRoleUnitTest
    {
        [TestMethod]
        public void UserManagerViewtUserRoleshouldReturnUserRoleDTOtypelist()
        {
            IUserManager userrolemanager = new UserManager();

            var expextedUser=userrolemanager.ViewtUserRole();

            Assert.IsInstanceOfType(expextedUser, typeof(IEnumerable<UserRoleDTO>));
        }
        [TestMethod]
        public void UserRoleInsertUserRoleShouldInsertAUserroleTypeDtoObject()
        {
            IUserManager userrolemanager = new UserManager();

            UserRoleDTO userdto = new UserRoleDTO()
            {
                Id = 1,
                Role = "Doctor"
            };

            userrolemanager.InsertUserRole(userdto);

            using (var db = new LP_HMSDbEntities())
            {
                var insertedUserRoleIndex = db.UserRoles.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedUser = userrolemanager.ViewtUserRoleById(Convert.ToInt32(insertedUserRoleIndex));

                //Assert
                Assert.IsInstanceOfType(expectedUser, typeof(UserRoleDTO));
            }

        }
        [TestMethod]
        public void UserRoleEditUserRoleModifyAUserRoleTypeDto()
        {
            IUserManager usermanager = new UserManager();

            UserRoleDTO userDto = new UserRoleDTO()
            {
                Id =1,
                Role ="Doctor"
            };
            usermanager.InsertUserRole(userDto);
            using(var db= new LP_HMSDbEntities())
            {
                 var insertedUserRoleIndex= db.UserRoles.OrderByDescending(x=>x.Id).Max(y=>y.Id);

                UserRoleDTO userDtoTest= new UserRoleDTO()
                {
                    Id =insertedUserRoleIndex,
                    Role ="Admin"
                };

                usermanager.EditUserRole(userDtoTest);

                var editedUser = usermanager.ViewtUserRoleById(Convert.ToInt32(insertedUserRoleIndex));

                //Assert
                Assert.IsInstanceOfType(editedUser, typeof(UserRoleDTO));
            };
           
        }
        [TestMethod]
        public void UsermanagerDeleteUserRoleshoulDeleteUserRoleType()
        {
            IUserManager usermanager = new UserManager();

            UserRoleDTO userDto = new UserRoleDTO()
            {
                Role = "CleRK"
            };

            usermanager.InsertUserRole(userDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastuser = db.UserRoles.OrderByDescending(u => u.Id).FirstOrDefault();

                //Assert
                usermanager.DeleteUserRole(lastuser.Id);
            }
        }
        [TestMethod]
        public void UserManagerViewtUserRoleByIdMethodShouldReturnAUserRoleTypeDto()
        {
            IUserManager usermanager = new UserManager();

            UserRoleDTO userRoleInsertDto = new UserRoleDTO()
            {
                Role ="Buisnessmen"
            };

            //Act
            usermanager.InsertUserRole(userRoleInsertDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastuserrole = db.UserRoles.OrderByDescending(u => u.Id).FirstOrDefault();

                //Assert
                var selectedUserRole = usermanager.ViewtUserRoleById(lastuserrole.Id);

                Assert.IsInstanceOfType(selectedUserRole, typeof(UserRoleDTO));
            }
        }

    }
}
