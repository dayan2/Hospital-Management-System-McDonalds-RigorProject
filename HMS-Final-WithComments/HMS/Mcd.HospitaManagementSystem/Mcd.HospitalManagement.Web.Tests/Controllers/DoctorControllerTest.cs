using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagement.Web.Controllers;
using System.Web.Mvc;
using Mcd.HospitalManagement.Web.Models;
using System.Web.Routing;
using Mcd.HospitalManagementSystem.Data;
using System.Web;

namespace Mcd.HospitalManagement.Web.Tests.Controllers
{
    [TestClass]
    public class DoctorControllerTest
    {
        

        #region FrontPage
        /// <summary>
        /// Taking All Patient Objects to the View
        /// </summary>
        //[TestMethod]
        //public void FrontPageMethodShouldReturnAPatientDoctorModelTypeOfListToTheView()
        //{
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    List<PatientDoctorModel> list = new List<PatientDoctorModel>{
        //                                        new PatientDoctorModel{Id = 11 , Name = "neranjan", Charges = 220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1},
        //                                        new PatientDoctorModel{Id = 91 , Name = "Ewerdney", Charges = 1220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1}
        //                               };
        //    IEnumerable<PatientDoctorModel> clist = list;

        //    mock.Setup(e => e.GetDoctors()).Returns(new IEnumerable<PatientDoctorModel>());

        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (ViewResult)cont.GetDoctors();
        //    var actual = (IEnumerable<DoctorModel>)View.Model;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorModel>));

        //}
        #endregion


        #region GetDoctorsMethod

        /// <summary>
        /// GetAllDoctors List
        /// </summary>
        [TestMethod]
        public void GetDoctorsMethodShouldReturnADoctorModelTypeOfListToTheView()
        {
            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

            List<DoctorRoleDTO> list = new List<DoctorRoleDTO>{
                                                new DoctorRoleDTO{Id = 11 , Name = "neranjan", Charges = 220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1},
                                                new DoctorRoleDTO{Id = 91 , Name = "Ewerdney", Charges = 1220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1}
                                       };
            IEnumerable<DoctorRoleDTO> clist = list;

            mock.Setup(e => e.GetDoctors()).Returns(clist);

            DoctorController cont = new DoctorController(mock.Object);
            var View = (ViewResult)cont.GetDoctors();
            var actual = (IEnumerable<DoctorModel>)View.Model;

            //Verify
            mock.Verify(e => e.GetDoctors() , Times.Once);

            //Assert
            Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorModel>));

        }
        /// <summary>
        /// Calling Doctor/GetDoctors url maps to the GetDoctors action
        /// </summary>
        /// we would need a test to check if Doctor/GetDoctors are called then the action RemoveDoctor is called 
        [TestMethod]
        public void Route_GetDoctors_View_Maps_To_Doctor_Controller_GetDoctors_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces


            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/GetDoctors");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"], "Route should map to the Home controller");
            Assert.AreEqual("GetDoctors", route.Values["action"], "Route should map to the Index action");

        }
        #endregion

        #region UpdateDoctorMethod

        /// <summary>
        /// UpdateDoctor
        /// </summary>
        /// HTTPGet Method
        [TestMethod]
        public void DoctorControllerUpdateDoctorMethodShouldReturnADoctorModel()
        {
            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

            DoctorRoleDTO expectedDoctor = new DoctorRoleDTO { Id = 11, Name = "neranjan", Charges = 220, 
                DoctorSpecialityId = 1, PhoneNo = "", WardId = 1 };

            mock.Setup(x => x.GetDoctorsById(It.IsAny<int>())).Returns(expectedDoctor);

            DoctorController controller = new DoctorController(mock.Object);
            var control = (ViewResult)controller.UpdateDoctor(1);
            var actual = (DoctorModel)control.ViewData.Model;

            //Assert
            Assert.AreEqual(expectedDoctor.Id, actual.Id);
            Assert.AreEqual(expectedDoctor.Name, actual.Name);
        }
        /// <summary>
        /// UpdateDoctor
        /// </summary>
        /// HTTPPostMethod
        //[TestMethod]
        //public void UpdateDoctorMethodShouldVerifyAndRedirectToTheIndex()
        //{
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    DoctorModel expectedDoctor = new DoctorModel { Id = 11, Name = "neranjan", Charges = 220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1 };

        //    mock.Setup(x => x.UpdateDoctor(It.IsAny<DoctorRoleDTO>()));

        //    DoctorController controller = new DoctorController(mock.Object);
        //    var result = (RedirectToRouteResult)controller.UpdateDoctor(expectedDoctor);
        //    //var actual = (DoctorRoleDTO)control.ViewData.Model;

        //    mock.Verify();

        //    //Assert
        //    Assert.AreEqual("GetDoctors", result.RouteValues["action"]);
        //    Assert.AreEqual("Doctor", result.RouteValues["controller"]);
        //}
        [TestMethod]
        public void UpdateDoctorMethodGetDoctorsWithActionParamId()
        {
            //Arrange
            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
            mock.Setup(x => x.GetDoctorsById(It.IsAny<int>())).Returns(new DoctorRoleDTO());

            //Act   
            DoctorController controller = new DoctorController(mock.Object);
            var actionResult = controller.UpdateDoctor(1) as ViewResult;

            //Assert
            mock.Verify(x => x.GetDoctorsById(It.IsAny<int>()), Times.Once());

        }
        /// <summary>
        /// Calling Doctor/UpdateDoctor url maps to the UpdateDoctor action
        /// </summary>
        /// we would need a test to check if Doctor/UpdateDoctor are called then the action RemoveDoctor is called 
        /// checking whether correct view is called or not
        [TestMethod]
        public void Route_Update_View_Maps_To_Doctor_Controller_UpdateDoctor_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces


            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/UpdateDoctor/2");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"]);
            Assert.AreEqual("UpdateDoctor", route.Values["action"]);
            Assert.AreEqual("2", route.Values["id"]);

        }
        #endregion

        #region RemoveDoctorMethod

        /// <summary>
        /// RemoveDoctor
        /// </summary>
        //[TestMethod]
        //public void RemoveDoctorMethodShouldRedirectToTheIndex()
        //{
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.RemoveDoctor(It.IsAny<int>()));

        //    DoctorController controller = new DoctorController(mock.Object);
        //    var result = (RedirectToRouteResult)controller.RemoveDoctor(new DoctorModel());

        //    //Verify
        //    mock.Verify(x => x.RemoveDoctor(It.IsAny<int>()));

        //    //Assert
        //    Assert.AreEqual("GetDoctors", result.RouteValues["action"]);
        //    Assert.AreEqual("Doctor", result.RouteValues["controller"]);
        //}
        [TestMethod]
        public void RemoveDoctorMethodGetDoctorsWithActionParamId()
        {
            //Arrange
            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
            mock.Setup(x => x.GetDoctorsById(It.IsAny<int>())).Returns(new DoctorRoleDTO());


            //Act   
            DoctorController controller = new DoctorController(mock.Object);
            var actionResult = controller.RemoveDoctor(1) as ViewResult;
            //Assert

            mock.Verify(x => x.GetDoctorsById(It.IsAny<int>()), Times.Once());

        }
        /// <summary>
        /// Calling Doctor/RemoveDoctor url maps to the RemoveDoctor action
        /// </summary>
        /// we would need a test to check if Doctor/RemoveDoctor/2 are called then the action RemoveDoctor is called with a parameter 2
        [TestMethod]
        public void Route_Remove_View_Maps_To_Doctor_Controller_RemoveDoctor_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces


            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/RemoveDoctor/2");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"], "Route should map to the Home controller");
            Assert.AreEqual("RemoveDoctor", route.Values["action"], "Route should map to the Index action");
            Assert.AreEqual("2", route.Values["id"], "Route should map to the id parameter");

        }
        #endregion


        #region DetailsMethod
        /// <summary>
        /// DoctorDetails
        /// </summary>
        /// DetailsMethod in DoctorController Should Return A DoctorDetailModel type object To it's View
        [TestMethod]
        public void DetailsMethodShouldReturnADoctorDetailModelToTheView()
        {
            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

            DoctorDetailDTO expectedDoctor = new DoctorDetailDTO { Id = 11, Name = "neranjan", PhoneNo = "", WardId = 1 };

            mock.Setup(x => x.GetDoctorByUserIdForProfileDetail(It.IsAny<int>())).Returns(expectedDoctor);

            DoctorController controller = new DoctorController(mock.Object);
            var control = (ViewResult)controller.Details(1);
            var actual = (DoctorDetailModel)control.ViewData.Model;

            //Verify
            mock.Verify(x => x.GetDoctorByUserIdForProfileDetail(It.IsAny<int>()), Times.Once);

            //Assert
            Assert.AreEqual(11, expectedDoctor.Id);
            Assert.AreEqual("neranjan", expectedDoctor.Name);
        }
        /// <summary>
        /// Calling Doctor/Details url maps to the Details action
        /// </summary>
        /// we would need a test to check if Doctor/Details/2 are called then the action Details is called with a parameter 2
        [TestMethod]
        public void Route_Details_View_Maps_To_Doctor_Controller_Details_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces


            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/Details/2");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"], "Route should map to the Home controller");
            Assert.AreEqual("Details", route.Values["action"], "Route should map to the Index action");
            Assert.AreEqual("2", route.Values["id"], "Route should map to the id parameter");

        }
        #endregion

        #region AddDoctor
        
        /// <summary>
        /// Calling Doctor/AddDoctor url maps to the AddDoctor action
        /// </summary>
        /// we would need a test to check if Doctor/AddDoctor are called then the action Details is called 
        [TestMethod]
        public void Route_AddDoctor_View_Maps_To_Doctor_Controller_AddDoctor_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces


            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/AddDoctor/2");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"], "Route should map to the Home controller");
            Assert.AreEqual("AddDoctor", route.Values["action"], "Route should map to the Index action");

        }
        #endregion


        #region TransferDoctorMethod
        [TestMethod]
        public void TransferDoctorMethodDoctorViewBagPropertyShouldContainValueAndDoctorRecommendationModelTypeOfObjectShouldBeReturnedToTheView()
        {
            // Arrange
            // - create the mock repository
            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

            // Action
            var Controller = new DoctorController(mock.Object);
            var result = Controller.TransferDoctor(1) as ViewResult;
            var actual = (DoctorRecommendationModel)result.Model;

            // Assert
            Assert.IsNotNull(result.ViewBag.doctorViewbag);
            Assert.IsInstanceOfType(actual, typeof(DoctorRecommendationModel));
        }

        [TestMethod]
        public void Route_TransferDoctor_View_Maps_To_Doctor_Controller_TransferDoctor_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces

            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/TransferDoctor/2");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"], "Route should map to the Home controller");
            Assert.AreEqual("TransferDoctor", route.Values["action"], "Route should map to the Index action");

        }
        [TestMethod]
        public void TransferDoctorMethodShouldReturnDoctorModelTypeOfObjectToTheView()
        {

            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

            mock.Setup(e => e.TransferDoctor(It.IsAny<DoctorRecommendationDTO>()));

            DoctorController cont = new DoctorController(mock.Object);
            var result = (RedirectToRouteResult)cont.TransferDoctor(new DoctorRecommendationModel());
            //var actual = (DoctorModel)View.Model;

            ////Verify
            mock.Verify(e => e.TransferDoctor(It.IsAny<DoctorRecommendationDTO>()), Times.Once);

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Doctor", result.RouteValues["controller"]);
        }

        //[TestMethod]
        //public void TransferDoctorMethodShouldRedirectToTheIndex()
        //{
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.AddDoctor(It.IsAny<DoctorRoleDTO>()));

        //    DoctorController controller = new DoctorController(mock.Object);
        //    var result = (RedirectToRouteResult)controller.AddDoctor(new DoctorModel());

        //    //Verify
        //    mock.Verify(e => e.AddDoctor(It.IsAny<DoctorRoleDTO>()), Times.Once);

        //    //Assert
        //    Assert.AreEqual("GetDoctors", result.RouteValues["action"]);
        //    Assert.AreEqual("Doctor", result.RouteValues["controller"]);
        //}
        #endregion

        #region RecommendDoctors

        [TestMethod]
        public void Route_RecommendDoctors_View_Maps_To_Doctor_Controller_RecommendDoctors_Action()
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            Mcd.HospitalManagement.Web.RouteConfig.RegisterRoutes(routes);//namespaces

            var mockHttpContext = new Mock<HttpContextBase>();//buit-in Context type

            mockHttpContext
                .Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Doctor/RecommendDoctors");

            //Act
            RouteData route = routes.GetRouteData(mockHttpContext.Object);

            //Assert
            Assert.AreEqual("Doctor", route.Values["controller"], "Route should map to the Home controller");
            Assert.AreEqual("RecommendDoctors", route.Values["action"], "Route should map to the Index action");

        }
        [TestMethod]
        public void RecommendDoctorsMethodShouldReturnDoctorModelTypeOfListToTheView()
        {

            Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

            mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

            DoctorController cont = new DoctorController(mock.Object);
            var View = (ViewResult)cont.RecommendDoctors();
            var actual = (IEnumerable<DoctorDetailModel>)View.Model;
            //Assert
            Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));

        }
        #endregion

        #region JsonMethods

        //[TestMethod]
        //public void getJsonAllDoctorsMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonAllDoctors();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        //[TestMethod]
        //public void getJsonDentistsMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonDentists();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        //[TestMethod]
        //public void getJsonPhysiciansMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonPhysicians();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        //[TestMethod]
        //public void getJsonSurgeonsMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonSurgeons();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        //[TestMethod]
        //public void getJsonRadiologistsMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonRadiologists();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        //[TestMethod]
        //public void getJsonNeurologistsMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonNeurologists();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        //[TestMethod]
        //public void getJsonNeurosurgeonsMethodShouldReturnJsonResultAndShouldContainDoctorDetailModelTypeOfList()
        //{

        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();
        //    //mock.Setup(e => e.GetDoctors()).Returns(new List<DoctorRoleDTO>());

        //    //Act
        //    DoctorController cont = new DoctorController(mock.Object);
        //    var View = (JsonResult)cont.getJsonNeurosurgeons();
        //    var actual = (IEnumerable<DoctorDetailModel>)View.Data;
        //    //Assert
        //    Assert.IsInstanceOfType(actual, typeof(IEnumerable<DoctorDetailModel>));
        //}
        #endregion

    }
}
