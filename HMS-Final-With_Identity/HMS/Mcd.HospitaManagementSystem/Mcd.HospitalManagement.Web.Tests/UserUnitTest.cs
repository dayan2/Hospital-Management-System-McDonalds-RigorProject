using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;


namespace Mcd.HospitalManagement.Web.Tests
{

    #region Public methods for testing user manager functions through local db
    [TestClass]
    public class UserUnitTest
    {
        /// <summary>
        /// This test method is used to test ViewAllUser function in UserRegistration class 
        /// </summary>
        [TestMethod]
        public void UserRegistrationViewAllUserMethodShouldReturnAUserDTOTypeOfList()
        { 
            // Arrange
            IUserManager usermanager = new UserManager();

            //Act
            var expectedUserList = usermanager.ViewtAllUser();

            //Assert
            Assert.IsInstanceOfType(expectedUserList, typeof(IEnumerable<UserDTO>));
        }

        /// <summary>
        /// This test method is used to test AddNewUser function in UserRegistration class 
        /// </summary>
        [TestMethod]
        public void UserRegistrationAddNewUserMethodShouldInsertAUserDTOTypeOfList()
        {
            // Arrange
            IUserManager usermanager = new UserManager();
   
            UserDTO userDto = new UserDTO()
            {
                UserName = "Duminda1",
                Password = "123",
                UserRoleId = 1
            };

            //Act
            usermanager.InsertUser(userDto);


            using (var db = new LP_HMSDbEntities())
            {
                var insertedUserIndex = db.Users.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedUser = usermanager.ViewtUserById(Convert.ToInt32(insertedUserIndex));

                //Assert
                Assert.IsInstanceOfType(expectedUser, typeof(UserDTO));

            }
        }
        /// <summary>
        /// This test method is used to test EditUser function in UserRegistration class
        /// </summary>
        [TestMethod]
        public void UserRegistrationEditUserMethodShouldEditAUserDTOTypeOfList()
        {

            using (var db = new LP_HMSDbEntities())
            {
                // Arrange
                IUserManager usermanager = new UserManager();

                UserDTO userInsertDto = new UserDTO()
                {
                    UserName = "Duminda1",
                    Password = "123",
                    UserRoleId = 1
                };

                //Act
                usermanager.InsertUser(userInsertDto);

                var insertedUserIndex = db.Users.OrderByDescending(u => u.Id).Max(c => c.Id);

                UserDTO userEditDto = new UserDTO()
                {
                    Id =insertedUserIndex,
                    UserName = "Duminda2",
                    Password = "1234",
                    UserRoleId = 1
                };

                usermanager.EditUser(userEditDto);

                var editedUser = usermanager.ViewtUserById(Convert.ToInt32(insertedUserIndex));

                //Assert
                Assert.IsInstanceOfType(editedUser, typeof(UserDTO));

            }
        }
         /// <summary>
        ///  This test method is used to test DeleteUser function in UserRegistration class
         /// </summary>
        [TestMethod]
        public void UserRegistrationDeleteUserMethodShouldDeleteAUserDTOTypeOfList()
        {
            using (var db = new LP_HMSDbEntities())
            {
                // Arrange
                IUserManager usermanager = new UserManager();

                UserDTO userInsertDto = new UserDTO()
                {
                    UserName = "Duminda3",
                    Password = "1236",
                    UserRoleId = 1
                };

                usermanager.InsertUser(userInsertDto);
                //Act
                var lastuser = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();

                //Assert
                usermanager.DeleteUser(lastuser.Id);
            }
        }
        /// <summary>
        ///  This test method is used to test ViewUserById function in UserRegistration class
        /// </summary>
        [TestMethod]
        public void UserRegistrationViewUserByIdMethodShouldReturnAUserDTOTypeOfList()
        {
            using (var db = new LP_HMSDbEntities())
            {
                // Arrange
                IUserManager usermanager = new UserManager();

                UserDTO userInsertDto = new UserDTO()
                {
                    UserName = "Duminda7",
                    Password = "12367",
                    UserRoleId = 1
                };

                //Act
                usermanager.InsertUser(userInsertDto);

                var lastuser = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();

                var selectedUserById=usermanager.ViewtUserById(lastuser.Id);

                //Assert
                Assert.IsInstanceOfType(selectedUserById, typeof(UserDTO));

            }
        }
    #endregion


    }
}
