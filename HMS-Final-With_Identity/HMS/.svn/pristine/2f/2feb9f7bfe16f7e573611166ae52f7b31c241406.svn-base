#region Using Directives
using Mcd.HospitalManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagement.Web.Enums;
#endregion

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class PatientAdmissionDetailsController : BaseController
    {
        #region Constant
        const string ERROR_TYPE = "AccessDenied";
        const string ERROR_MSG = "Error";
        #endregion

        #region Private Variable
        private IPatientManager patientManager;
        private PatientAdmissionDetailsModel patientAdmissionDetails;
        private PatientModel patient;
        private IWards wards;
        private IDoctorManager doctors;
        private IBedManager bedDetails;
        private string displayMessage;
        private bool confirmationResult;
        #endregion


        #region Constructor
        public PatientAdmissionDetailsController()
            : base()
        {
            patientManager = new PatientManager();
            wards = new WardDetails();
            doctors = new DoctorManager();
            bedDetails = new BedManager();
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// get all patient admissioion details
        /// </summary>
        /// <returns>patient admission details</returns>
        public ActionResult Index()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                //view all the all patientAddmission details
                IEnumerable<AllPatientDTO> patientAdmissionDTO = patientManager.ViewAllPatientAdmissionDetails();
                IEnumerable<PatientAdmissionDetailsModel> patientAdmissionDetails = patientAdmissionDTO.Select(x => new PatientAdmissionDetailsModel
                {
                    PatientDetailId = x.PatientDetailId,
                    PatientId = x.PatientId,
                    Address = x.Address,
                    MobileNo = x.MobileNo,
                    AdmitDate = x.AdmitDate,
                    Name = x.Name,
                    WardNo = x.WardNo,
                    WardId = x.WardId,
                    BedTicketNo = x.BedTicketNo,
                    BedId = x.BedId,
                    DoctorName = x.DoctorName,
                    DoctorId = x.DoctorId,
                    IsDischarged = x.IsDischarged,
                }).ToList();

                return View(patientAdmissionDetails);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }


        /// <summary>
        /// get patient admission details according to patient id
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns>patient admission details for patient</returns>
        public ActionResult DetailsPatientAdmission(int? id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }

                //view patient admission details according to patient id
                AllPatientDTO patientAdmissionDTO = patientManager.ViewPatientRelatedData(id);
                patientAdmissionDetails = new PatientAdmissionDetailsModel()
                {
                    PatientId = patientAdmissionDTO.PatientId,
                    PatientDetailId = patientAdmissionDTO.PatientDetailId,
                    Address = patientAdmissionDTO.Address,
                    MobileNo = patientAdmissionDTO.MobileNo,
                    AdmitDate = patientAdmissionDTO.AdmitDate,
                    Name = patientAdmissionDTO.Name,
                    WardNo = patientAdmissionDTO.WardNo,
                    WardId = patientAdmissionDTO.WardId,
                    BedTicketNo = patientAdmissionDTO.BedTicketNo,
                    BedId = patientAdmissionDTO.BedId,
                    DoctorName = patientAdmissionDTO.DoctorName,
                    DoctorId = patientAdmissionDTO.DoctorId,
                    IsDischarged = patientAdmissionDTO.IsDischarged,




                };

                if (patientAdmissionDetails == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                return View(patientAdmissionDetails);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }


        /// <summary>
        /// get list of all patients,doctors,wards and bed details
        /// </summary>
        /// <returns>list of patients,doctors,wards and beds</returns>
        public ActionResult CreatePatientAdmission()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                //get patient details
                patient = new PatientModel();
                var patients = patientManager.fillPatients();
                IEnumerable<PatientModel> PList = patients.Select(c => new PatientModel

                                                  {
                                                      Id = c.Id,
                                                      Name = c.Name,
                                                  }).ToList();

                //get ward Details


                var wardList = wards.ViewWardDetails();
                IEnumerable<WardModel> wardDetailList = wardList.Select(w => new WardModel
                                                               {
                                                                   Id = w.Id,
                                                                   WardNo = w.WardNo,
                                                               }).ToList();


                //get doctor details

                var doctorList = doctors.GetDoctors();
                IEnumerable<DoctorModel> doctorDetailList = doctorList.Select(d => new DoctorModel
                                                                           {
                                                                               Id = d.Id,
                                                                               Name = d.Name
                                                                           }).ToList();


                //get beds details

                var bedList = bedDetails.ViewAllBed();
                IEnumerable<BedModel> bedDetailList = bedList.Select(d =>
                                                      new BedModel
                                                                             {
                                                                                 Id = d.Id,
                                                                                 BedTicketNo = d.BedTicketNo
                                                                             }).ToList();


                //set the default discharge status for patient admission details
                patientAdmissionDetails = new PatientAdmissionDetailsModel() { IsDischarged = false };


                ViewBag.BedId = new SelectList(bedDetailList, "Id", "BedTicketNo");
                ViewBag.DoctorId = new SelectList(doctorDetailList, "Id", "Name");
                ViewBag.PatientId = new SelectList(PList, "Id", "Name");
                ViewBag.WardId = new SelectList(wardDetailList, "Id", "WardNo");
                return View(patientAdmissionDetails);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }


        /// <summary>
        /// insert patient admission details.when inserting check that patient
        /// is already exists or not
        /// </summary>
        /// <param name="patientdetail">patientdetails object</param>
        /// <returns>status of insert</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatientAdmission(PatientAdmissionDetailsModel patientdetail)
        {
            PatientAdmissionDTO patientAdmissionDTO = new PatientAdmissionDTO()
            {
                PatientDetailId = patientdetail.PatientDetailId,
                AdmitDate = patientdetail.AdmitDate,
                BedId = patientdetail.BedId,
                WardId = patientdetail.WardId,
                DoctorId = patientdetail.DoctorId,
                PatientId = patientdetail.PatientId,
                IsDischarged = patientdetail.IsDischarged
            };
            if (ModelState.IsValid)
            {
                //check the dischage status...if discharge status true only can insert
                if (patientManager.CheckPatientAdmissionAvilabilty(patientAdmissionDTO.PatientId))
                {

                    //insert patient admissiondetails 
                    var confirmationResult = patientManager.InsertPatientAdmissionDetails(patientAdmissionDTO);
                    displayMessage = string.Empty;
                    if (confirmationResult == true)
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                    else
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                    return Json(new { Message = displayMessage });

                }
                else
                {

                    displayMessage = CustomEnumMessage.GetStringValue(PatientErrorMessage.AlreadyAdmittedPatient);
                    return Json(new { Message = displayMessage });


                }
            }

            //get patient details
            patient = new PatientModel();
            var patients = patientManager.fillPatients();
            IEnumerable<PatientModel> PList = patients.Select(c => new PatientModel

            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            //get ward Details


            var wardList = wards.ViewWardDetails();
            IEnumerable<WardModel> wardDetailList = wardList.Select(w => new WardModel
            {
                Id = w.Id,
                WardNo = w.WardNo,
            }).ToList();


            //get doctor details

            var doctorList = doctors.GetDoctors();
            IEnumerable<DoctorModel> doctorDetailList = doctorList.Select(d => new DoctorModel
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();


            //get beds details

            var bedList = bedDetails.ViewAllBed();
            IEnumerable<BedModel> bedDetailList = bedList.Select(d =>
                                                  new BedModel
                                                  {
                                                      Id = d.Id,
                                                      BedTicketNo = d.BedTicketNo
                                                  }).ToList();



            ViewBag.BedId = new SelectList(bedDetailList, "Id", "BedTicketNo");
            ViewBag.DoctorId = new SelectList(doctorDetailList, "Id", "Name");
            ViewBag.PatientId = new SelectList(PList, "Id", "Name");
            ViewBag.WardId = new SelectList(wardDetailList, "Id", "WardNo");


            return View();
        }

        /// <summary>
        /// get patient admission details according to patient id
        /// </summary>
        /// <param name="id">patientdetails id</param>
        /// <returns>patient admission list</returns>
        public ActionResult EditPatientAdmission(int? id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {

                if (id == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }

                //view patientadmission details according to id
                AllPatientDTO patientAdmissionDTO = patientManager.ViewPatientRelatedData(id);
                patientAdmissionDetails = new PatientAdmissionDetailsModel()
                {
                    PatientId = patientAdmissionDTO.PatientId,
                    PatientDetailId = patientAdmissionDTO.PatientDetailId,
                    Address = patientAdmissionDTO.Address,
                    MobileNo = patientAdmissionDTO.MobileNo,
                    AdmitDate = patientAdmissionDTO.AdmitDate,
                    Name = patientAdmissionDTO.Name,
                    WardNo = patientAdmissionDTO.WardNo,
                    WardId = patientAdmissionDTO.WardId,
                    BedTicketNo = patientAdmissionDTO.BedTicketNo,
                    BedId = patientAdmissionDTO.BedId,
                    DoctorName = patientAdmissionDTO.DoctorName,
                    DoctorId = patientAdmissionDTO.DoctorId,
                    IsDischarged = patientAdmissionDTO.IsDischarged,




                };

                if (patientAdmissionDetails == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }

                //get patient details
                patient = new PatientModel();
                var patients = patientManager.fillPatients();
                IEnumerable<PatientModel> PList = patients.Select(c => new PatientModel

                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();

                //get ward Details


                var wardList = wards.ViewWardDetails();
                IEnumerable<WardModel> wardDetailList = wardList.Select(w => new WardModel
                {
                    Id = w.Id,
                    WardNo = w.WardNo,
                }).ToList();


                //get doctor details

                var doctorList = doctors.GetDoctors();
                IEnumerable<DoctorModel> doctorDetailList = doctorList.Select(d => new DoctorModel
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();


                //get beds details

                var bedList = bedDetails.ViewAllBed();
                IEnumerable<BedModel> bedDetailList = bedList.Select(d =>
                                                      new BedModel
                                                      {
                                                          Id = d.Id,
                                                          BedTicketNo = d.BedTicketNo
                                                      }).ToList();




                ViewBag.BedTicketNo = new SelectList(bedDetailList, "Id", "BedTicketNo", patientAdmissionDetails.BedId);
                ViewBag.DoctorName = new SelectList(doctorDetailList, "Id", "Name", patientAdmissionDetails.DoctorId);
                ViewBag.Name = new SelectList(PList, "Id", "Name", patientAdmissionDetails.PatientId);
                ViewBag.WardNo = new SelectList(wardDetailList, "Id", "WardNo", patientAdmissionDetails.WardId);
                patientAdmissionDetails.IsDischarged = false;



                return View(patientAdmissionDetails);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// edit patient admission details
        /// </summary>
        /// <param name="patientDetail"> patientdetail object</param>
        /// <returns>status of edit</returns>
        [HttpPost, ActionName("EditPatientAdmission")]
        public ActionResult EditPatient(PatientAdmissionDetailsModel patientDetail)
        {
            AllPatientDTO patientAdmissionDTO = new AllPatientDTO()
            {
                PatientDetailId = patientDetail.PatientDetailId,
                AdmitDate = patientDetail.AdmitDate,
                BedId = int.Parse(patientDetail.BedTicketNo),
                WardId = int.Parse(patientDetail.WardNo),
                DoctorId = int.Parse(patientDetail.DoctorName),
                PatientId = int.Parse(patientDetail.Name),
                IsDischarged = patientDetail.IsDischarged,



            };
            if (ModelState.IsValid)
            {

                //edit patientAdmissiondetails
                var confirmationResult = patientManager.EditPatientAdmissionDetails(patientAdmissionDTO);
                displayMessage = string.Empty;
                if (confirmationResult == true)
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                return Json(new { Message = displayMessage });


            }

            //get patient details
            patient = new PatientModel();
            var patients = patientManager.fillPatients();
            IEnumerable<PatientModel> PList = patients.Select(c => new PatientModel

            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            //get ward Details


            var wardList = wards.ViewWardDetails();
            IEnumerable<WardModel> wardDetailList = wardList.Select(w => new WardModel
            {
                Id = w.Id,
                WardNo = w.WardNo,
            }).ToList();


            //get doctor details

            var doctorList = doctors.GetDoctors();
            IEnumerable<DoctorModel> doctorDetailList = doctorList.Select(d => new DoctorModel
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();


            //get beds details

            var bedList = bedDetails.ViewAllBed();
            IEnumerable<BedModel> bedDetailList = bedList.Select(d =>
                                                  new BedModel
                                                  {
                                                      Id = d.Id,
                                                      BedTicketNo = d.BedTicketNo
                                                  }).ToList();

            ViewBag.BedId = new SelectList(bedDetailList, "Id", "BedTicketNo");
            ViewBag.DoctorId = new SelectList(doctorDetailList, "Id", "Name");
            ViewBag.PatientId = new SelectList(PList, "Id", "Name");
            ViewBag.WardId = new SelectList(wardDetailList, "Id", "WardNo");

            return View(patientDetail);
        }



        /// <summary>
        /// get patient admission details according to id.
        /// </summary>
        /// <param name="id">patientdetail id</param>
        /// <returns></returns>
        public ActionResult DeletePatientAdmission(int? id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }

                //view patient admission details according to id
                AllPatientDTO patientAdmissionDTO = patientManager.ViewPatientRelatedData(id);
                patientAdmissionDetails = new PatientAdmissionDetailsModel()
                {
                    AdmitDate = patientAdmissionDTO.AdmitDate,
                    PatientId = patientAdmissionDTO.PatientId,
                    PatientDetailId = patientAdmissionDTO.PatientDetailId,
                    Address = patientAdmissionDTO.Address,
                    MobileNo = patientAdmissionDTO.MobileNo,
                    Name = patientAdmissionDTO.Name,
                    WardNo = patientAdmissionDTO.WardNo,
                    WardId = patientAdmissionDTO.WardId,
                    BedTicketNo = patientAdmissionDTO.BedTicketNo,
                    BedId = patientAdmissionDTO.BedId,
                    DoctorName = patientAdmissionDTO.DoctorName,
                    DoctorId = patientAdmissionDTO.DoctorId,
                    IsDischarged = patientAdmissionDTO.IsDischarged,




                };
                if (patientAdmissionDetails == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                return View(patientAdmissionDetails);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }


        /// <summary>
        /// delete patient admission details
        /// </summary>
        /// <param name="id">patient detail id</param>
        /// <returns></returns>
        [HttpPost, ActionName("DeletePatientAdmission")]
        public ActionResult DeleteConfirmed(int? id)
        {

            //check relationship to invoice with patient details
            if (patientManager.CheckRelationshipBetweenPatientDetailsAndInvoice(id))
            {
                displayMessage = CustomEnumMessage.GetStringValue(PatientErrorMessage.valideteRelationship);
                return Json(new { Message = displayMessage });
            }

            //check relationship to patientfeedback with patientdetails
            if (patientManager.CheckRelationshipBetweenPatientDetailsAndpatientFeedback(id))
            {
                displayMessage = CustomEnumMessage.GetStringValue(PatientErrorMessage.valideteRelationship);
                return Json(new { Message = displayMessage });
            }

            //check relationship between patientMedicalTest to patient details
            if (patientManager.CheckRelationshipBetweenPatientDetailsAndpatientMediaclTest(id))
            {
                displayMessage = CustomEnumMessage.GetStringValue(PatientErrorMessage.valideteRelationship);
                return Json(new { Message = displayMessage });
            }

            //delete patient admission details
            confirmationResult = patientManager.DeletePatientAdmissionDetails(id);
            displayMessage = string.Empty;
            if (confirmationResult == true)
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
            else
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

            return Json(new { Message = displayMessage });

        }
        #endregion

    }
}
