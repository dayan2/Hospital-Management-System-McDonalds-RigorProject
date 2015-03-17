#region Using Directives
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitaManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitalManagement.Web;
using Mcd.HospitalManagement.Web.Enums;
#endregion


namespace Mcd.HospitalManagement.Web.Controllers
{
    public class PatientController : BaseController
    {
        #region Constant
        const string ERROR_TYPE = "AccessDenied";
        const string ERROR_MSG = "Error";
        #endregion

        #region private variables
        private IPatientManager patientManager;
        private PatientModel patient;
        private string displayMessage;
        private bool confirmationResult;
        #endregion

        #region constructors
        public PatientController():base()
        {
            patientManager = new PatientManager();
        }
        #endregion

        #region public methods

        /// <summary>
        /// get all details of patients
        /// </summary>
        /// <returns>view all list of patients</returns>
        public ActionResult Index()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                patient = new PatientModel();

                //view all patient records
                var patients = patientManager.ViewAllPatientDetails();
                IEnumerable<PatientModel> PList = patients.Select(c=>new PatientModel
                                                  {
                                                      Id = c.Id,
                                                      Name = c.Name,
                                                      NIC = c.NIC,
                                                      Address = c.Address,
                                                      Gender = c.Gender,
                                                      MobileNo = c.MobileNo,


                                                  }).ToList();
                patient.Patients = PList;
                return View(patient);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

        }

 
        /// <summary>
        /// get patients according to id
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns>patient according to patient id</returns>
        public ActionResult DetailsPatientDetails(int id)
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

                //view patient recors according to id
                PatientDTO PatientDTO = patientManager.ViewPatientDetails(id);
                patient = new PatientModel
                {
                    Id = PatientDTO.Id,
                    Name = PatientDTO.Name,
                    Address = PatientDTO.Address,
                    NIC = PatientDTO.NIC,
                    Gender = PatientDTO.Gender,
                    MobileNo = PatientDTO.MobileNo,


                };




                if (patient == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }


        /// <summary>
        /// add patients
        /// </summary>
        /// <returns>status of insert</returns>
        public ActionResult CreatePatientDetails()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                return View();
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

   
        /// <summary>
        /// add patients 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nic"></param>
        /// <param name="address"></param>
        /// <param name="gender"></param>
        /// <param name="mobileNo"></param>
        /// <returns>status of insert</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatientDetails(string name, string nic, string address, string gender, string mobileNo)
        {


            PatientDTO patientDTO = new PatientDTO { Name = name, NIC = nic, Address = address, Gender = gender, MobileNo = mobileNo };
            if (ModelState.IsValid)
            {

                //check availble patients according to id
                if (patientManager.CheckPatientAvilability(patientDTO.NIC))
                {

                    //insert patient
                    confirmationResult = patientManager.InsertPatient(patientDTO);
                    displayMessage = string.Empty;
                    if (confirmationResult == true)
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                    else
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                    return Json(new { Message = displayMessage });
                }
                else
                {
                    displayMessage = CustomEnumMessage.GetStringValue(PatientErrorMessage.checkAvilabiltyOfPatient);
                    return Json(new { Message = displayMessage });
                }

            }
            return View();
        }

        /// <summary>
        /// get patient according to id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>patient details</returns>
        public ActionResult EditPatientDetails(int? id)
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

                //view patient according to id
                PatientDTO patientDTO = patientManager.ViewPatientDetails(id);
                patient = new PatientModel
                {
                    Id = patientDTO.Id,
                    Name = patientDTO.Name,
                    Address = patientDTO.Address,
                    NIC = patientDTO.NIC,
                    Gender = patientDTO.Gender,
                    MobileNo = patientDTO.MobileNo,


                };

                List <string> lst = new List <string>();
                lst.Add("Female");
                lst.Add("Male");
                ViewBag.Gender = new SelectList(lst,patient.Gender);



                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }



        /// <summary>
        /// edit patient details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="nic"></param>
        /// <param name="address"></param>
        /// <param name="gender"></param>
        /// <param name="mobileNo"></param>
        /// <returns>staus of insert</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatientDetails(string id, string name, string nic, string address, string gender, string mobileNo)
        {
            PatientDTO patientDTO = new PatientDTO
            {
                Id = int.Parse(id),
                Name = name,
                NIC = nic,
                Address = address,
                Gender = gender,
                MobileNo = mobileNo
            };
            if (ModelState.IsValid)
            {


                //edit patient 
                confirmationResult = patientManager.EditPatient(patientDTO);
                displayMessage = string.Empty;
                if (confirmationResult == true)
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                return Json(new { Message = displayMessage });

            }
            return View(patient);
        }


        /// <summary>
        /// get patient according to id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeletePatientDetails(int id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin || (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //view patient according to id
                PatientDTO patientDTO = patientManager.ViewPatientDetails(id);
                patient = new PatientModel
                {
                    Id = patientDTO.Id,
                    Name = patientDTO.Name,
                    Address = patientDTO.Address,
                    NIC = patientDTO.NIC,
                    Gender = patientDTO.Gender,
                    MobileNo = patientDTO.MobileNo,


                };
                if (patient == null)
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        //// POST: /Patient/Delete/5
        /// <summary>
        /// delete patient
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns>status of delete</returns>
        [HttpPost, ActionName("DeletePatientDetails")]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the relationship between patinet and patientdetails...if their is relationship can't delete
            if (!patientManager.CheckRelationshipBetweenPatientAndPatientDetails(id))
            {
                displayMessage = CustomEnumMessage.GetStringValue(PatientErrorMessage.valideteRelationshipTopatientDetils);
                return Json(new { Message = displayMessage });
            }
            else
            {

                //delete patient according to patient id
                confirmationResult = patientManager.Deletepatient(id);
                displayMessage = string.Empty;
                if (confirmationResult == true)
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                return Json(new { Message = displayMessage });
            }



        }
        #endregion

    }
}
