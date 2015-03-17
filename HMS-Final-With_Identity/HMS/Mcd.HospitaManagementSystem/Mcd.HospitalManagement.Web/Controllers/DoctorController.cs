﻿#region Using Directives
using Mcd.HospitalManagement.Web.Enums;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitalManagement.Web.UserIdentityScope;
using Mcd.HospitaManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
#endregion


namespace Mcd.HospitalManagement.Web.Controllers
{
    public class DoctorController : BaseController
    {
        #region Constants
        const string ERROR_TYPE = "AccessDenied";
        const string ERROR_MESSAGE = "Error";
        #endregion


        #region Private fields
        private IDoctorManager doctorManager;
        #endregion


        #region Constructors

        public DoctorController()
            : base()
        {
            this.doctorManager = new DoctorManager();
        }

        public DoctorController(IDoctorManager doctorManager)
        {
            this.doctorManager = doctorManager;
        }
        #endregion


        #region ActionMethods

        /// <summary>
        /// Action method will redirect to the Doctor's Main Menu
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Doctor")]
        public ActionResult HomePage()
        {
            return View();
        }


        /// <summary>
        /// returning to the view
        /// </summary>
        /// <returns>ReDirecting to it's View With doctor's patient list</returns>
        [CustomAuthorize(Roles = "Doctor")]
        public ActionResult Index()
        {
            //Declaring PatientManager class through IPatientManager Interface
            IPatientManager patientDetailsManager = new PatientManager();

            //getting session UserId to a variable
            int userId = Convert.ToInt32(Session["USERID"]);

            ////Sending the userId and getting patient details that each Doctor 
            IEnumerable<PatientDoctorDTO> patientDTOList = patientDetailsManager.GetAllPatientDetailsForDoctor(userId);

            //Mapping DTO into Model
            IEnumerable<PatientDoctorModel> patientList = patientDTOList.Select(c => new PatientDoctorModel
            {
                Id = c.Id,
                Name = c.Name,
                AdmitDate = c.AdmitDate,
                Gender = c.Gender,
                Ward = c.Ward,
                IsDischarged = c.IsDischarged,
                Bed = c.Bed
            });

            return View(patientList);

        }

        /// <summary>
        /// Retrieving All Doctors from the DB and returning it to the View
        /// </summary>
        /// <returns>Returns DoctorModel Type of List to the View</returns>
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult GetDoctors()
        {
            //getting all Doctor elements from DB
            IEnumerable<DoctorModel> doctorList = RetrieveDoctors();
            return View(doctorList);
        }

        /// <summary>
        /// AddDoctor HTTPGET method will direct into it's view
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddDoctor()
        {

            #region getUsers into a dropDownList

            //Declaring UserManager class through IUserManager Interface
            IUserManager userManager = new UserManager();

            //Getting all user elements into a list
            IEnumerable<UserDTO> userList = userManager.ViewtAllUser();

            //Creating a DropDown List with userNames and IDs
            List<SelectListItem> userReturningValues = new List<SelectListItem>();

            foreach (var i in userList)
            {
                userReturningValues.Add(new SelectListItem
                {
                    Text = i.UserName,
                    Value = i.Id.ToString()
                });
            }

            //Putting DropDown to a ViewBag
            ViewBag.userViewbag = userReturningValues.ToList();
            #endregion


            #region get Wards into a dropDownList

            //Declaring WardManager class through IWards Interface
            IWards wardManager = new WardManager();

            //Getting all ward elements into a list
            IEnumerable<WardDTO> wardList = wardManager.ViewWardDetails();
            //IEnumerable<DoctorModel> doctorList = GetFakeDoctorsList();

            //Creating a DropDown List with wardNames and IDs
            List<SelectListItem> wardReturningValues = new List<SelectListItem>();

            foreach (var i in wardList)
            {
                wardReturningValues.Add(new SelectListItem
                {
                    Text = i.WardNo,
                    Value = i.Id.ToString()
                });
            }

            //Putting DropDown to a ViewBag
            ViewBag.wardViewbag = wardReturningValues.ToList();
            #endregion


            #region get DoctorSpecialities into a dropDownList

            //Declaring PatientManager class through IPatientManager Interface
            IPatientManager patientManager = new PatientManager();

            //Getting all DoctorSpecialities elements into a list
            IEnumerable<DoctorSpecialityDTO> doctorSpecialitiesList = doctorManager.GetAllDoctorSpecialityTypes();

            //Creating a DropDown List with DoctorSpecialitiesNames and IDs
            List<SelectListItem> doctorSpecialitiesReturningValues = new List<SelectListItem>();

            foreach (var i in doctorSpecialitiesList)
            {
                doctorSpecialitiesReturningValues.Add(new SelectListItem
                {
                    Text = i.SpecializeArea,
                    Value = i.DoctorSpecialityId.ToString()
                });
            }

            //Putting DropDown to a ViewBag
            ViewBag.doctorSpecialitiesViewbag = doctorSpecialitiesReturningValues.ToList();
            #endregion


            return View();
        }

