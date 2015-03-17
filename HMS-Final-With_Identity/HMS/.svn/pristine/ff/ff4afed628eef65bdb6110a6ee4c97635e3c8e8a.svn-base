#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcd.HospitalManagementSystem.Data;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class PatientManager : IPatientManager
    {
        #region private Variable
        private MainPatientDetails patient;
        private PatientAdmissionDetails patientAdmission;
        #endregion


        #region constructor
        public PatientManager()
        {
            patient = new MainPatientDetails();
            patientAdmission = new PatientAdmissionDetails();

        }
        #endregion

        #region public method
        public bool InsertPatient(PatientDTO PatientDTO)
        {
            return patient.InsertPatient(PatientDTO);
        }


        /// <summary>
        /// Retrieving patient Details From the MainPatientDetails class
        /// </summary>
        /// <returns>
        /// patient id is returning from the method containing all MainPatientDetails
        /// </returns>
        public PatientDTO ViewPatientDetails(int? PatientID)
        {
            return patient.ViewPatientDetails(PatientID);
        }



        /// <summary>
        /// view all patient details From the MainPatientDetails class
        /// </summary>
        /// <returns>patient list</returns>
        public IEnumerable<PatientDTO> ViewAllPatientDetails()
        {
            return patient.ViewAllPatientDetails();
        }



        /// <summary>
        /// edit patient detais From the MainPatientDetails class
        /// </summary>
        /// <param name="PatientDTO"></param>
        public bool EditPatient(PatientDTO PatientDTO)
        {
            return patient.EditPatient(PatientDTO);
        }


        /// <summary>
        /// delete patient From the MainPatientDetails class
        /// </summary>
        /// <param name="PatientId"></param>
        public bool Deletepatient(int? PatientId)
        {
            return patient.Deletepatient(PatientId);
        }


        /// <summary>
        /// fill  all patients from PatientAdmissionDetails class
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientDTO> fillPatients()
        {
            return patientAdmission.FillPatients();
        }


        /// <summary>
        /// insert patient admission detaios from from PatientAdmissionDetails class
        /// </summary>
        /// <param name="PatientAdmissionDTO"></param>
        public bool InsertPatientAdmissionDetails(PatientAdmissionDTO PatientAdmissionDTO)
        {
            return patientAdmission.InsertPatientAdmissionDetails(PatientAdmissionDTO);
        }


        /// <summary>
        /// edit patient admission details from from PatientAdmissionDetails class
        /// </summary>
        /// <param name="PatientAdmissionDTO"></param>
        public bool EditPatientAdmissionDetails(AllPatientDTO PatientAdmissionDTO)
        {
            return patientAdmission.EditPatientAdmissionDetails(PatientAdmissionDTO);
        }



        /// <summary>
        /// view patient admission detail according to  from from PatientAdmissionDetails class
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns>return patient according to patient id</returns>
        public PatientAdmissionDTO ViewPatientAdmissionDetails(int? PatientID)
        {
            return patientAdmission.ViewPatientAdmissionDetails(PatientID);
        }


        /// <summary>
        /// view all patient admission details from PatientAdmissionDetails class
        /// </summary>
        /// <returns>return all admission details of patients</returns>
        public IEnumerable<PatientAdmissionDTO> viewAllAdmissionPatientDetails()
        {
            return patientAdmission.ViewAllAdmissionPatientDetails();
        }


        /// <summary>
        /// delete patientAddmission Details from PatientAdmissionDetails class
        /// </summary>
        /// <param name="PatientId"></param>
        public bool DeletePatientAdmissionDetails(int? PatientId)
        {
            return patientAdmission.DeletePatientAdmissionDetails(PatientId);
        }

        /// <summary>
        /// view all patient admission details accoring to patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>return patient admission details for patient id</returns>
        public AllPatientDTO ViewPatientRelatedData(int? patientId)
        {
            return patientAdmission.viewPatientRelatedData(patientId);
        }


        /// <summary>
        /// view all patient admission details
        /// </summary>
        /// <returns>retun all admission details for all patients</returns>
        public IEnumerable<AllPatientDTO> ViewAllPatientAdmissionDetails()
        {
            return patientAdmission.ViewAllPatientAdmissionDetails();
        }


        /// <summary>
        /// 
        ///check patient discharge status according to patient id from PatientAdmissionDetails class
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns>status of the availabilty of patientAdmission</returns>
        public bool CheckPatientAdmissionAvilabilty(int? patientID)
        {
            return patientAdmission.CheckPatientAdmissionAvilabilty(patientID);
        }

        /// <summary>
        /// check patient availability according to nic from MainPatientDetails class
        /// </summary>
        /// <param name="nic"></param>
        /// <returns>status of avilability of patient according to nic</returns>
        public bool CheckPatientAvilability(string nic)
        {
            return patient.CheckPatientAvilability(nic);
        }


        /// <summary>
        /// insert patient feedback from from PatientAdmissionDetails class
        /// </summary>
        /// <param name="patientFeedbackDTO"></param>
        public bool InsertPatientFeedBack(PatientFeedbackDTO patientFeedbackDTO)
        {
            return patientAdmission.InsertPatientFeedBack(patientFeedbackDTO);
        }


        /// <summary>
        /// check relationship between patient and patient details accoring to patient id from PatientAdmissionDetails class
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>status of realtionship availability of patient and patientdetails</returns>
        public bool CheckRelationshipBetweenPatientAndPatientDetails(int? patientId)
        {
            return patientAdmission.CheckRelationshipBetweenPatientAndPatientDetails(patientId);
        }


        /// <summary>
        /// view doctor feedback according to id from PatientAdmissionDetails class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DoctorDetailDTO> ViewDoctorFeedback(int? id)
        {
            return patientAdmission.ViewDoctorFeedback(id);
        }


        /// <summary>
        /// get patient detail id according to patient id from PatientAdmissionDetails class
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>patient detail id for patient id</returns>
        public PatientAdmissionDTO GetPatientDetailId(int? patientId)
        {
            return patientAdmission.GetPatientDetailId(patientId);
        }


        /// <summary>
        /// view doctor details according to patient id from PatientAdmissionDetails class
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>doctors for patientid</returns>
        public IEnumerable<AllPatientDTO> ViewDoctorAccordingToPatientId(int patientId)
        {
            return patientAdmission.ViewDoctorAccordingToPatientId(patientId);
        }


        /// <summary>
        /// view all patientfeedback details according to from PatientAdmissionDetails class
        /// </summary>
        /// <returns>all feedback details</returns>
        public IEnumerable<PatientFeedbackDTO> ViewAllPatientFeedbackDetails()
        {
            return patientAdmission.ViewAllPatientFeedbackDetails();
        }


        /// <summary>
        /// view patientfeedbackdetails according to patientfeedbackid
        /// </summary>
        /// <param name="patientFeedBackId"></param>
        /// <returns>patient feedback details according to patientfeedback id</returns>
        public PatientFeedbackDTO ViewAllPatientFeedbackDetailsAccordingToId(int? patientFeedBackId)
        {
            return patientAdmission.ViewAllPatientFeedbackDetailsAccordingToId(patientFeedBackId);
        }


        /// <summary>
        /// get all patient details for doctors
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>patient details</returns>
        public IEnumerable<PatientDoctorDTO> GetAllPatientDetailsForDoctor(int? userId)
        {
            return patientAdmission.GetAllPatientDetailsForDoctor(userId);
        }


        /// <summary>
        /// edit patient feedback details according to patientfeedback object
        /// </summary>
        /// <param name="patientFeedback"></param>
        /// <returns></returns>
        public bool EditPatientsFeedbackDetails(PatientFeedbackDTO patientFeedback)
        {
            return patientAdmission.EditPatientsFeedbackDetails(patientFeedback);
        }


        /// <summary>
        /// view all patientadmissiondetails according to admission
        /// </summary>
        /// <returns>all data of patient admission</returns>
        public IEnumerable<PatientDTO> ViewAllPatientDetailsAccordingToAdmission()
        {
            return patientAdmission.ViewAllPatientDetailsAccordingToAdmission();
        }

        /// <summary>
        /// check relationship between invoice and patientdetails
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns>status of realationship between patient details and invoice</returns>
        public bool CheckRelationshipBetweenPatientDetailsAndInvoice(int? detailId)
        {
            return patientAdmission.CheckRelationshipBetweenPatientDetailsAndInvoice(detailId);
        }

        /// <summary>
        /// check relationship between patientdetails and feedback
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns>status of relationship between patientdetails and feedback</returns>
        public bool CheckRelationshipBetweenPatientDetailsAndpatientFeedback(int? detailId)
        {
            return patientAdmission.CheckRelationshipBetweenPatientDetailsAndpatientFeedback(detailId);
        }

        /// <summary>
        /// check relationship between patientdetails and medical test
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns>status of realtionship between patientdetails and medical test</returns>
        public bool CheckRelationshipBetweenPatientDetailsAndpatientMediaclTest(int? detailId)
        {
            return patientAdmission.CheckRelationshipBetweenPatientDetailsAndpatientMediaclTest(detailId);
        }
        #endregion
    }
}
