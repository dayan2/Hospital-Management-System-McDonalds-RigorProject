using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagementSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class PatientMedicalTest
    {
        /// <summary>
        /// PatientMedicalTestAllocation class ViewtPatientMedicalTestById Method should return a PatientMedicalTest Object to the controller
        /// </summary>
        [TestMethod]
        public void PatientMedicalTestDetailsGetPatientMedicalByIdMethodShouldReturnPatientMedicalTestObject()
        {
            IPatientMedicalTestAllocation patientMedical = new PatientMedicalTestAllocation();
            PatientMedicalTestDTO patientMedicalDto = new PatientMedicalTestDTO
            {
                MedicalTestId = 4,
                NurseId = 10,
                PatientDetailId = 18,
                PatientName = "kasuni",
                NurseName = "Piyumi",
                MedicalTest = "Blood"

            };
            patientMedical.InsertPatientMedicalTest(patientMedicalDto);
           
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastPatientMedicalTestIndex = db.PatientMedicalTests.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedPatientMedicalTest = patientMedical.ViewtPatientMedicalTestById(Convert.ToInt32(lastPatientMedicalTestIndex));

                //Assert
                Assert.AreEqual("Blood", expectedPatientMedicalTest.MedicalTest);
                Assert.AreEqual("kasuni", expectedPatientMedicalTest.PatientName);

                //Deleting the testing PatientMedicalTest object
                patientMedical.DeletePatientMedicalTest(Convert.ToInt32(lastPatientMedicalTestIndex));
            }
        }

        /// <summary>
        ///  PatientMedicalTestAllocation class InsertPatientMedicalTest Method should save the PatientMedicalTest Object in the Database
        /// </summary>
        [TestMethod]
        public void PatientMedicalTestDetailsInsertPatientMedicalTestMethodSavesAPatientMedicalTestObjectViaContext()
        {
            // Arrange
            IPatientMedicalTestAllocation patientMedical = new PatientMedicalTestAllocation();
            PatientMedicalTestDTO patientMedicalDto = new PatientMedicalTestDTO
            {
                MedicalTestId = 4,
                NurseId = 10,
                PatientDetailId = 18,
                PatientName = "kasuni",
                NurseName = "Piyumi",
                MedicalTest = "Blood"

            };

            // Act
            patientMedical.InsertPatientMedicalTest(patientMedicalDto);
            using (var db = new LP_HMSDbEntities())
            {
                var lastPatientMedicalTestIndex = db.PatientMedicalTests.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedPatientMedicalTest = patientMedical.ViewtPatientMedicalTestById(Convert.ToInt32(lastPatientMedicalTestIndex));

                //Assert
                Assert.IsInstanceOfType(expectedPatientMedicalTest, typeof(PatientMedicalTestDTO));

                //Deleting the testing PatientMedicalTest object
                patientMedical.DeletePatientMedicalTest(Convert.ToInt32(lastPatientMedicalTestIndex));
            }

        }

        /// <summary>
        /// PatientMedicalTestAllocation class DeletePatientMedicalTest Method should remove PatientMedicalTest Objects from the Database
        /// </summary>
        [TestMethod]
        public void PatientMedicalTestDetailsRemovePatientMedicalTestMethodShouldRemoveObjectsFromDB()
        {
            // Arrange
            IPatientMedicalTestAllocation patientMedical = new PatientMedicalTestAllocation();
            PatientMedicalTestDTO patientMedicalDto = new PatientMedicalTestDTO
            {
                MedicalTestId = 4,
                NurseId = 10,
                PatientDetailId = 18,
                PatientName = "kasuni",
                NurseName = "Piyumi",
                MedicalTest = "Blood"

            };

            // Act
            patientMedical.InsertPatientMedicalTest(patientMedicalDto);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                //get last index
                var lastpatientMedical = db.PatientMedicalTests.OrderByDescending(u => u.Id).FirstOrDefault();

                //Deleting the testing Doctor object
                patientMedical.DeletePatientMedicalTest(Convert.ToInt32(lastpatientMedical.Id));
            }
        }
    }
}
