using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using System.Collections.Generic;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class WardUnitTest
    {
        [TestMethod]
        public void WardManagerViewWardDetailsShouldReturnAAllWardDetails()
        {
            IWards wardmanager = new WardManager();

            var expextedWard = wardmanager.ViewWardDetails();

            Assert.IsInstanceOfType(expextedWard, typeof(IEnumerable<WardDTO>));
        }

        [TestMethod]
        public void WardManagerInsertWardShouldInsertWardDtoTypeObject()
        {
            IWards wardmanager = new WardManager();

            WardDTO warddto = new WardDTO()
            {
                WardFee = 1000,
                WardNo = "1000"
            };

            wardmanager.InsertWard(warddto);

            using (var db=new LP_HMSDbEntities())
            {
                var insertedIndexWard = db.Wards.OrderByDescending(x => x.Id).Max(y => y.Id);

                var insertedWard = wardmanager.ViewWardById(insertedIndexWard);

                Assert.IsInstanceOfType(insertedWard, typeof(WardDTO));

            };
           
        }
        [TestMethod]
        public void WardManagerEditWardShouldSendSuccessfullEditedObject()
        {
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

                wardmanager.EditWard(warddto);

                var editedWard = wardmanager.ViewWardById(Convert.ToInt32(insertedWardIndex));

                //Assert
                Assert.IsInstanceOfType(editedWard, typeof(WardDTO));
            }
            

        }
        [TestMethod]
        public void WardManagerInsertWardShouldDeleteWardDetailsById()
        {
            IWards wardmanager = new WardManager();
            WardDTO wardDto = new WardDTO()
            {
                WardFee = 100,
                WardNo = "1200"
            };
            wardmanager.InsertWard(wardDto);

            using (var db = new LP_HMSDbEntities())
            {
                int insertedWardIndex = db.Wards.OrderByDescending(u => u.Id).Max(c => c.Id);

                wardmanager.DeleteWard(insertedWardIndex);
            }
        }

        [TestMethod]
        public void WardManagerViewWardByIdMethodShouldReturnAWardTypeDto()
        {
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
    }
}