        /// <summary>
        /// AddDoctor HTTPPOST method will retrieve all doctor details and will be sent to the DB to store
        /// </summary>
        /// <param name="doctor">AddDoctor view will be Posting a DoctorModel which will be then stored in the DB</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AddDoctor(DoctorModel doctor)
        {
            //Mapping DTO into Model
            DoctorRoleDTO doctorModel = new DoctorRoleDTO
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Charges = doctor.Charges,
                DoctorSpecialityId = doctor.DoctorSpecialityId,
                PhoneNo = doctor.PhoneNo,
                WardId = doctor.WardId,
                UserId = doctor.UserId
            };

            string doctorExistsMessage = string.Empty;

            //Check whether doctor details are already exist in DB
            doctorExistsMessage = doctorManager.DoctorAlreadyExist(doctor.Name);

            //if doctor Name already exists in DB, prompting an error message to user
            if (doctorExistsMessage == CustomEnumMessage.GetStringValue(DoctorError.AlreadyExistMsg))
            {
                //returning a Json result to the view saying Record already exist
                return Json(new { Message = doctorExistsMessage });
            }


            //Getting the Confirmation result (Checking whether doctor object is added into the DB 
            //and returning a boolean result)
            bool confirmationResult = doctorManager.AddDoctor(doctorModel);

            //Creating a String variable to hold the display result
            string displayMessage = string.Empty;
            if (confirmationResult == true)
            {
                //inserting the success Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
            }
            else
            {
                //inserting the Error Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            }
            //returning the message through Json
            return Json(new { Message = displayMessage });

        }

        /// <summary>
        /// UpdateDoctor HTTPGET method will be retrieving the specified doctor's details 
        /// And it will send those details into the View to be filled in each textboxes
        /// </summary>
        /// <param name="id">Getting the doctorId</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateDoctor(int id)
        {
            //Getting info about Doctor using their ID
            DoctorRoleDTO doctorDTO = doctorManager.GetDoctorsById(id);

            //Mapping DTO into Model
            DoctorModel doctor = new DoctorModel
            {
                Id = doctorDTO.Id,
                Name = doctorDTO.Name,
                Charges = doctorDTO.Charges,
                DoctorSpecialityId = doctorDTO.DoctorSpecialityId,
                PhoneNo = doctorDTO.PhoneNo,
                WardId = doctorDTO.WardId,
                UserId = doctorDTO.UserId
            };

            return View(doctor);
        }

        /// <summary>
        /// UpdateDoctor HTTPPOST method will send the updated doctor details to the DB
        /// </summary>
        /// <param name="doctor">Modified doctor details</param>
        /// <returns>Redirecting the View To HomePage</returns>
        //[HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult UpdateDoctor(DoctorModel doctor)
        {
            //Mapping Model object into DTO to send it to the Business Layer
            DoctorRoleDTO doctorModel = new DoctorRoleDTO
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Charges = doctor.Charges,
                DoctorSpecialityId = doctor.DoctorSpecialityId,
                PhoneNo = doctor.PhoneNo,
                WardId = doctor.WardId,
                UserId = doctor.UserId
            };

            //Getting the Confirmation result (Checking whether doctor object is Updated in DB 
            //and returning a boolean result)
            bool confirmationResult = doctorManager.UpdateDoctor(doctorModel);

            //Creating a String variable to hold the display result
            string displayMessage = string.Empty;
            if (confirmationResult == true)
            {
                //inserting the success Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
            }
            else
            {
                //inserting the Error Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            }

            //returning the message through Json
            return Json(new { Message = displayMessage });
        }

        /// <summary>
        /// Method will retrieve specified doctor details and will be send into the View
        /// </summary>
        /// <param name="id">DoctorId to be removed</param>
        /// <returns>DoctorModel to be removed</returns>
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult RemoveDoctor(int id)
        {
            //Getting info about Doctor using their ID
            DoctorRoleDTO doctorDTO = doctorManager.GetDoctorsById(id);

            //Mapping DTO into Model
            DoctorModel doctor = new DoctorModel
            {
                Id = doctorDTO.Id,
                Name = doctorDTO.Name,
                Charges = doctorDTO.Charges,
                DoctorSpecialityId = doctorDTO.DoctorSpecialityId,
                PhoneNo = doctorDTO.PhoneNo,
                WardId = doctorDTO.WardId,
                UserId = doctorDTO.UserId
            };

            return View(doctor);
        }

