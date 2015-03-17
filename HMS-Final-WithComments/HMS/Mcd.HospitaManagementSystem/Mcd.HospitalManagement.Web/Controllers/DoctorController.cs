﻿using Mcd.HospitalManagement.Web.Enums;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitaManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class DoctorController : BaseController
    {
        #region Private fields
        private IDoctorManager doctorManager;
        private IPatientManager patientManager;
        private IPatientManager patientDetailsManager;
        private IUserManager userManager;
        private IWards wardManager;
        #endregion


        #region Constructors

        public DoctorController()
            : base()
        {
            this.doctorManager = new DoctorManager();
            this.patientManager = new PatientManager();
            this.patientDetailsManager = new PatientManager();
            this.userManager = new UserManager();
            this.wardManager = new WardManager();

        }
        public DoctorController(IDoctorManager repo)
        {
            this.doctorManager = repo;
        }
        #endregion

        #region ActionMethods

        public ActionResult HomePage()
        {
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

        }


        /// <summary>
        /// returning to the view
        /// </summary>
        /// <returns>ReDirecting to it's View With doctor's patient list</returns>
        public ActionResult Index()
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {
                //getting session UserId to a variable
                int userId = Convert.ToInt32(Session["USERID"]);

                ////Sending the userId and getting patient details that each Doctor 
                IEnumerable<PatientDoctorDTO> patientDTOList = patientDetailsManager.GetAllPatientDetailsForDoctor(userId);
                IEnumerable<PatientDoctorModel> patientList = from c in patientDTOList
                                                              select new PatientDoctorModel
                                                              {
                                                                  Id = c.Id,
                                                                  Name = c.Name,
                                                                  AdmitDate = c.AdmitDate,
                                                                  Gender = c.Gender,
                                                                  Ward = c.Ward,
                                                                  IsDischarged = c.IsDischarged,
                                                                  Bed = c.Bed
                                                              };

                //IEnumerable<PatientDoctorDTO> patientList = patientDTOList.Select(m => new PatientDoctorModel
                //{
                //    Id = m.Id,
                //    Name = m.Name,
                //    AdmitDate = m.AdmitDate,
                //    Gender = m.Gender,
                //    Bed = m.Bed,
                //    Ward = m.Ward,
                //    IsDischarged = m.IsDischarged
                //});
                return View(patientList);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

        }

        /// <summary>
        /// Retrieving All Doctors from the DB and returning it to the View
        /// </summary>
        /// <returns>Returns DoctorModel Type of List to the View</returns>
        public ActionResult GetDoctors()
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                //getting all Doctor elements from DB
                IEnumerable<DoctorModel> doctorList = RetrieveDoctors();
                //IEnumerable<DoctorModel> doctorList = GetFakeDoctorsList();
                return View(doctorList);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// AddDoctor HTTPGET method will direct into it's view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddDoctor()
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {

                #region getUsers into a dropDownList
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
            else
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied)});
            }
        }

        /// <summary>
        /// AddDoctor HTTPPOST method will retrieve all doctor details and will be sent to the DB to store
        /// </summary>
        /// <param name="doctor">AddDoctor view will be Posting a DoctorModel which will be then stored in the DB</param>
        /// <returns></returns>
        public ActionResult AddDoctor(DoctorModel doctor)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            


            //Mapping DTO into Model
            DoctorRoleDTO model = new DoctorRoleDTO
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
                return Json(new { Message = doctorExistsMessage });
            }


            //Getting the Confirmation result (Checking whether doctor object is added into the DB 
            //and returning a boolean result)
            bool confirmationResult = doctorManager.AddDoctor(model);

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
        [HttpGet]
        public ActionResult UpdateDoctor(int id)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)           
            {
                //Getting info about Doctor using their ID
                DoctorRoleDTO DTOObj = doctorManager.GetDoctorsById(id);

                //Mapping DTO into Model
                DoctorModel doctor = new DoctorModel
                {
                    Id = DTOObj.Id,
                    Name = DTOObj.Name,
                    Charges = DTOObj.Charges,
                    DoctorSpecialityId = DTOObj.DoctorSpecialityId,
                    PhoneNo = DTOObj.PhoneNo,
                    WardId = DTOObj.WardId,
                    UserId = DTOObj.UserId
                };

                return View(doctor);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// UpdateDoctor HTTPPOST method will send the updated doctor details to the DB
        /// </summary>
        /// <param name="doctor">Modified doctor details</param>
        /// <returns>Redirecting the View To HomePage</returns>
        //[HttpPost]
        public ActionResult UpdateDoctor(DoctorModel doctor)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            //Mapping Model object into DTO to send it to the Business Layer
            DoctorRoleDTO model = new DoctorRoleDTO
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
            bool confirmationResult = doctorManager.UpdateDoctor(model);

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
        [HttpGet]
        public ActionResult RemoveDoctor(int id)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                //Getting info about Doctor using their ID
                DoctorRoleDTO doctorDTO = doctorManager.GetDoctorsById(id);

                //Mapping DTO into Model
                DoctorModel doctorObj = new DoctorModel
                {
                    Id = doctorDTO.Id,
                    Name = doctorDTO.Name,
                    Charges = doctorDTO.Charges,
                    DoctorSpecialityId = doctorDTO.DoctorSpecialityId,
                    PhoneNo = doctorDTO.PhoneNo,
                    WardId = doctorDTO.WardId,
                    UserId = doctorDTO.UserId
                };

                return View(doctorObj);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Method will remove the doctor object from the DB
        /// </summary>
        /// <param name="doctor">DoctorModel type of doctor object will be sent from the view</param>
        /// <returns>Redirects to the home page</returns>
        [HttpPost]
        public ActionResult RemoveDoctor(DoctorModel doctor)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

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
        [HttpGet]
        public ActionResult Details(int id)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
                (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {
                //DoctorDetailDTO doctorDTO = repo.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(id);

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
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// PatientDetails will prompt details about each patient in Index
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PatientDoctorDetails(int id)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {

                //Getting info about Patient using their ID
                PatientDTO PatientDTO = patientManager.ViewPatientDetails(id);

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
                    return HttpNotFound();
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// TransferDoctor
        /// </summary>
        /// <param name="id">PatientId is passed here, patient who is to be transfered to another doctor</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TransferDoctor(int? id)
        {
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {

                #region getting Doctors into a DropDownList
                //Getting all Doctor elements into a list
                IEnumerable<DoctorModel> doctorList = RetrieveDoctors();
                //IEnumerable<DoctorModel> doctorList = GetFakeDoctorsList();

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


                //#region getting patients into a DropDownList

                ////Getting all patient elements into a list
                //IEnumerable<PatientDTO> patientList = patientManager.ViewAllPatientDetails();

                ////Creating a DropDown List with patientNames and IDs
                //List<SelectListItem> patientReturningValues = new List<SelectListItem>();

                //foreach (var i in patientList)
                //{
                //    patientReturningValues.Add(new SelectListItem
                //    {
                //        Text = i.Name,
                //        Value = i.Id.ToString()
                //    });
                //}

                ////Putting DropDown to a ViewBag
                //ViewBag.patientViewbag = patientReturningValues.ToList();
                //#endregion

                //creating an object because we need to use the patient id
                DoctorRecommendationModel doctorObj = new DoctorRecommendationModel
                {
                    PatientId = (int)id
                };
                return View(doctorObj);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
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
            //checking the permissions
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", 
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

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
        public ActionResult RecommendDoctors()
        {
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {
                IEnumerable<DoctorDetailModel> doctorList = RetrieveDoctorsWithSpecializedField();
                //IEnumerable<DoctorDetailModel> doctorList = GetFakeDoctorsList();

                //List<SelectListItem> doctorReturningValues = new List<SelectListItem>();
                //doctorReturningValues.Add(new SelectListItem { Text = "All", Value = "0" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Dentists", Value = "1" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Physicians", Value = "2" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Surgeons", Value = "3" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Radiologists", Value = "4" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Neurologists", Value = "5" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Neurosurgeons", Value = "6" });

                //ViewBag.doctorViewbag = doctorReturningValues.ToList();

                return View(doctorList);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error",
                    new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter">Getting an integer variable from the jQuery POST function</param>
        /// <returns>Returning DoctorDetail type of List to the View</returns>
        [HttpPost]
        public ActionResult RenderDoctorGrid(int filter)
        {
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {
                //Getting all Doctors
                IEnumerable<DoctorDetailModel> doctorListSelect = RetrieveDoctorsWithSpecializedField();

                //List<SelectListItem> doctorReturningValues = new List<SelectListItem>();
                //doctorReturningValues.Add(new SelectListItem { Text = "All", Value = "0" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Dentists", Value = "1" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Physicians", Value = "2" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Surgeons", Value = "3" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Radiologists", Value = "4" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Neurologists", Value = "5" });
                //doctorReturningValues.Add(new SelectListItem { Text = "Neurosurgeons", Value = "6" });

                //ViewBag.doctorViewbag = doctorReturningValues.ToList();
                if (filter == 0)
                {
                    //Getting all Doctor elements to a List
                    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
                    //var doctorList = from c in doctorDTOList
                    //                 select new DoctorDetailModel
                    //                 {
                    //                     Id = c.Id,
                    //                     Name = c.Name,
                    //                     DoctorSpecialityField = c.DoctorSpecialityField,
                    //                     PhoneNo = c.PhoneNo,
                    //                     WardId = c.WardId
                    //                 };

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
                    //IEnumerable<DoctorRoleDTO> doctorDTOList = repo.GetDoctors();
                    //var dentists = from c in doctorDTOList
                    //               where c.DoctorSpecialityField == "Dentists"
                    //               select new DoctorDetailModel
                    //               {
                    //                   Id = c.Id,
                    //                   Name = c.Name,
                    //                   DoctorSpecialityField = c.DoctorSpecialityField,
                    //                   PhoneNo = c.PhoneNo,
                    //                   WardId = c.WardId
                    //               };

                    //var dentists = doctorDTOList.
                    //                Where(c => c.SpecializeArea == "Dentists").
                    //                Select(c => new DoctorDetailModel
                    //                {
                    //                    Id = c.Id,
                    //                    Name = c.Name,
                    //                    DoctorSpecialityField = c.SpecializeArea,
                    //                    PhoneNo = c.PhoneNo,
                    //                    WardId = c.WardId
                    //                }).ToList();

                    //Getting all Dentists
                    IEnumerable<DoctorDetailModel> dentists = GetDoctorsForEachSpecialityField("Dentists");

                    //Returning Dentists list to the partial view
                    return PartialView("DoctorDetails", dentists);
                }
                else if (filter == 2)
                {
                    //IEnumerable<DoctorRoleDTO> doctorDTOList = repo.GetDoctors();
                    //var physicians = from c in doctorDTOList
                    //                 where c.DoctorSpecialityField == "Physicians"
                    //                 select new DoctorDetailModel
                    //                 {
                    //                     Id = c.Id,
                    //                     Name = c.Name,
                    //                     DoctorSpecialityField = c.DoctorSpecialityField,
                    //                     PhoneNo = c.PhoneNo,
                    //                     WardId = c.WardId
                    //                 };

                    //var physicians = doctorDTOList.
                    //                Where(c => c.SpecializeArea == "Physicians").
                    //                Select(c => new DoctorDetailModel
                    //                {
                    //                    Id = c.Id,
                    //                    Name = c.Name,
                    //                    DoctorSpecialityField = c.SpecializeArea,
                    //                    PhoneNo = c.PhoneNo,
                    //                    WardId = c.WardId
                    //                }).ToList();

                    //Getting all physicians
                    IEnumerable<DoctorDetailModel> physicians = GetDoctorsForEachSpecialityField("Physicians");

                    //Returning physicians list to the partial view
                    return PartialView("DoctorDetails", physicians);
                }
                else if (filter == 3)
                {
                    //IEnumerable<DoctorRoleDTO> doctorDTOList = repo.GetDoctors();
                    //var surgeons = from c in doctorDTOList
                    //               where c.DoctorSpecialityField == "Surgeons"
                    //               select new DoctorDetailModel
                    //               {
                    //                   Id = c.Id,
                    //                   Name = c.Name,
                    //                   DoctorSpecialityField = c.DoctorSpecialityField,
                    //                   PhoneNo = c.PhoneNo,
                    //                   WardId = c.WardId
                    //               };

                    //var surgeons = doctorDTOList.
                    //                Where(c => c.SpecializeArea == "Surgeons").
                    //                Select(c => new DoctorDetailModel
                    //                {
                    //                    Id = c.Id,
                    //                    Name = c.Name,
                    //                    DoctorSpecialityField = c.SpecializeArea,
                    //                    PhoneNo = c.PhoneNo,
                    //                    WardId = c.WardId
                    //                }).ToList();

                    //Getting all surgeons
                    IEnumerable<DoctorDetailModel> surgeons = GetDoctorsForEachSpecialityField("Surgeons");

                    //Returning surgeons list to the partial view
                    return PartialView("DoctorDetails", surgeons);
                }
                else if (filter == 4)
                {
                    //IEnumerable<DoctorRoleDTO> doctorDTOList = repo.GetDoctors();
                    //var radiologists = from c in doctorDTOList
                    //                   where c.DoctorSpecialityField == "Radiologists"
                    //                   select new DoctorDetailModel
                    //                   {
                    //                       Id = c.Id,
                    //                       Name = c.Name,
                    //                       DoctorSpecialityField = c.DoctorSpecialityField,
                    //                       PhoneNo = c.PhoneNo,
                    //                       WardId = c.WardId
                    //                   };

                    //var radiologists = doctorDTOList.
                    //                    Where(c => c.SpecializeArea == "Radiologists").
                    //                    Select(c => new DoctorDetailModel
                    //                    {
                    //                        Id = c.Id,
                    //                        Name = c.Name,
                    //                        DoctorSpecialityField = c.SpecializeArea,
                    //                        PhoneNo = c.PhoneNo,
                    //                        WardId = c.WardId
                    //                    }).ToList();

                    //Getting all radiologists
                    IEnumerable<DoctorDetailModel> radiologists = GetDoctorsForEachSpecialityField("Radiologists");

                    //Returning radiologists list to the partial view
                    return PartialView("DoctorDetails", radiologists);
                }
                else if (filter == 5)
                {
                    //IEnumerable<DoctorRoleDTO> doctorDTOList = repo.GetDoctors();
                    //var neurologists = from c in doctorDTOList
                    //                   where c.DoctorSpecialityField == "Neurologists"
                    //                   select new DoctorDetailModel
                    //                   {
                    //                       Id = c.Id,
                    //                       Name = c.Name,
                    //                       DoctorSpecialityField = c.DoctorSpecialityField,
                    //                       PhoneNo = c.PhoneNo,
                    //                       WardId = c.WardId
                    //                   };

                    //var neurologists = doctorDTOList.
                    //                    Where(c => c.SpecializeArea == "Neurologists").
                    //                    Select(c => new DoctorDetailModel
                    //                    {
                    //                        Id = c.Id,
                    //                        Name = c.Name,
                    //                        DoctorSpecialityField = c.SpecializeArea,
                    //                        PhoneNo = c.PhoneNo,
                    //                        WardId = c.WardId
                    //                    }).ToList();

                    //Getting all neurologists
                    IEnumerable<DoctorDetailModel> neurologists = GetDoctorsForEachSpecialityField("Neurologists");

                    //Returning neurologists list to the partial view
                    return PartialView("DoctorDetails", neurologists);
                }
                else if (filter == 6)
                {
                    //IEnumerable<DoctorRoleDTO> doctorDTOList = repo.GetDoctors();
                    //var neurosurgeons = from c in doctorDTOList
                    //                    where c.DoctorSpecialityField == "Neurosurgeons"
                    //                    select new DoctorDetailModel
                    //                    {
                    //                        Id = c.Id,
                    //                        Name = c.Name,
                    //                        DoctorSpecialityField = c.DoctorSpecialityField,
                    //                        PhoneNo = c.PhoneNo,
                    //                        WardId = c.WardId
                    //                    };

                    //var neurosurgeons = doctorDTOList.
                    //                    Where(c => c.SpecializeArea == "Neurosurgeons").
                    //                    Select(c => new DoctorDetailModel
                    //                    {
                    //                        Id = c.Id,
                    //                        Name = c.Name,
                    //                        DoctorSpecialityField = c.SpecializeArea,
                    //                        PhoneNo = c.PhoneNo,
                    //                        WardId = c.WardId
                    //                    }).ToList();

                    //Getting all neurosurgeons
                    IEnumerable<DoctorDetailModel> neurosurgeons = GetDoctorsForEachSpecialityField("Neurosurgeons");

                    //Returning neurosurgeons list to the partial view
                    return PartialView("DoctorDetails", neurosurgeons);
                }

                return null;
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
            //return Json(filter);
        }

        #endregion



        #region JsonMethods

        ///// <summary>
        ///// Retrieving all DoctorDetails and sending just dentists to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing dentists doctor details</returns>
        //public JsonResult getJsonDentists()
        //{
        //    //List<DoctorDetailModel> dentists = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - Dentists", DoctorSpecialityField = "Dentists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - Dentists", DoctorSpecialityField = "Dentists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - Dentists", DoctorSpecialityField = "Dentists", PhoneNo= "0786904432", WardId = 1}                
        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var dentists = from c in doctorDTOList
        //                   where c.SpecializeArea == "Dentists"
        //                   select new DoctorDetailModel
        //                   {
        //                       Id = c.Id,
        //                       Name = c.Name,
        //                       DoctorSpecialityField = c.SpecializeArea,
        //                       PhoneNo = c.PhoneNo,
        //                       WardId = c.WardId
        //                   };
        //    return Json(dentists, JsonRequestBehavior.AllowGet);
        //    //return ("part",skksdv);
        //}
        ///// <summary>
        ///// Retrieving all DoctorDetails and sending all details to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing all doctor details</returns>
        //public JsonResult getJsonAllDoctors()
        //{
        //    //List<DoctorDetailModel> all = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - Dentists", DoctorSpecialityField = "Dentists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - Dentists", DoctorSpecialityField = "Dentists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - Dentists", DoctorSpecialityField = "Dentists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - Neurosurgeons", DoctorSpecialityField = "Neurosurgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - Neurosurgeons", DoctorSpecialityField = "Neurosurgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - Neurosurgeons", DoctorSpecialityField = "Neurosurgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - physicians", DoctorSpecialityField = "physicians", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - physicians", DoctorSpecialityField = "physicians", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - physicians", DoctorSpecialityField = "physicians", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - surgeons", DoctorSpecialityField = "surgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - surgeons", DoctorSpecialityField = "surgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - surgeons", DoctorSpecialityField = "surgeons", PhoneNo= "0786904432", WardId = 1}

        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var doctorList = from c in doctorDTOList
        //                     select new DoctorDetailModel
        //                     {
        //                         Id = c.Id,
        //                         Name = c.Name,
        //                         DoctorSpecialityField = c.SpecializeArea,
        //                         PhoneNo = c.PhoneNo,
        //                         WardId = c.WardId
        //                     };
        //    return Json(doctorList, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Retrieving all DoctorDetails and sending just physicians to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing physicians doctor details</returns>
        //public JsonResult getJsonPhysicians()
        //{
        //    //List<DoctorDetailModel> physicians = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - physicians", DoctorSpecialityField = "physicians", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - physicians", DoctorSpecialityField = "physicians", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - physicians", DoctorSpecialityField = "physicians", PhoneNo= "0786904432", WardId = 1}                
        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var physicians = from c in doctorDTOList
        //                     where c.SpecializeArea == "Physicians"
        //                     select new DoctorDetailModel
        //                     {
        //                         Id = c.Id,
        //                         Name = c.Name,
        //                         DoctorSpecialityField = c.SpecializeArea,
        //                         PhoneNo = c.PhoneNo,
        //                         WardId = c.WardId
        //                     };
        //    return Json(physicians, JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// Retrieving all DoctorDetails and sending just surgeons to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing surgeons doctor details</returns>
        //public JsonResult getJsonSurgeons()
        //{
        //    //List<DoctorDetailModel> surgeons = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - surgeons", DoctorSpecialityField = "surgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - surgeons", DoctorSpecialityField = "surgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - surgeons", DoctorSpecialityField = "surgeons", PhoneNo= "0786904432", WardId = 1}                
        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var surgeons = from c in doctorDTOList
        //                   where c.SpecializeArea == "Surgeons"
        //                   select new DoctorDetailModel
        //                   {
        //                       Id = c.Id,
        //                       Name = c.Name,
        //                       DoctorSpecialityField = c.SpecializeArea,
        //                       PhoneNo = c.PhoneNo,
        //                       WardId = c.WardId
        //                   };
        //    return Json(surgeons, JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// Retrieving all DoctorDetails and sending just Radiologists to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing Radiologists doctor details</returns>
        //public JsonResult getJsonRadiologists()
        //{
        //    //List<DoctorDetailModel> radiologists = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - Radiologists", DoctorSpecialityField = "Radiologists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - Radiologists", DoctorSpecialityField = "Radiologists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - Radiologists", DoctorSpecialityField = "Radiologists", PhoneNo= "0786904432", WardId = 1}                
        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var radiologists = from c in doctorDTOList
        //                       where c.SpecializeArea == "Radiologists"
        //                       select new DoctorDetailModel
        //                       {
        //                           Id = c.Id,
        //                           Name = c.Name,
        //                           DoctorSpecialityField = c.SpecializeArea,
        //                           PhoneNo = c.PhoneNo,
        //                           WardId = c.WardId
        //                       };
        //    return Json(radiologists, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Retrieving all DoctorDetails and sending just Neurologists to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing Neurologists doctor details</returns>
        //public JsonResult getJsonNeurologists()
        //{
        //    //List<DoctorDetailModel> neurologists = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - Neurologists", DoctorSpecialityField = "Neurologists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - Neurologists", DoctorSpecialityField = "Neurologists", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - Neurologists", DoctorSpecialityField = "Neurologists", PhoneNo= "0786904432", WardId = 1}                
        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var neurologists = from c in doctorDTOList
        //                       where c.SpecializeArea == "Neurologists"
        //                       select new DoctorDetailModel
        //                       {
        //                           Id = c.Id,
        //                           Name = c.Name,
        //                           DoctorSpecialityField = c.SpecializeArea,
        //                           PhoneNo = c.PhoneNo,
        //                           WardId = c.WardId
        //                       };
        //    return Json(neurologists, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Retrieving all DoctorDetails and sending just Neurosurgeons to the view
        ///// </summary>
        ///// <returns>Json result will be sent to the view containing Neurosurgeons doctor details</returns>
        //public JsonResult getJsonNeurosurgeons()
        //{
        //    //List<DoctorDetailModel> neurosurgeons = new List<DoctorDetailModel> 
        //    //{ 
        //    //    new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis - Neurosurgeons", DoctorSpecialityField = "Neurosurgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Dayan Mendis - Neurosurgeons",DoctorSpecialityField = "Neurosurgeons", PhoneNo= "0786904432", WardId = 1},
        //    //    new DoctorDetailModel{Id = 1, Name = "Ewerdney Mendis - Neurosurgeons",DoctorSpecialityField = "Neurosurgeons", PhoneNo= "0786904432", WardId = 1}                
        //    //};
        //    IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();
        //    var neurosurgeons = from c in doctorDTOList
        //                        where c.SpecializeArea == "Neurosurgeons"
        //                        select new DoctorDetailModel
        //                        {
        //                            Id = c.Id,
        //                            Name = c.Name,
        //                            DoctorSpecialityField = c.SpecializeArea,
        //                            PhoneNo = c.PhoneNo,
        //                            WardId = c.WardId
        //                        };
        //    return Json(neurosurgeons, JsonRequestBehavior.AllowGet);
        //}
        #endregion


        #region Private Methods

        private IEnumerable<DoctorDetailModel> GetDoctorsForEachSpecialityField(string SpecializedField)
        {
            //Get All Doctors From DB
            IEnumerable<DoctorRoleDTO> doctorDTOList = doctorManager.GetDoctors();

            //Using parameter string finding the specified Doctors in their field
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
            IEnumerable<DoctorRoleDTO> list = doctorManager.GetDoctors();

            //Mapping DTO to Models
            IEnumerable<DoctorModel> dlist = from c in list
                                             select new DoctorModel
                                             {
                                                 Id = c.Id,
                                                 Name = c.Name,
                                                 Charges = c.Charges,
                                                 PhoneNo = c.PhoneNo,
                                                 DoctorSpecialityId = c.DoctorSpecialityId,
                                                 WardId = c.WardId
                                             };
            return dlist;
        }
        private IEnumerable<DoctorDetailModel> RetrieveDoctorsWithSpecializedField()
        {
            IEnumerable<DoctorRoleDTO> list = doctorManager.GetDoctors();
            IEnumerable<DoctorDetailModel> dlist = from c in list
                                                   select new DoctorDetailModel
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       PhoneNo = c.PhoneNo,
                                                       DoctorSpecialityField = c.SpecializeArea,
                                                       WardId = c.WardId
                                                   };
            return dlist;
        }
        private IEnumerable<DoctorDetailModel> GetFakeDoctorsList()
        {
            List<DoctorDetailModel> list = new List<DoctorDetailModel>
            {
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                new DoctorDetailModel{Id = 1, Name = "Neranjan Mendis", DoctorSpecialityField = "dentists", PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 1, Name = "Neranjan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1},
                //new DoctorModel{Id = 2, Name = "Dayan Mendis", Charges = 12000, DoctorSpecialityId = 1, PhoneNo= "0786904432", WardId = 1}
            };
            IEnumerable<DoctorDetailModel> plist = list;
            return plist;
        }
        //private IEnumerable<PatientDoctorModel> RetrieveFakePatients()
        //{
        //    List<PatientDoctorModel> list = new List<PatientDoctorModel>
        //    {
        //        new PatientDoctorModel{Id = 2, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Neranjan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //        new PatientDoctorModel{Id = 1, AdmitDate = "20-10-2015", Bed = 1, Gender = "Male", Name = "Dayan mendis", IsDischarged = 1 , Ward = 1},
        //    };
        //    IEnumerable<PatientDoctorModel> plist = list;
        //    return plist;
        //}
        #endregion
    }
}