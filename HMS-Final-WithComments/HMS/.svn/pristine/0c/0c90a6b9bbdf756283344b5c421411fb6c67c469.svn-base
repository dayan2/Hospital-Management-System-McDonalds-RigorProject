#region Using Directives
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitalManagement.Web.Enums;

#endregion

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class PatientFeedbackController : Controller
    {
        #region private variables
        private IDoctorManager doctors;
        private IPatientManager patientManager;
        private PatientModel patient;
        private PatientFeedbackModel patientFeedBack;
        private string displayMessage;
        private bool confirmationResult;
        #endregion

        public PatientFeedbackController()
        {
            patientManager = new PatientManager();

            doctors = new DoctorManager();

        }

        // GET: /PatientFeedback/
        public ActionResult Index()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }

            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                //view all patient feedback details
                IEnumerable<PatientFeedbackDTO> patientFeedBacksDTO = patientManager.ViewAllPatientFeedbackDetails();
                IEnumerable<PatientFeedbackModel> patientFeedBackDetails = patientFeedBacksDTO.Select(x => new PatientFeedbackModel
                {
                    Id = x.Id,
                    PatientDetailId = x.PatientDetailId,
                    DoctorId = x.DoctorId,
                    DoctorName = x.DoctorName,
                    FeedbackDate = x.FeedbackDate,
                    FeedbackDescription = x.FeedbackDescription,
                    PatientName = x.PatientName
                }).ToList();

                return View(patientFeedBackDetails);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }
        public ActionResult FeedbackDetails(int? id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //view feedback details according to feedback id
                PatientFeedbackDTO patientFeedbackDTO = patientManager.ViewAllPatientFeedbackDetailsAccordingToId(id);
                patientFeedBack = new PatientFeedbackModel()
                {


                    Id = patientFeedbackDTO.Id,
                    DoctorId = patientFeedbackDTO.DoctorId,
                    DoctorName = patientFeedbackDTO.DoctorName,
                    PatientId = patientFeedbackDTO.PatientId,
                    PatientName = patientFeedbackDTO.PatientName,
                    FeedbackDate = patientFeedbackDTO.FeedbackDate,
                    FeedbackDescription = patientFeedbackDTO.FeedbackDescription,
                    PatientDetailId = patientFeedbackDTO.PatientDetailId




                };

                if (patientFeedBack == null)
                {
                    return HttpNotFound();
                }
                return View(patientFeedBack);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        // GET: /PatientFeedback/Create
        public ActionResult Create()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                //get patient details
                patient = new PatientModel();
                var patients = patientManager.fillPatients();
                IEnumerable<PatientFeedbackModel> patientsList = patients.Select(c => new PatientFeedbackModel

                                                  {
                                                      PatientId = c.Id,
                                                      PatientName = c.Name,
                                                  }).ToList();

                return View(patientsList);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }




        public ActionResult GetDoctorsById(int PatientId)
        {


            if (String.IsNullOrEmpty(PatientId.ToString()))
            {
                throw new ArgumentNullException("PatientId");
            }

            //get doctors according to patient id
            var doctors = patientManager.ViewDoctorAccordingToPatientId(PatientId);
            IEnumerable<PatientFeedbackModel> doctorList = doctors.Select(x => new PatientFeedbackModel

                                                    {
                                                        DoctorId = (int)x.DoctorId,
                                                        DoctorName = x.DoctorName

                                                    }).ToList();

            return PartialView("PatientFeedBackDetails", doctorList);

        }

        [HttpPost]
        public ActionResult InsertFeedBack(string patientId, string doctorId, string feedbackDate, string feedBackDescription)
        {

            //get patientdetailid according to patientid
            var patientDetailId = patientManager.GetPatientDetailId(Int32.Parse(patientId));

            var patientFeedBack = new PatientFeedbackDTO()
            {

                DoctorId = int.Parse(doctorId),
                PatientDetailId = patientDetailId.PatientDetailId,
                FeedbackDate = Convert.ToDateTime(feedbackDate),
                FeedbackDescription = feedBackDescription

            };

            if (ModelState.IsValid)
            {

                //insert patient feedback
                patientManager.InsertPatientFeedBack(patientFeedBack);

                //displayMessage = string.Empty;
                //if (confirmationResult == true)
                //    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                //else
                //    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                //return Json(new { Message = displayMessage });
            }
            else
            {

            }



            return View();
        }

        // GET: /EditPatientFeedBack/Edit/5
        public ActionResult EditPatientFeedBack(int? id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //view patientadmission details according to id
                PatientFeedbackDTO patientFeedbackDTO = patientManager.ViewAllPatientFeedbackDetailsAccordingToId(id);
                PatientFeedbackModel feedbackModel = new PatientFeedbackModel()
                {


                    Id = patientFeedbackDTO.Id,
                    DoctorId = patientFeedbackDTO.DoctorId,
                    DoctorName = patientFeedbackDTO.DoctorName,
                    PatientId = patientFeedbackDTO.PatientId,
                    PatientName = patientFeedbackDTO.PatientName,
                    FeedbackDate = patientFeedbackDTO.FeedbackDate,
                    FeedbackDescription = patientFeedbackDTO.FeedbackDescription,
                    PatientDetailId = patientFeedbackDTO.PatientDetailId



                };

                if (feedbackModel == null)
                {
                    return HttpNotFound();
                }

                return View(feedbackModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        //// POST: /EditPatientFeedBack/Edit/5
        [HttpPost, ActionName("EditPatientFeedBack")]
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult EditPatientFeedBack(PatientFeedbackModel feedbackModel)
        {
            //patient detail id according to patinet id
            var patientDetailId = patientManager.GetPatientDetailId(feedbackModel.PatientId);

            var patientFeedBack = new PatientFeedbackDTO()
            {
                Id = feedbackModel.Id,
                DoctorId = feedbackModel.DoctorId,
                PatientDetailId = patientDetailId.PatientDetailId,
                FeedbackDate = feedbackModel.FeedbackDate,
                FeedbackDescription = feedbackModel.FeedbackDescription,
                DoctorName = feedbackModel.DoctorName,
                PatientName = feedbackModel.PatientName

            };

            if (ModelState.IsValid)
            {

                //edit patientAdmissiondetails
                confirmationResult = patientManager.EditPatientsFeedbackDetails(patientFeedBack);
                displayMessage = string.Empty;
                if (confirmationResult == true)
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                return Json(new { Message = displayMessage });


            }

            return View(feedbackModel);
        }









    }
}
