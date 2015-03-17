using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Security;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class DoctorUnitTest
    {
        #region Mock

        #region AddDoctorMethod


        //[TestMethod]
        //public void DoctorDetailsAddDoctorMethodSavesADoctorObjectViaContext()
        //{
        //    // Arrange
        //    var mockSet = new Mock<DbSet<Doctor>>();

        //    var mockContext = new Mock<LP_HMSDbEntities>();
        //    mockContext.Setup(m => m.Doctors).Returns(mockSet.Object);

        //    // Act
        //    var service = new DoctorDetails(mockContext.Object);
        //    service.AddDoctor(new DoctorRoleDTO() { Id = 11, Name = "neranjan", Charges = 220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1 });

        //    //Verify
        //    mockSet.Verify(m => m.Add(It.IsAny<Doctor>()), Times.Once());
        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}


        #endregion

        #region GetDoctorsMethod

        /// <summary>
        /// GetDoctors Method
        /// </summary>
        /// 
        //[TestMethod]
        //public void DoctorDetailsGetDoctorsMethodRetrieveAllDoctorModels()
        //{
        //    // Arrange
        //    var data = new List<Doctor> 
        //    { 
        //        new Doctor { Name = "Duminda" }, 
        //        new Doctor { Name = "Yohan" }, 
        //        new Doctor { Name = "Piyumi" },
        //        new Doctor { Name = "Neranjan" }
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Doctor>>();
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(data.Provider);
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        //    var mockContext = new Mock<LP_HMSDbEntities>();
        //    mockContext.Setup(c => c.Doctors).Returns(mockSet.Object);

        //    // Act
        //    var service = new DoctorDetails(mockContext.Object);
        //    var doctors = service.GetDoctors();

        //    // Assert
        //    Assert.IsNotNull(doctors);
        //    Assert.AreEqual(4, (int)doctors.Count());
        //    Assert.AreEqual("Duminda", doctors.ElementAt(0).Name.ToString());
        //    Assert.AreEqual("Yohan", doctors.ElementAt(1).Name.ToString());
        //    Assert.AreEqual("Piyumi", doctors.ElementAt(2).Name.ToString());
        //    Assert.AreEqual("Neranjan", doctors.ElementAt(3).Name.ToString());
        //}
        #endregion

        #region GetDoctorsByIdMethod


        //[TestMethod]
        //public void DoctorDetailsGetDoctorsByIdMethodShouldRetrieveDoctorsById()
        //{
        //    // Arrange
        //    var data = new List<Doctor> 
        //    { 
        //        new Doctor { Id = 1 }, 
        //        new Doctor { Id = 2 }, 
        //        new Doctor { Id = 3 }
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Doctor>>();
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(data.Provider);
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        //    var mockContext = new Mock<LP_HMSDbEntities>();
        //    mockContext.Setup(c => c.Doctors).Returns(mockSet.Object);

        //    // Act
        //    var service = new DoctorDetails(mockContext.Object);
        //    //service.AddDoctor(new DoctorRoleDTO() { Id = 11, Name = "neranjan", Charges = 220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1 });
        //    var actual = (DoctorRoleDTO)service.GetDoctorsById(11);

        //    // Assert
        //    Assert.IsNotNull(actual);
        //    //Assert.AreEqual(1, actual.Id);
        //}

        /// <summary>
        /// Testing Through The Real Database
        /// </summary>
        //[TestMethod]
        //public void DoctorDetailsGetDoctorsByIdMethodShouldReturnADoctorObject()
        //{
        //    //Arrange
        //    //Mock<DoctorDetails> mock = new Mock<DoctorDetails>();

        //    //Act
        //    //mock.Setup(e => e.GetDoctorsById(It.IsAny<int>() ));
        //    //doctorDetails.AddDoctor(expectedDoctor);
        //    //var receivedDoctor = doctorDetails.GetDoctorsById(expectedDoctor.Id);



        //    DoctorRoleDTO expectedDoctor = new DoctorRoleDTO { Id = 100, Name = "Dayan", DoctorSpecialityId = 1, Charges = 222000, 
        //                                                        PhoneNo = "0947465", WardId = 1 };


        //    //Act
        //    DoctorDetails doctorDetails = new DoctorDetails();
        //    doctorDetails.AddDoctor(expectedDoctor);
        //    var result = (DoctorRoleDTO)(doctorDetails.GetDoctorsById(1));

        //    //Assert
        //    Assert.AreEqual("Dayan", result.Name);
        //    Assert.AreEqual(222000, result.Charges);

        //    //deleting doctor element from the DB
        //    doctorDetails.RemoveDoctor(expectedDoctor.Id);

        //    //Verify
        //    //mock.Verify(e => e.GetDoctorsById(It.IsAny<int>()), Times.Once);
        //}
        /// <summary>
        /// DoctorDetails class GetDoctorsById Method should return a Doctor Object to the controller
        /// </summary>

        #endregion

        #region RemoveDoctorMethod
        /// <summary>
        /// RemoveDoctor Method
        /// </summary>
        //[TestMethod]
        //public void DoctorDetailsRemoveDoctorMethodRemoveDoctorObjectsByIdFromContext()
        //{
        //    //Arrange
        //    var data = new List<Doctor> 
        //    { 
        //        new Doctor { Id = 1 }, 
        //        new Doctor { Id = 2 }, 
        //        new Doctor { Id = 3 }
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Doctor>>();

        //    var mockContext = new Mock<LP_HMSDbEntities>();

        //    mockSet.As<IQueryable<Doctor>>()
        //     .Setup(x => x.Provider)
        //     .Returns(data.Provider);
        //    mockSet.As<IQueryable<Doctor>>()
        //             .Setup(x => x.ElementType)
        //             .Returns(data.ElementType);
        //    mockSet.As<IQueryable<Doctor>>()
        //             .Setup(x => x.Expression)
        //             .Returns(data.Expression);
        //    mockSet.As<IQueryable<Doctor>>()
        //             .Setup(x => x.GetEnumerator())
        //             .Returns(data.GetEnumerator());

        //    mockContext.Setup(m => m.Doctors).Returns(mockSet.Object);

        //    //Act
        //    var service = new DoctorDetails(mockContext.Object);
        //    service.RemoveDoctor(2);

        //    //Verify
        //    mockSet.Verify(m => m.Remove(It.IsAny<Doctor>()), Times.Once());
        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}
        #endregion

        #region UpdateDoctorMethod

        //[TestMethod]
        //public void DoctorDetailsUpdateDoctorMethodModifyDoctorObjectsFromContext()
        //{
        //    //Arrange
        //    var data = new List<Doctor> 
        //    { 
        //        new Doctor { Id = 1 }, 
        //        new Doctor { Id = 2 }, 
        //        new Doctor { Id = 3 }
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Doctor>>();
        //    var mockContext = new Mock<LP_HMSDbEntities>();

        //    mockSet.As<IQueryable<Doctor>>()
        //     .Setup(x => x.Provider)
        //     .Returns(data.Provider);
        //    mockSet.As<IQueryable<Doctor>>()
        //             .Setup(x => x.ElementType)
        //             .Returns(data.ElementType);
        //    mockSet.As<IQueryable<Doctor>>()
        //             .Setup(x => x.Expression)
        //             .Returns(data.Expression);
        //    mockSet.As<IQueryable<Doctor>>()
        //             .Setup(x => x.GetEnumerator())
        //             .Returns(data.GetEnumerator());

        //    mockContext.Setup(m => m.Entry(It.IsAny<Doctor>()));

        //    //Act
        //    var service = new DoctorDetails(mockContext.Object);
        //    service.UpdateDoctor(new DoctorRoleDTO() { Id = 1, Name = "neranjan", Charges = 220, DoctorSpecialityId = 1, PhoneNo = "", WardId = 1 });

        //    //Verify
        //    mockSet.Verify(m => m.Entry(new Doctor()), Times.Once());

        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}

        /// <summary>
        /// Testing Through the Real Database
        /// </summary>
        //[TestMethod]
        //public void DoctorDetailsUpdateDoctorMethodModifyDoctorObjectsFromContext()
        //{
        //    //Arrange
        //    DoctorDetails doctorDetails = new DoctorDetails();
        //    DoctorRoleDTO doctorDTO = new DoctorRoleDTO { Id=1, Name="",DoctorSpecialityId=1, Charges=22000, PhoneNo="0947465", WardId = 1};
        //    DoctorRoleDTO expectedDoctor = new DoctorRoleDTO { Id = 1, Name = "Dayan", DoctorSpecialityId = 1, Charges = 222000, PhoneNo = "0947465", WardId = 1 };

        //    //Act
        //    doctorDetails.AddDoctor(doctorDTO);
        //    doctorDetails.UpdateDoctor(expectedDoctor);
        //    var modifiedDoctor = doctorDetails.GetDoctorsById(expectedDoctor.Id);

        //    //Assert
        //    Assert.AreEqual("Dayan", modifiedDoctor.Name);
        //    Assert.AreEqual(222000, modifiedDoctor.Charges);

        //    //deleting doctor element from the DB
        //    doctorDetails.RemoveDoctor(expectedDoctor.Id);
        //}
        #endregion

        #region TransferDoctor

        //[TestMethod]
        //public void DoctorDetailsTransferDoctorMethodShouldAddDoctorRecommendationDataInToTheDB()
        //{
        //    // Arrange
        //    var mockSet = new Mock<DbSet<DoctorRecomendation>>();

        //    var mockContext = new Mock<LP_HMSDbEntities>();
        //    mockContext.Setup(m => m.DoctorRecomendations).Returns(mockSet.Object);

        //    // Act
        //    var service = new DoctorDetails(mockContext.Object);
        //    service.TransferDoctor(new DoctorRecommendationDTO() );

        //    //Verify
        //    mockSet.Verify(m => m.Add(It.IsAny<DoctorRecomendation>()), Times.Once());
        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}
        #endregion

        #endregion//Mock-End

        #region Exception Handling
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsGetDoctorsMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.GetDoctors()).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    var doctorList = (IEnumerable<DoctorRoleDTO>)manager.GetDoctors();

        //    //Verify
        //    mock.Verify(x => x.GetDoctors(), Times.Once());

        //    //Assert
        //    Assert.Fail("", typeof(System.Exception));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsGetDoctorsByIdMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    //mock.Setup(x => x.GetDoctorsById(It.IsAny<int>())).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    var doctorList = (IEnumerable<DoctorRoleDTO>)manager.GetDoctorsById(It.IsAny<int>());

        //    //Verify
        //    mock.Verify(x => x.GetDoctorsById(It.IsAny<int>()), Times.Once());

        //    //Assert
        //    Assert.Fail("", typeof(System.Exception));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsAddDoctorMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    //mock.Setup(x => x.AddDoctor(It.IsAny<DoctorRoleDTO>())).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    manager.AddDoctor(new DoctorRoleDTO());

        //    //Verify
        //    mock.Verify(x => x.AddDoctor(It.IsAny<DoctorRoleDTO>()), Times.Once());

        //    //Assert
        //    Assert.Fail("", typeof(System.Exception));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsRemoveDoctorMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.RemoveDoctor(It.IsAny<int>())).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    manager.RemoveDoctor(It.IsAny<int>());

        //    //Verify
        //    mock.Verify(x => x.RemoveDoctor(It.IsAny<int>()), Times.Once());

        //    //Assert
        //    Assert.Fail("Faild To Remove Doctor element", typeof(System.Exception));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsUpdateDoctorMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.UpdateDoctor(It.IsAny<DoctorRoleDTO>())).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    manager.UpdateDoctor(It.IsAny<DoctorRoleDTO>());

        //    //Verify
        //    mock.Verify(x => x.UpdateDoctor(It.IsAny<DoctorRoleDTO>()), Times.Once());

        //    //Assert
        //    Assert.Fail("", typeof(System.Exception));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsTransferDoctorMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.TransferDoctor(It.IsAny<DoctorRecommendationDTO>())).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    manager.TransferDoctor(It.IsAny<DoctorRecommendationDTO>());

        //    //Verify
        //    mock.Verify(x => x.TransferDoctor(It.IsAny<DoctorRecommendationDTO>()), Times.Once());

        //    //Assert
        //    Assert.Fail("Faild To Transfer Patient", typeof(System.Exception));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsGetAllDoctorSpecialityTypesMethodShouldRaisedAnException()
        //{
           
        //        //Arrange
        //        var mock = new Mock<DbSet<DoctorSpeciality>>();
        //        var mockContext = new Mock<LP_HMSDbEntities>();

        //        mockContext.Setup(x => x.DoctorSpecialities).Throws(new System.Exception());

        //        //Act
        //        DoctorDetails manager = new DoctorDetails();
        //        var doctorList = manager.GetAllDoctorSpecialityTypes();

                
        //        //Verify
        //        //mockContext.Verify(x => x.DoctorSpecialities, Times.Once());

        //        //Assert
        //        Assert.Fail("", typeof(System.Exception));
           
        //}
        //[TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        //public void DoctorDetailsGetDoctorByDoctorIdWithSpecializeAreaForProfileDetailMethodShouldRaisedAnException()
        //{
        //    //Arrange
        //    Mock<IDoctorManager> mock = new Mock<IDoctorManager>();

        //    mock.Setup(x => x.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(It.IsAny<int>())).Throws(new System.Exception());

        //    //Act
        //    DoctorDetails manager = new DoctorDetails();
        //    var doctorList = (IEnumerable<DoctorSpecialityDTO>)manager.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(It.IsAny<int>());

        //    //Verify
        //    mock.Verify(x => x.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(It.IsAny<int>()), Times.Once());

        //    //Assert
        //    Assert.Fail("", typeof(System.Exception));
        //}
        #endregion

        #region Through DB

        /// <summary>
        /// DoctorDetails class GetDoctors Method should return a DoctorRoleDTO type of list to the controller
        /// </summary>
        [TestMethod]
        public void DoctorDetailsGetDoctorsMethodShouldReturnADoctorRoleDTOTypeOfList()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();

            //Act
            var expectedDoctorList = manager.GetDoctors();

            //Assert
            Assert.IsInstanceOfType(expectedDoctorList, typeof(IEnumerable<DoctorRoleDTO>));
        }

        /// <summary>
        /// DoctorDetails class GetDoctorsById Method should return a Doctor Object to the controller
        /// </summary>
        [TestMethod]
        public void DoctorDetailsGetDoctorsByIdMethodShouldReturnADoctorObject()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();
            DoctorRoleDTO doctor = new DoctorRoleDTO
            {
                Name = "Neranjan Mendis",
                PhoneNo = "0786950333",
                UserId = 24,
                WardId = 1,
                Charges = 600,
                DoctorSpecialityId = 2
            };

            // Act
            manager.AddDoctor(doctor);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastDoctorIndex = db.Doctors.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedDoctor = manager.GetDoctorsById(Convert.ToInt32(lastDoctorIndex ));

                //Assert
                Assert.AreEqual("Neranjan Mendis", expectedDoctor.Name);
                Assert.AreEqual(600, expectedDoctor.Charges);

                //Deleting the testing Doctor object
                manager.RemoveDoctor(Convert.ToInt32(lastDoctorIndex));
            }
        }


        /// <summary>
        /// DoctorDetails class AddDoctor Method should save the Doctor Object in the Database
        /// </summary>
        [TestMethod]
        public void DoctorDetailsAddDoctorMethodSavesADoctorObjectViaContext()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();
            DoctorRoleDTO doctor = new DoctorRoleDTO
            {
                Name = "Neranjan Mendis",
                PhoneNo = "0786950333",
                UserId = 24,
                WardId = 1,
                Charges = 600,
                DoctorSpecialityId = 2
            };

            // Act
            manager.AddDoctor(doctor);
            using (var db = new LP_HMSDbEntities())
            {
                var lastDoctorIndex = db.Doctors.OrderByDescending(u => u.Id).Max(c=> c.Id);

                var expectedDoctor = manager.GetDoctorsById(Convert.ToInt32(lastDoctorIndex));

                //Assert
                Assert.IsInstanceOfType(expectedDoctor, typeof(DoctorRoleDTO));

                //Deleting the testing Doctor object
                manager.RemoveDoctor(Convert.ToInt32(lastDoctorIndex));
            }

        }

        /// <summary>
        /// DoctorDetails class RemoveDoctor Method should remove Doctor Objects from the Database
        /// </summary>
        [TestMethod]
        public void DoctorDetailsRemoveDoctorMethodShouldRemoveObjectsFromDB()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();
            DoctorRoleDTO doctor = new DoctorRoleDTO
            {
                Name = "Neranjan Mendis",
                PhoneNo = "0786950333",
                UserId = 24,
                WardId = 1,
                Charges = 600,
                DoctorSpecialityId = 2
            };

            // Act
            manager.AddDoctor(doctor);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                //var lastDoctor = db.Doctors.Max();
                var lastDoctor = db.Doctors.OrderByDescending(u => u.Id).FirstOrDefault();

                //Deleting the testing Doctor object
                manager.RemoveDoctor(Convert.ToInt32(lastDoctor.Id));
            }
        }

        /// <summary>
        /// DoctorDetails class UpdateDoctor Method should modify doctor details
        /// </summary>
        //[TestMethod]
        //public void DoctorDetailsUpdateDoctorMethodShouldModifyDoctorsDetails()
        //{
        //    // Arrange
        //    IDoctorManager manager = new DoctorManager();
        //    DoctorRoleDTO doctor = new DoctorRoleDTO
        //    {
        //        Name = "Neranjan Mendis",
        //        PhoneNo = "0786950333",
        //        UserId = 24,
        //        WardId = 1,
        //        Charges = 600,
        //        DoctorSpecialityId = 2
        //    };            

        //    // Act
        //    manager.AddDoctor(doctor);
        //    using (LP_HMSDbEntities db = new LP_HMSDbEntities())
        //    {
        //        //var lastDoctor = db.Doctors.Max();
        //        var lastDoctorIndex = db.Doctors.OrderByDescending(u => u.Id).FirstOrDefault();

        //        DoctorRoleDTO modifiedDoctor = new DoctorRoleDTO
        //        {
        //            Name = "Neranjan Mendis",
        //            PhoneNo = "0786950333",
        //            UserId = 24,
        //            WardId = 1,
        //            Charges = 400,
        //            DoctorSpecialityId = 2
        //        };
        //        manager.UpdateDoctor(modifiedDoctor);
        //        var actualDoctor = manager.GetDoctorsById(Convert.ToInt32(lastDoctorIndex));

        //        //Assert
        //        Assert.IsInstanceOfType(actualDoctor, typeof(DoctorRoleDTO));
        //        Assert.AreEqual("Neranjan Mendis", actualDoctor.Name);

        //        //Deleting the testing Doctor object
        //        manager.RemoveDoctor(Convert.ToInt32(lastDoctorIndex));
        //    }

        //}

        /// <summary>
        /// DoctorDetails class TransferDoctor Method should Save a DoctorRecommendation Object into DB
        /// </summary>
        [TestMethod]
        public void DoctorDetailsTransferDoctorMethodShouldSaveDoctorRecommendationDTOTypeObjectInDB()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();
            DoctorRecommendationDTO doctor = new DoctorRecommendationDTO
            {
                Reason = "Doesn't have Insurance",
                CurrentDoctorId = 44,
                RecomendedDoctorId = 45
            };

            // Act
            manager.TransferDoctor(doctor);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastRecommendationIndex = db.DoctorRecomendations.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedDoctor = db.DoctorRecomendations.Find(lastRecommendationIndex);

                //Assert
                Assert.AreEqual("Doesn't have Insurance", expectedDoctor.Reason);
                Assert.AreEqual(44, expectedDoctor.CurrentDoctorId);

                //Deleting the testing DoctorRecommendation object
                var actualDoctor = db.DoctorRecomendations.Find(lastRecommendationIndex);
                db.DoctorRecomendations.Remove(actualDoctor);
            }
        }

        /// <summary>
        /// DoctorDetails class GetAllDoctorSpecialityTypes Method should Return a DoctorSpeciality type of list to the controller
        /// </summary>
        [TestMethod]
        public void DoctorDetailsGetAllDoctorSpecialityTypesMethodShouldReturnADoctorSpecialityDTOTypeOfList()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();
            
            //Act
            var specialityList = manager.GetAllDoctorSpecialityTypes();

            //Assert
            Assert.IsInstanceOfType(specialityList, typeof(IEnumerable<DoctorSpecialityDTO>));
        }


        /// <summary>
        /// DoctorDetails class GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail Method should return a DoctorDetailDTO Object to the controller
        /// </summary>
        [TestMethod]
        public void DoctorDetailsGetDoctorByDoctorIdWithSpecializeAreaForProfileDetailMethodShouldReturnADoctorDetailDTOObject()
        {
            // Arrange
            IDoctorManager manager = new DoctorManager();            

            // Act
            var doctor = manager.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(30);
            
            //Assert
            Assert.IsInstanceOfType(doctor, typeof(DoctorDetailDTO));
        }


        #endregion


    }
}
