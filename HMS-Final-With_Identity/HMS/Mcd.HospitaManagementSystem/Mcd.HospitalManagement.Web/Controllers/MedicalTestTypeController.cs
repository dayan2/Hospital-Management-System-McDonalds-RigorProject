﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitalManagement.Web.Enums;
#endregion

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class MedicalTestTypeController : BaseController
    {
        #region Constant
        const string ERROR_TYPE = "AccessDenied";
        const string ERROR_MSG = "Error";
        #endregion

        #region Private Fields
        private IHMSMedicalTestType dbMedical;
        private IPatientMedicalTestAllocation dbPatientMedical;
        string displayMessage = string.Empty;
        #endregion

        #region Constructor Method
        public MedicalTestTypeController()
            : base()
        {
            dbMedical = new HMSMedicalTestType();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get Medical Test details
        /// </summary>
        /// <returns>Return Index View</returns>
        public ActionResult Index()
        {
            if (Session["USERROLE"] == null)// checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                //Get Medical Test details
                var medicalTestType = dbMedical.ViewtMedicalTestType();
                IEnumerable<MedicalTestTypeModel> medical = medicalTestType.Select(c =>
                                                             new MedicalTestTypeModel
                                              {
                                                  Id = c.Id,
                                                  Description = c.Description
                                              }).ToList();
                return View(medical);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Get Details by id.
        /// </summary>
        /// <param name="id">MedicalTestType</param>
        /// <returns>Details view</returns>
        public ActionResult Details(int? id)
        {
            if (Session["USERROLE"] == null)// checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                if (id == null)// check have Medical Test type id, if no one then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                MedicalTestTypeDTO medicalTestTypeDto = dbMedical.ViewtMedicalTestTypeById(id); //Get Medical Test details by id
                if (medicalTestTypeDto == null) // check have object, if no object then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                MedicalTestTypeModel medicalTestType = new MedicalTestTypeModel { Id = medicalTestTypeDto.Id, Description = medicalTestTypeDto.Description };

                return View(medicalTestType);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Get Medical test details to create.
        /// </summary>
        /// <returns>Create view</returns>
        public ActionResult Create()
        {
            // checking the login state. if not login, then pass the error page.
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                return View();
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Send details to save.
        /// </summary>
        /// <param name="medicalTestType">MedicalTestTypeModel</param>
        /// <returns>Confirm message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description")] MedicalTestTypeModel medicalTestType)
        {
            MedicalTestTypeDTO MedicalTypeDto = new MedicalTestTypeDTO { Id = medicalTestType.Id, Description = medicalTestType.Description };
            if (ModelState.IsValid)
            {
                // save details. if saved, result should send to view using JSON.
                if (dbMedical.InsertMedicalTestType(MedicalTypeDto))
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
                return Json(new { Message = displayMessage });
            }

            return View(medicalTestType);
        }

        /// <summary>
        /// Get Medical Test Details.
        /// </summary>
        /// <param name="id">MedicalTestTypeId</param>
        /// <returns>Edit View</returns>
        public ActionResult Edit(int? id)
        {
            if (Session["USERROLE"] == null)// checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                if (id == null)// check have Medical Test type id, if no one then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                MedicalTestTypeDTO medicalTestTypeDTO = dbMedical.ViewtMedicalTestTypeById(id);
                if (medicalTestTypeDTO == null) // check have object, if no object then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                MedicalTestTypeModel medicalType = new MedicalTestTypeModel { Id = medicalTestTypeDTO.Id, Description = medicalTestTypeDTO.Description };
                return View(medicalType);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Edit  Medical Test type details.
        /// </summary>
        /// <param name="medicalTestType">MedicalTestTypeModel</param>
        /// <returns>Json result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] MedicalTestTypeModel medicalTestType)
        {
            MedicalTestTypeDTO medicalTypeDto = new MedicalTestTypeDTO { Id = medicalTestType.Id, Description = medicalTestType.Description };
            if (ModelState.IsValid) // validation model
            { 
                // get state of the Medical Test edited as a booleon value.
                if (dbMedical.EditMedicalTestType(medicalTypeDto))
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
                return Json(new { Message = displayMessage });
                //return RedirectToAction("Index");
            }
            return View(medicalTestType);
        }

        /// <summary>
        /// Get Medical test details to delete
        /// </summary>
        /// <param name="id">Medical test id</param>
        /// <returns>Medical test details</returns>
        public ActionResult Delete(int? id)
        {
            dbPatientMedical = new PatientMedicalTestAllocation();
            if (Session["USERROLE"] == null)// checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                if (id == null)// check have Medical Test type id, if no one then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                MedicalTestTypeDTO medicalTestTypeDto = dbMedical.ViewtMedicalTestTypeById(id);
                if (medicalTestTypeDto == null) // check have object, if no object then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                if (dbPatientMedical.ExitsMedicalInMedicalAllocation((int)id))
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PatientMedicalTestError.ExistsMedicalTestInMedicalAllocation) });
                }
                //mapping
                MedicalTestTypeModel medicalType = new MedicalTestTypeModel { Id = medicalTestTypeDto.Id, Description = medicalTestTypeDto.Description };
                return View(medicalType);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Remove Medical test by id
        /// </summary>
        /// <param name="id">MedicalTestTypeId</param>
        /// <returns>JSon Result</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // get state of the Medical Test deleted as a booleon value.
            if (dbMedical.DeleteMedicalTestType(id))
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
            else
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            return Json(new { Message = displayMessage });
        }

        #endregion
    }
}