        /// <summary>
        /// Method will remove the doctor object from the DB
        /// </summary>
        /// <param name="doctor">DoctorModel type of doctor object will be sent from the view</param>
        /// <returns>Redirects to the home page</returns>
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RemoveDoctor(DoctorModel doctor)
        {
            //Getting the Confirmation result (Checking whether doctor object is removed from DB 
            //and returning a boolean result)
            bool confirmationResult = doctorManager.RemoveDoctor(doctor.Id);

            //Creating a String variable to hold the display result
            string displayMessage = string.Empty;
            if (confirmationResult == true)
            {
                //inserting the success Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
            }
            else
            {
                //inserting the Error Enum message to the string variable
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            }

            //returning the message through Json
            return Json(new { Message = displayMessage }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Details Method will retrieve specified doctor details and will be sent into the View
        /// </summary>
        /// <param name="id">DoctorId will be passed into the method to find the specified doctor details</param>
        /// <returns>Returning to the it's view</returns>
        [CustomAuthorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public ActionResult Details(int id)
        {

            //Getting info about Doctor using their ID
            DoctorDetailDTO doctorDTO = doctorManager.GetDoctorByUserIdForProfileDetail(id);

            //Mapping DTO into Model
            DoctorDetailModel doctor = new DoctorDetailModel
            {
                Id = doctorDTO.Id,
                Name = doctorDTO.Name,
                DoctorSpecialityField = doctorDTO.DoctorSpecialityField,
                PhoneNo = doctorDTO.PhoneNo,
                WardId = doctorDTO.WardId
            };

            return View(doctor);
        }

        /// <summary>
        /// PatientDetails will prompt details about each patient in Index
        /// </summary>
        /// <param name="id">Patient ID</param>
        [CustomAuthorize(Roles = "Doctor")]
        [HttpGet]
        public ActionResult PatientDoctorDetails(int id)
        {
            //Declaring PatientManager class through IPatientManager Interface
            IPatientManager patientDetailsManager = new PatientManager();

            //Getting info about Patient using their ID
            PatientDTO PatientDTO = patientDetailsManager.ViewPatientDetails(id);

            //Mapping DTO into Model
            PatientModel patient = new PatientModel
            {
                //Id = PatientDTO.Id,
                Name = PatientDTO.Name,
                Address = PatientDTO.Address,
                NIC = PatientDTO.NIC,
                Gender = PatientDTO.Gender,
                MobileNo = PatientDTO.MobileNo
            };

            if (patient == null)
            {
                //page will be redirected to an Error page
                return RedirectToAction(ERROR_TYPE, ERROR_MESSAGE,
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            return View(patient);
        }

        /// <summary>
        /// TransferDoctor
        /// </summary>
        /// <param name="id">PatientId is passed here, patient who is to be transfered to another doctor</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Doctor")]
        [HttpGet]
        public ActionResult TransferDoctor(int? id)
        {

            #region getting Doctors into a DropDownList

            //Getting all Doctor elements into a list
            IEnumerable<DoctorModel> doctorList = RetrieveDoctors();

            //Creating a DropDown List with DoctorNames and IDs
            List<SelectListItem> doctorReturningValues = new List<SelectListItem>();

            foreach (var i in doctorList)
            {
                doctorReturningValues.Add(new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }

            //Putting DropDown to a ViewBag
            ViewBag.doctorViewbag = doctorReturningValues.ToList();
            #endregion


            //Creating an object and passing it to the view to show the patientId in the textBox
            DoctorRecommendationModel doctor = new DoctorRecommendationModel
            {
                PatientId = (int)id
            };
            return View(doctor);
        }

        /// <summary>
        /// Doctor's patient transfering details are stored in the DB
        /// </summary>
        /// <param name="details">Transfer Patient details</param>
        /// <returns>Redirecting to the HomePage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransferDoctor(DoctorRecommendationModel details)
        {
            if (ModelState.IsValid)
            {
                DoctorRecommendationDTO doctorDTO = new DoctorRecommendationDTO
                {
                    Id = details.Id,
                    CurrentDoctorId = details.CurrentDoctorId,
                    Reason = details.Reason,
                    RecomendedDoctorId = details.RecomendedDoctorId,
                    PatientId = details.PatientId
                };

                doctorManager.TransferDoctor(doctorDTO);
                return RedirectToAction("HomePage", "Doctor");
            }
            return View(details);

        }

        /// <summary>
        /// This method will be directing to the RecommendDoctors View, all the action in the page is handled by jQuery
        /// </summary>
        [CustomAuthorize(Roles = "Doctor")]
        public ActionResult RecommendDoctors()
        {
            IEnumerable<DoctorDetailModel> doctorList = RetrieveDoctorsWithSpecializedField();
            return View(doctorList);
        }

        /// <summary>
        /// This method will decide which doctor Speciality Field to be load in the partial view
        /// </summary>
        /// <param name="filter">Getting an integer variable from the jQuery POST function</param>
        /// <returns>Returning DoctorDetail type of List to the View</returns>
        [CustomAuthorize(Roles = "Doctor")]
        [HttpPost]
        public ActionResult RenderDoctorGrid(int filter)
        {
            //Getting all Doctors
            IEnumerable<DoctorDetailModel> doctorListSelect = RetrieveDoctorsWithSpecializedField();

            if (filter == 0)
            {
                //Getting all Doctor elements to a List
                IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();

                //Mapping DTO into Model
                IEnumerable<DoctorDetailModel> doctorList = doctorDTOList.Select(c => new DoctorDetailModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    DoctorSpecialityField = c.SpecializeArea,
                    PhoneNo = c.PhoneNo,
                    WardId = c.WardId
                }).ToList();

                //Returning doctor details list to the partial view to load in it's table template
                return PartialView("DoctorDetails", doctorList);

            }
            else if (filter == 1)
            {
                //Getting all Dentists
                IEnumerable<DoctorDetailModel> dentists = GetDoctorsForEachSpecialityField("Dentists");

                //Returning Dentists list to the partial view
                return PartialView("DoctorDetails", dentists);
            }
            else if (filter == 2)
            {

                //Getting all physicians
                IEnumerable<DoctorDetailModel> physicians = GetDoctorsForEachSpecialityField("Physicians");

                //Returning physicians list to the partial view
                return PartialView("DoctorDetails", physicians);
            }
            else if (filter == 3)
            {
                //Getting all surgeons
                IEnumerable<DoctorDetailModel> surgeons = GetDoctorsForEachSpecialityField("Surgeons");

                //Returning surgeons list to the partial view
                return PartialView("DoctorDetails", surgeons);
            }
            else if (filter == 4)
            {

                //Getting all radiologists
                IEnumerable<DoctorDetailModel> radiologists = GetDoctorsForEachSpecialityField("Radiologists");

                //Returning radiologists list to the partial view
                return PartialView("DoctorDetails", radiologists);
            }
            else if (filter == 5)
            {
                //Getting all neurologists
                IEnumerable<DoctorDetailModel> neurologists = GetDoctorsForEachSpecialityField("Neurologists");

                //Returning neurologists list to the partial view
                return PartialView("DoctorDetails", neurologists);
            }
            else if (filter == 6)
            {
                //Getting all neurosurgeons
                IEnumerable<DoctorDetailModel> neurosurgeons = GetDoctorsForEachSpecialityField("Neurosurgeons");

                //Returning neurosurgeons list to the partial view
                return PartialView("DoctorDetails", neurosurgeons);
            }

            return null;
        }

        #endregion


        #region Private Methods
        /// <summary>
        /// Details about Doctors in each Specialized Field will be retrieved from this method
        /// </summary>
        /// <param name="SpecializedField">Each Doctor Speciality Field will be passed into the parameter as a string variable</param>
        /// <returns></returns>
        private IEnumerable<DoctorDetailModel> GetDoctorsForEachSpecialityField(string SpecializedField)
        {
            //Get All Doctors From DB
            IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();

            //Using the parameter string and finding the specified Doctors in their field
            var doctorList = doctorDTOList.
                             Where(c => c.SpecializeArea == SpecializedField).
                             Select(c => new DoctorDetailModel
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 DoctorSpecialityField = c.SpecializeArea,
                                 PhoneNo = c.PhoneNo,
                                 WardId = c.WardId
                             }).ToList();

            return doctorList;
        }


        /// <summary>
        /// Retrieving all doctordetails
        /// </summary>
        /// <returns>DoctorModel type of list is been returned from the method</returns>
        private IEnumerable<DoctorModel> RetrieveDoctors()
        {
            //Get All Doctors From DB
            IEnumerable<DoctorRoleDTO> doctorDTOlist = doctorManager.GetDoctors();

            //Mapping DTO to Models
            IEnumerable<DoctorModel> doctorList = doctorDTOlist.Select(c => new DoctorModel
            {
                Id = c.Id,
                Name = c.Name,
                Charges = c.Charges,
                PhoneNo = c.PhoneNo,
                DoctorSpecialityId = c.DoctorSpecialityId,
                WardId = c.WardId
            });

            return doctorList;
        }

        /// <summary>
        /// Method will take all doctor details from DB and will be mapped into Doctor Model
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DoctorDetailModel> RetrieveDoctorsWithSpecializedField()
        {
            //Getting all doctor detials from the Business Layer
            IEnumerable<DoctorRoleDTO> doctorDTOlist = doctorManager.GetDoctors();

            //Mapping DTO into Model
            IEnumerable<DoctorDetailModel> doctorList = doctorDTOlist.Select(c => new DoctorDetailModel
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNo = c.PhoneNo,
                DoctorSpecialityField = c.SpecializeArea,
                WardId = c.WardId
            });

            return doctorList;
        }

        #endregion
    }
}