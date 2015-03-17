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

            //insert patient admission details to patientdetails table in db
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get the last index of patientdetails table
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);

                //view all admission details according to patient detail id(according to last index)
                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                //compare the equlity
                Assert.IsInstanceOfType(patientForSelectedIndex, typeof(AllPatientDTO));


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

            //insert patientadmission details to patientdetails table
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get last index of patient details table
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
                //view all patientadmission details according to patientdetailsId(according to last index)
                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                //compaire the objects
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

            //view all patient admissio details
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

            //insert patient admission details to patientdetails table
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get the last index of patientdetails
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
                //delete patient details according to patientdetailid(according to last index)
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
            //insert patientadmission details to patientdetails table
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get the last index
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
                //get patient admission details according to id
                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                //check the discharge status for last index is equal to false
                Assert.AreEqual(false, patientForSelectedIndex.IsDischarged);

                //check the patient is already admited or not
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

            //insert patient admission details to patientdetails
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get last index
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
                //view details according id
                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);


                Assert.AreEqual(false, patientForSelectedIndex.IsDischarged);

                //check the patient has admission
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

            //insert patient admission details
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get last indes of patient admission
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
                //view patient details according to id
                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                //get patient detail id according patient id
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

            //insert patient admission details
            patientManager.InsertPatientAdmissionDetails(patientAdmission);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get last index of patientdetails table
                var indexOfPatientDetail = dbContext.PatientDetails.OrderByDescending(u => u.PatientDetailId).Max(c => c.PatientDetailId);
               //view patient related data according to patientid
                var patientForSelectedIndex = patientManager.ViewPatientRelatedData(indexOfPatientDetail);

                //view doctor details according to patient id
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

            //insert patient feedback to patientfeedbacks table
            patientManager.InsertPatientFeedBack(patientFeedback);
            using (var dbContext = new LP_HMSDbEntities())
            {
                //get last index of patient feedback table
                var idOfPatientFeedBack = dbContext.PatientFeedbacks.OrderByDescending(u => u.id).Max(c => c.id);

                //view feedback details according to patientfeedback id
                var feedbackDetailsForId = patientManager.ViewAllPatientFeedbackDetailsAccordingToId(idOfPatientFeedBack);

                Assert.IsInstanceOfType(feedbackDetailsForId, typeof(PatientFeedbackDTO));


            }

        }




        #endregion
    }
}
