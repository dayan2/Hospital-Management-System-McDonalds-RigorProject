#region Using Directives
using Mcd.HospitalManagement.Web.Enums;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitalManagement.Web.UserIdentityScope;
using Mcd.HospitaManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
#endregion


namespace Mcd.HospitalManagement.Web.Controllers
{
    public class DoctorRecommendationController : Controller
    {
        #region Constants
        const string ERROR_TYPE = "AccessDenied";
        const string ERROR_MESSAGE = "Error";
        #endregion

        #region private fields
        IDoctorRecommendationManager doctorRecommendationManager;
        #endregion

        #region Constructors
        public DoctorRecommendationController()
        {
            this.doctorRecommendationManager = new DoctorRecommendationManager();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Getting all doctor recommendations from the businessLayer method and will be displayed in the View
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin,Cashier")]
        public ActionResult Index()
        {

            //Get all doctor recommendations from the businessLayer method
            IEnumerable<DoctorRecommendationDTO> doctorRecommendationList = doctorRecommendationManager.GetDoctorRecommendationDetails();

            //Mapping DTO into Model
            IEnumerable<DoctorRecommendationModel> doctorRecommendationModelList =
                doctorRecommendationList.Select(m => new DoctorRecommendationModel
            {
                CurrentDoctorId = m.CurrentDoctorId,
                CurrentDoctorName = m.CurrentDoctorName,
                PatientId = m.PatientId,
                PatientName = m.PatientName,
                Reason = m.Reason,
                RecomendedDoctorId = m.RecomendedDoctorId,
                RecomendedDoctorName = m.RecomendedDoctorName,
                Id = m.Id

            });

            return View(doctorRecommendationModelList);
        }

        /// <summary>
        /// HTTPGET - Getting DoctorRecommendation details from DB using it's ID and will be displayed in the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin,Cashier")]
        [HttpGet]
        public ActionResult UpdateDoctorRecommendationDetails(int id)
        {

            //Getting info about DoctorRecommendations using it's ID
            DoctorRecommendationDTO doctorRecommendation = doctorRecommendationManager.GetDoctorRecommendationById(id);

            //Mapping DTO into Model
            DoctorRecommendationModel doctor = new DoctorRecommendationModel
            {
                Id = doctorRecommendation.Id,
                CurrentDoctorName = doctorRecommendation.CurrentDoctorName,
                CurrentDoctorId = doctorRecommendation.CurrentDoctorId,
                PatientId = doctorRecommendation.PatientId,
                PatientName = doctorRecommendation.PatientName,
                Reason = doctorRecommendation.Reason,
                RecomendedDoctorId = doctorRecommendation.RecomendedDoctorId,
                RecomendedDoctorName = doctorRecommendation.RecomendedDoctorName
            };

            return View(doctor);
        }

        /// <summary>
        /// HTTPPOST - Updating the doctorRecommendation details
        /// </summary>
        /// <param name="doctorRecommendation"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin,Cashier")]
        [HttpPost]
        public ActionResult UpdateDoctorRecommendationDetails(DoctorRecommendationDTO doctorRecommendation)
        {

            //Declaring PatientManager class through IPatientManager Interface
            IPatientManager patietManager = new PatientManager();

            //Creating a String variable to hold the display result
            string displayMessage = string.Empty;

            // get  patient Detail Id by max admit date
            PatientAdmissionDTO patientAdmissionFirst = patietManager.GetPatientDetailId(doctorRecommendation.PatientId);

            // get all patient details
            PatientAdmissionDTO patientAdmissionDTO = patietManager.ViewPatientAdmissionDetails(patientAdmissionFirst.PatientDetailId);

            //Mapping Model into DTO
            AllPatientDTO patientAllAdmissionDTO = new AllPatientDTO()
            {
                PatientDetailId = patientAdmissionDTO.PatientDetailId,
                AdmitDate = patientAdmissionDTO.AdmitDate,
                BedId = patientAdmissionDTO.BedId,
                WardId = patientAdmissionDTO.WardId,
                DoctorId = doctorRecommendation.RecomendedDoctorId,
                PatientId = (int)doctorRecommendation.PatientId,
                IsDischarged = patientAdmissionDTO.IsDischarged,
            };

            //Checking whether EditPatientAdmissionDetails method's returning boolean value is true
            if (patietManager.EditPatientAdmissionDetails(patientAllAdmissionDTO))
            {
                bool confirmationResult = doctorRecommendationManager.RemoveDoctorRecommendationDetails(doctorRecommendation.Id);
                //inserting the success Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
            }
            else
            {
                //inserting the Error Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            }

            //Returning a Json result
            return Json(new { Message = displayMessage });
        }


        /// <summary>
        /// Removing DoctorRecommendations by it's ID
        /// </summary>
        /// <param name="id">Removing DoctorRecommendation ID will be passed here</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin,Cashier")]
        public ActionResult RemoveDoctorRecommendationDetails(int id)
        {
            //Getting the Confirmation result (Checking whether doctorRecommendation object is removed from DB 
            //and returning a boolean result)
            bool confirmationResult = doctorRecommendationManager.RemoveDoctorRecommendationDetails(id);
            return View();

        }

        #endregion

    }
}

