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
    public class PatientAdmissionDetailsUnitTest
    {
        #region Get data through db



        /// <summary>
        /// PatientAdmissionDetails class InsertPatientAdmissionDetails Method should save the PatientAdmission Object in the Database
        /// </summary>
        [TestMethod]
        public void PatientAdmissionDetailsInsertPatientAdmissionDetailsMethodSavePatientAdmissionsObjectViaDBContext()
        {
            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-10-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                Assert.IsInstanceOfType(patientForSelectedIndex, typeof(AllPatientDTO));


                //  patientManager.DeletePatientAdmissionDetails(indexOfPatientDetail);

            }

        }



        /// <summary>
        /// PatientAdmissionDetails class ViewPatientRelatedData Method should return a patient Object to the patient controller
        /// </summary>

        [TestMethod]
        public void ViewPatientAdmissionDetailsByPatientIDMethodReturnsPatientObject()
        {

            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-03-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                Assert.AreEqual(Convert.ToDateTime("2015-03-01"), patientForSelectedIndex.AdmitDate);
                Assert.AreEqual(10, patientForSelectedIndex.PatientId);

            }
        }




        /// <summary>
        /// PatientAdmissionDetails class ViewAllPatientAdmissionDetails Method should return a AllPatientDTO type of list to the controller
        /// </summary>
        [TestMethod]
        public void ViewAllPatientAdmissionDetailsMethodShouldReturnAllPatientDTO()
        {
            IPatientManager patientManager = new PatientManager();


            var patientList = patientManager.ViewAllPatientAdmissionDetails();

            Assert.IsInstanceOfType(patientList, typeof(IEnumerable<AllPatientDTO>));
        }


        /// <summary>
        /// DeletePatientAdmissionDetails Method should remove patientdetails Objects from the Database
        /// </summary>
        [TestMethod]
        public void DeletePatientAdmissionDetailsFromDB()
        {


            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-09-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
                patientManager.DeletePatientAdmissionDetails(indexOfPatientDetail);

            }
        }




        /// <summary>
        /// PatientAdmissionDetails class CheckPatientAdmissonAvilability Method should return a status of discharge
        /// </summary>

        [TestMethod]
        public void CheckCheckPatientAdmissionAvilabiltyMethodCheckDischargeStatus()
        {

            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-03-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);


                Assert.AreEqual(false, patientForSelectedIndex.IsDischarged);

                patientManager.CheckPatientAdmissionAvilabilty(patientForSelectedIndex.PatientId);

            }

        }


        /// <summary>
        /// PatientAdmissionDetails class CheckRelationshipBetweenPatientAndPatientDetails Method should 
        //  return a status of avilability  of patientdetails according to patient id
        /// </summary>

        [TestMethod]
        public void CheckRelationshipBetweenPatientAndPatientDetailsMethod()
        {

            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-03-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);


                Assert.AreEqual(false, patientForSelectedIndex.IsDischarged);

                patientManager.CheckRelationshipBetweenPatientAndPatientDetails(patientForSelectedIndex.PatientId);

            }

        }

        /// <summary>
        /// PatientAdmissionDetails class GetPatientDetailId Method should 
        //  return patientdetailId
        /// </summary>

        [TestMethod]
        public void GetPatientDetailIdAccordingToPatientId()
        {

            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-03-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);


                patientManager.GetPatientDetailId(patientForSelectedIndex.PatientId);

            }

        }



        /// <summary>
        /// PatientAdmissionDetails class ViewDoctorAccordingToPatientId Method should 
        //  return patientdetailId
        /// </summary>

        [TestMethod]
        public void ViewDoctorAccordingToPatientId()
        {

            IPatientManager patientManager = new PatientManager();
            PatientAdmissionDTO patientAdmission = new PatientAdmissionDTO
            {
                AdmitDate = Convert.ToDateTime("2015-03-01"),
                BedId = 2,
                WardId = 7,
                DoctorId = 33,
                PatientId = 10,
                IsDischarged = false

            };

            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);


                patientManager.ViewDoctorAccordingToPatientId(patientForSelectedIndex.PatientId);

            }

        }


        /// <summary>
        /// PatientAdmissionDetails class InsertPatientFeedBack Method should save the PatientFeedack Object in the Database
        /// </summary>
        [TestMethod]
        public void PatientAdmissionDetailsInsertPatientFeedBackMethodSavePatientFeedbackObjectViaDBContext()
        {
            IPatientManager patientManager = new PatientManager();
            PatientFeedbackDTO patientFeedback = new PatientFeedbackDTO
            {
                DoctorId = 33,
                PatientDetailId = 50,
                FeedbackDate = Convert.ToDateTime("2015-03-02"),
                FeedbackDescription = "excellent"

            };

            patientManager.InsertPatientFeedBack(patientFeedback);
            using (var dbContext = new LP_HMSDbEntities())
            {
                var idOfPatientFeedBack = dbContext.PatientFeedbacks.OrderByDescending(u => u.id).Max(c => c.id);

                var feedbackDetailsForId = patientManager.ViewAllPatientFeedbackDetailsAccordingToId(idOfPatientFeedBack);

                Assert.IsInstanceOfType(feedbackDetailsForId, typeof(PatientFeedbackDTO));


                //  patientManager.DeletePatientAdmissionDetails(indexOfPatientDetail);

            }

        }




        #endregion
    }
}
