using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{

    #region Public methods for testing user role functions through local db

    /// <summary>
    /// This test method is used to test ViewtUserRoles function in UserManager class 
    /// </summary>
    [TestClass]
    public class UserRoleUnitTest
    {
        [TestMethod]
        public void UserManagerViewtUserRoleshouldReturnUserRoleDTOtypelist()
        {
            // Arrange
            IUserManager userrolemanager = new UserManager();
            //Act
            var expextedUser=userrolemanager.ViewtUserRole();
            //Assert
            Assert.IsInstanceOfType(expextedUser, typeof(IEnumerable<UserRoleDTO>));
        }
        [TestMethod]
        public void UserRoleInsertUserRoleShouldInsertAUserroleTypeDtoObject()
        {
            // Arrange
            IUserManager userrolemanager = new UserManager();

            UserRoleDTO userdto = new UserRoleDTO()
            {
                Id = 1,
                Role = "Doctor"
            };
            //Act
            userrolemanager.InsertUserRole(userdto);

            using (var db = new LP_HMSDbEntities())
            {
                var insertedUserRoleIndex = db.UserRoles.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedUser = userrolemanager.ViewtUserRoleById(Convert.ToInt32(insertedUserRoleIndex));

                //Assert
                Assert.IsInstanceOfType(expectedUser, typeof(UserRoleDTO));
            }

        }
        /// <summary>
        /// This test method is used to test EditUserRole function in UserManager class 
        /// </summary>
        [TestMethod]
        public void UserRoleEditUserRoleModifyAUserRoleTypeDto()
        {
            // Arrange
            IUserManager usermanager = new UserManager();

            UserRoleDTO userDto = new UserRoleDTO()
            {
                Id =1,
                Role ="Doctor"
            };
            //Act
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
        /// <summary>
        /// This test method is used to test DeleteUserRole function in UserManager class 
        /// </summary>
        [TestMethod]
        public void UsermanagerDeleteUserRoleshoulDeleteUserRoleType()
        {
            // Arrange
            IUserManager usermanager = new UserManager();

            UserRoleDTO userDto = new UserRoleDTO()
            {
                Role = "CleRK"
            };
            //Act
            usermanager.InsertUserRole(userDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastuser = db.UserRoles.OrderByDescending(u => u.Id).FirstOrDefault();

                //Assert
                usermanager.DeleteUserRole(lastuser.Id);
            }
        }
        /// <summary>
        /// This test method is used to test ViewtUserRoleById function in UserManager class 
        /// </summary>
        [TestMethod]
        public void UserManagerViewtUserRoleByIdMethodShouldReturnAUserRoleTypeDto()
        {
            //Arrange
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


#endregion


    }
}
