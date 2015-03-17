using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class NurseUnitTest
    {

        /// <summary>
        /// Nurse class ViewtNurseById Method should return a Nurse Object to the controller
        /// </summary>
        [TestMethod]
        public void NurseDetailsGetNurseByIdMethodShouldReturnNurseObject()
        {
            HMSNurse nurse = new HMSNurse();
            NurseDTO nurseDto = new NurseDTO
            {
                Name = "Minoli",
                WardId = 7

            };
            nurse.InsertNurse(nurseDto);

            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastNurseIndex = db.Nurses.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedNurse = nurse.ViewtNurseById(Convert.ToInt32(lastNurseIndex));

                //Assert
                Assert.AreEqual(7, expectedNurse.WardId);
                Assert.AreEqual("Minoli", expectedNurse.Name);

                //Deleting the testing Nurse object
                nurse.DeleteNurse(Convert.ToInt32(lastNurseIndex));
            }
        }

        /// <summary>
        ///  HMSNurse class InsertNurse Method should save the Nurse Object in the Database
        /// </summary>
        [TestMethod]
        public void NurseDetailsInsertNurseMethodSavesANurseObjectViaContext()
        {
            // Arrange
            HMSNurse nurse = new HMSNurse();
            NurseDTO nurseDto = new NurseDTO
            {
                Name = "Minoli",
                WardId = 7

            };
           

            //act
            nurse.InsertNurse(nurseDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastNurseIndex = db.Nurses.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedNurse = nurse.ViewtNurseById(Convert.ToInt32(lastNurseIndex));

                //Assert
                Assert.IsInstanceOfType(expectedNurse, typeof(NurseDTO));

                //Deleting the testing Nurse object
                nurse.DeleteNurse(Convert.ToInt32(lastNurseIndex));
            }

        }

        /// <summary>
        /// PatientMedicalTestAllocation class DeletePatientMedicalTest Method should remove PatientMedicalTest Objects from the Database
        /// </summary>
        [TestMethod]
        public void NurseDetailsDeleteNurseMethodShouldRemoveObjectsFromDB()
        {
            // Arrange
            HMSNurse nurse = new HMSNurse();
            NurseDTO nurseDto = new NurseDTO
            {
                Name = "Minoli",
                WardId = 7

            };

            // Act
            nurse.InsertNurse(nurseDto);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                //var lastDoctor = db.Doctors.Max();
                var lastInvocie = db.Nurses.OrderByDescending(u => u.Id).FirstOrDefault();

                //Deleting the testing Doctor object
                nurse.DeleteNurse(Convert.ToInt32(lastInvocie.Id));
            }
        }
    }
}
