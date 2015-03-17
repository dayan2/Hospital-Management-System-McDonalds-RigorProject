using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{

    #region Public methods for testing Ward manager functions through local db
   
    [TestClass]
    public class WardUnitTest
    {
        /// <summary>
        ///  This test method is used to test ViewWardDetails function in WardManager class 
        /// </summary>
        [TestMethod]
        public void WardManagerViewWardDetailsShouldReturnAAllWardDetails()
        {
            //Arrange
            IWards wardmanager = new WardManager();
            //Act
            var expextedWard = wardmanager.ViewWardDetails();
            //Assert
            Assert.IsInstanceOfType(expextedWard, typeof(IEnumerable<WardDTO>));
        }
        /// <summary>
        ///  This test method is used to test InsertWard function in WardManager class 
        /// </summary>
        [TestMethod]
        public void WardManagerInsertWardShouldInsertWardDtoTypeObject()
        {
            //Arrange
            IWards wardmanager = new WardManager();

            WardDTO warddto = new WardDTO()
            {
                WardFee = 1000,
                WardNo = "1000"
            };
            //Act
            wardmanager.InsertWard(warddto);

            using (var db=new LP_HMSDbEntities())
            {
                var insertedIndexWard = db.Wards.OrderByDescending(x => x.Id).Max(y => y.Id);

                var insertedWard = wardmanager.ViewWardById(insertedIndexWard);
                //Assert
                Assert.IsInstanceOfType(insertedWard, typeof(WardDTO));

            };
           
        }
        /// <summary>
        ///  This test method is used to test EditWard function in WardManager class 
        /// </summary>
        [TestMethod]
        public void WardManagerEditWardShouldSendSuccessfullEditedObject()
        {
            //Arrange
            IWards wardmanager = new WardManager();
            WardDTO wardDto = new WardDTO()
            {
                WardFee =100,
                WardNo ="1200"
            };

            wardmanager.InsertWard(wardDto);

            using(var db= new LP_HMSDbEntities())
            {
               
                var insertedWardIndex = db.Wards.OrderByDescending(u => u.Id).Max(c => c.Id);

                WardDTO warddto = new WardDTO()
                {
                    Id =insertedWardIndex,
                    WardFee = 1000,
                    WardNo = "Test"
                };
                //Act
                wardmanager.EditWard(warddto);

                var editedWard = wardmanager.ViewWardById(Convert.ToInt32(insertedWardIndex));

                //Assert
                Assert.IsInstanceOfType(editedWard, typeof(WardDTO));
            }
            

        }
        /// <summary>
        /// This test method is used to test InsertWard function in WardManager class 
        /// </summary>
        [TestMethod]
        public void WardManagerInsertWardShouldDeleteWardDetailsById()
        {
            //Arrange
            IWards wardmanager = new WardManager();
            WardDTO wardDto = new WardDTO()
            {
                WardFee = 100,
                WardNo = "1200"
            };
            //Act
            wardmanager.InsertWard(wardDto);

            using (var db = new LP_HMSDbEntities())
            {
                int insertedWardIndex = db.Wards.OrderByDescending(u => u.Id).Max(c => c.Id);

                //Assert
                wardmanager.DeleteWard(insertedWardIndex);
            }
        }
        /// <summary>
        /// This test method is used to test ViewWardById function in WardManager class 
        /// </summary>
        [TestMethod]
        public void WardManagerViewWardByIdMethodShouldReturnAWardTypeDto()
        {
            //Arrange
            IWards wardmanager = new WardManager();

            WardDTO wardDto = new WardDTO()
            {
                WardFee = 100,
                WardNo = "1200"
            };

            //Act
            wardmanager.InsertWard(wardDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastward = db.Wards.OrderByDescending(u => u.Id).FirstOrDefault();

                //Assert
                var selectedWard = wardmanager.ViewWardById(lastward.Id);

                Assert.IsInstanceOfType(selectedWard, typeof(WardDTO));
            }
        }

        #endregion
    }
}
