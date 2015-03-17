using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using Mcd.HospitalManagement.Web.Controllers;
using System.Web.Mvc;
using Mcd.HospitalManagement.Web.Models;

namespace Mcd.HospitalManagement.Web.Tests.Controllers
{
    [TestClass]
    public class UserControllerUnitTest
    {
        [TestMethod]
        public void IndexMethodShouldReturnAUserModelTypeOfListToTheView()
        {
            Mock<IUserManager> mock = new Mock<IUserManager>();

            List<UserDTO> lstUserDto = new List<UserDTO>
            {
                new UserDTO{ Id=1, UserName="Duminda", UserRoleId =1 },
                new UserDTO{ Id=2, UserName="Duminda1", UserRoleId =1 }
            };

            IEnumerable<UserDTO> list = lstUserDto;

            mock.Setup(e => e.ViewtAllUser()).Returns(list);

            UserController userControl = new UserController(mock.Object);
            var View = (ViewResult)userControl.Index();
            var actual = (IEnumerable<UserModel>)View.Model;

            //Verify
            mock.Verify(e => e.ViewtAllUser(), Times.Once);

            //Assert
            Assert.IsInstanceOfType(actual, typeof(IEnumerable<UserModel>));


        }

        [TestMethod]
        public void CreateMethodShouldInsertAUserModelTypeOfListToTheView()
        {
            Mock<IUserManager> mock = new Mock<IUserManager>();

            mock.Setup(x => x.InsertUser(It.IsAny<UserDTO>()));

            UserController userControl = new UserController(mock.Object);

            var result = (RedirectToRouteResult)userControl.Create(new UserModel());

            //Verify
            mock.Verify(e => e.InsertUser(It.IsAny<UserDTO>()), Times.Once);

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("User", result.RouteValues["controller"]);

        }

        [TestMethod]
        public void EditMethodShouldEditAUserModelTypeOfListToTheView()
        {
            Mock<IUserManager> mock = new Mock<IUserManager>();

            mock.Setup(x => x.ViewtUserById(It.IsAny<int>())).Returns(new UserDTO());

            //Act   
            UserController controller = new UserController(mock.Object);
            var actionResult = controller.Edit(1) as ViewResult;

            //Assert
            mock.Verify(x => x.ViewtUserById(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void DeleteMethodShouldEditAUserModelTypeOfListToTheView()
        {
            //Mock<IUserManager> mock = new Mock<IUserManager>();

            //mock.Setup(x => x.DeleteUser(It.IsAny<int>()));

            //UserController userController = new UserController(mock.Object);
            ////var result = (RedirectToRouteResult)userController.Delete(new UserModel());

            ////Verify
            //mock.Verify(x => x.DeleteUser(It.IsAny<int>()));

            ////Assert
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.AreEqual("User", result.RouteValues["controller"]);
        }


    }
}
