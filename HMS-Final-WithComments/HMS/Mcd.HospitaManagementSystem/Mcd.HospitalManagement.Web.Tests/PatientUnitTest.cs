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
    public class PatientUnitTest
    {
        #region Get data through db
        /// <summary>
        /// MainPatientDetails class InsertPatient Method should save the Patients Object in the Database
        /// </summary>
        [TestMethod]
        public void MainPatientDetailsInsertPatientMethodSavePatientsObjectViaDBContext()
        {
            IPatientManager patientManager = new PatientManager();
            PatientDTO patients = new PatientDTO
            {
              Name="Piyumi",
              NIC="896621076V",
              Address="Kalutara",
              Gender="female",
              MobileNo="0716574322"
            };

            patientManager.InsertPatient(patients);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatient = dbContext.Patients.OrderByDescending(u => u.Id).Max(c => c.Id);

                var patientForSelectedIndex = patientManager.ViewPatientDetails(Convert.ToInt32(indexOfPatient));

                Assert.IsInstanceOfType(patientForSelectedIndex, typeof(PatientDTO));


                patientManager.Deletepatient(indexOfPatient);
  
            }

        }



        /// <summary>
        /// MainPatientDetails class ViewPatientDetailsByID Method should return a patient Object to the patient controller
        /// </summary>
      
        [TestMethod]
        public void ViewPatientDetailsByIDMethodReturnsPatientObject()
        {

            IPatientManager patientManager = new PatientManager();
            PatientDTO patients = new PatientDTO
            {
                Name = "Piyumi",
                NIC = "896621076V",
                Address = "Kalutara",
                Gender = "female",
                MobileNo = "0716574322"
            };

            patientManager.InsertPatient(patients);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatient = dbContext.Patients.OrderByDescending(u => u.Id).Max(c => c.Id);

                var patientForSelectedIndex = patientManager.ViewPatientDetails(Convert.ToInt32(indexOfPatient));

                Assert.AreEqual("Piyumi", patientForSelectedIndex.Name);
                Assert.AreEqual("896621076V", patientForSelectedIndex.NIC);
                Assert.AreEqual("0716574322", patientForSelectedIndex.MobileNo);

                
            }
        }




        /// <summary>
        /// MainPatientDetails class ViewAllPatientDetails Method should return a PatientDTO type of list to the controller
        /// </summary>
        [TestMethod]
        public void ViewAllPatientDetailsMethodShouldReturnPatientDTO()
        {
            IPatientManager patientManager = new PatientManager();

         
            var patientList = patientManager.ViewAllPatientDetails();

            Assert.IsInstanceOfType(patientList, typeof(IEnumerable<PatientDTO>));
        }


        /// <summary>
        /// Deletepatient Method should remove Doctor Objects from the Database
        /// </summary>
        [TestMethod]
        public void DeletepatientFromDB()
        {

            IPatientManager patientManager = new PatientManager();
            PatientDTO patients = new PatientDTO
            {
                Name = "Piyumi",
                NIC = "896621076V",
                Address = "Kalutara",
                Gender = "female",
                MobileNo = "0716574322"
            };

            patientManager.InsertPatient(patients);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatient = dbContext.Patients.OrderByDescending(u => u.Id).Max(c => c.Id);

                patientManager.Deletepatient(Convert.ToInt32(indexOfPatient));
            }
        }

        /// <summary>
        /// MainPatientDetails class CheckPatientAvilability Method should return a status of avilability
        /// </summary>

        [TestMethod]
        public void CheckPatientAvilabilityMethodCheckStatus()
        {

            IPatientManager patientManager = new PatientManager();
            PatientDTO patients = new PatientDTO
            {
                Name = "Piyumi",
                NIC = "896621076V",
                Address = "Kalutara",
                Gender = "female",
                MobileNo = "0716574322"
            };

            //insert patient to local db
            patientManager.InsertPatient(patients);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get the last index of patient table
                var indexOfPatient = dbContext.Patients.OrderByDescending(u => u.Id).Max(c => c.Id);

                //get the recods of patients
                var patientForSelectedIndex = patientManager.ViewPatientDetails(Convert.ToInt32(indexOfPatient));

                Assert.AreEqual("896621076V", patientForSelectedIndex.NIC);

                //check available patient according to nic
                patientManager.CheckPatientAvilability(patientForSelectedIndex.NIC);
            }
        }




        #endregion



    }
}
