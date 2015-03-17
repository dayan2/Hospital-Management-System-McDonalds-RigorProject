using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class MedicalTestUnitTest
    {
        /// <summary>
        /// MedicalTest class ViewtMedicalTestById Method should return a MedicalTest Object to the controller
        /// </summary>
        [TestMethod]
        public void MedicalTestDetailsGetMedicalTestByIdMethodShouldReturnMedicalTestObject()
        {
            HMSMedicalTestType medicalTest = new HMSMedicalTestType();
            MedicalTestTypeDTO medicalTestDto = new MedicalTestTypeDTO
            {
                Description="Pressure"

            };
            medicalTest.InsertMedicalTestType(medicalTestDto);

            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastMedicalTestIndex = db.MedicalTestTypes.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedMedicalTest = medicalTest.ViewtMedicalTestTypeById(Convert.ToInt32(lastMedicalTestIndex));

                //Assert
                Assert.AreEqual("Pressure", expectedMedicalTest.Description);

                //Deleting the testing MedicalTest object
                medicalTest.DeleteMedicalTestType(Convert.ToInt32(lastMedicalTestIndex));
            }
        }

        /// <summary>
        ///  HMSMedicalTest class InsertMedicalTest Method should save the MedicalTest Object in the Database
        /// </summary>
        [TestMethod]
        public void MedicalTestDetailsInsertMedicalTestMethodSavesAMedicalTestObjectViaContext()
        {
            // Arrange
            HMSMedicalTestType medicalTest = new HMSMedicalTestType();
            MedicalTestTypeDTO medicalTestDto = new MedicalTestTypeDTO
            {
                Description = "Pressure"

            };


            //act
            medicalTest.InsertMedicalTestType(medicalTestDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastMedicalTestIndex = db.PatientMedicalTests.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedMedicalTest = medicalTest.ViewtMedicalTestTypeById(Convert.ToInt32(lastMedicalTestIndex));

                //Assert
                Assert.IsInstanceOfType(expectedMedicalTest, typeof(MedicalTestTypeDTO));

                //Deleting the testing MedicalTest object
                medicalTest.DeleteMedicalTestType(Convert.ToInt32(lastMedicalTestIndex));
            }

        }

        /// <summary>
        /// PatientMedicalTestAllocation class DeletePatientMedicalTest Method should remove PatientMedicalTest Objects from the Database
        /// </summary>
        [TestMethod]
        public void MedicalTestDetailsDeleteMedicalTestMethodShouldRemoveObjectsFromDB()
        {
            // Arrange
            HMSMedicalTestType medicalTest = new HMSMedicalTestType();
            MedicalTestTypeDTO medicalTestDto = new MedicalTestTypeDTO
            {
                Description = "Pressure"
            };

            // Act
            medicalTest.InsertMedicalTestType(medicalTestDto);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastInvocie = db.MedicalTestTypes.OrderByDescending(u => u.Id).FirstOrDefault();

                //Deleting the testing Doctor object
                medicalTest.DeleteMedicalTestType(Convert.ToInt32(lastInvocie.Id));
            }
        }
        
    }
}
