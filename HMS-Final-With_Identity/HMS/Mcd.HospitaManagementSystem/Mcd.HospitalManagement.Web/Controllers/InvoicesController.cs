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
    public class InvoicesController : BaseController
    {
        #region Constant
        const string ERROR_TYPE = "AccessDenied";
        const string ERROR_MSG = "Error";
        #endregion

        #region Private Fields
        IHMSInvoice dbInvoice;
        IPatientManager dbPatient;
        string displayMessage = string.Empty;
        #endregion

        #region Constructor Method

        public InvoicesController()
            : base()
        {
            dbInvoice = new HMSInvoice();
            dbPatient = new PatientManager();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get patientDetials and return to view. 
        /// </summary>
        /// <returns>return</returns>
        public ActionResult Index()
        {
            if (Session["USERROLE"] == null) // checking the login state.
            {   
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
               (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                //get admitted patient, not discharged one.
                IEnumerable<AllPatientDTO> allPatientDto = dbPatient.ViewAllPatientAdmissionDetails().Where(x => x.IsDischarged == false).Distinct();
                if (allPatientDto == null)  // check have any records, if no records then pass error page.
                {
                    // pass the error page
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                // assign patient data Dto to model.
                IEnumerable<PatientModel> PatientModel = allPatientDto.Select(x => new PatientModel
                {
                    Name = x.Name,
                    Id = x.PatientId,
                    Address = x.Address,
                    MobileNo = x.MobileNo
                });


                return View(PatientModel);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }


        /// <summary>
        /// Get invoice details by Invoice Id
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (Session["USERROLE"] == null) // checking the login state.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
               (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null) // check have any invoice id, if no one then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(id);
                if (invoiceDto == null) // check have any records, if no records then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                // assign Invoice data Dto to model.
                InvoiceDetailsModel InvoiceModel = new InvoiceDetailsModel
                {
                    InvoiceId = invoiceDto.Id,
                    InvoiceDate = invoiceDto.InvoiceDate,
                    PatientDetailId = invoiceDto.PatientDetailId
                };
                return View(InvoiceModel);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Get Details to make invoice by patient Id
        /// </summary>
        /// <param name="patientId">int? type</param>
        /// <returns>Invoice model details</returns>
        public ActionResult Create(int? patientId)
        {
            if (Session["USERROLE"] == null)    // checking the login state.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            // checking the permission.
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
              (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                InvoiceDetialsDTO invoiceDetialsDTO = dbInvoice.ViewInitialInoice(patientId);
                if (patientId == null || patientId == 0) // check have any patient id, if no one then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = "Object Not found" });
                }
                //check patient is admitted or discharged, if discharge pass error page.
                if (!dbInvoice.CheckPatientAvalabilityForMakeInvoice(patientId))
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = "Patient not avaible to make invoice, First make patient admission details" });
                }
                //validate dates, if admited date larger than invoice date then pass error.
                if (!dbInvoice.validateDatesForInvoices((DateTime)invoiceDetialsDTO.AdmitDate, (DateTime)invoiceDetialsDTO.InvoiceDate))
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = "Invoice date must be greater than Admited date" });
                }
                //mapping data Dto to Model.
                InvoiceDetailsModel invoiceModel = new InvoiceDetailsModel
                {
                    PatientName = invoiceDetialsDTO.PatientName,
                    WardNo = invoiceDetialsDTO.WardNo,
                    WardFee = invoiceDetialsDTO.WardFee,
                    InvoiceDate = invoiceDetialsDTO.InvoiceDate,
                    DoctorName = invoiceDetialsDTO.DoctorName,
                    DoctorCharges = invoiceDetialsDTO.DoctorCharges,
                    AdmitDate = invoiceDetialsDTO.AdmitDate,
                    PatientDetailId = invoiceDetialsDTO.PatientDetailId,
                    TotalFee = dbInvoice.CalculateInvoice(invoiceDetialsDTO)
                };

                return View(invoiceModel);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// send invoice details to save
        /// </summary>
        /// <param name="invoiceModel">InvoiceDetailsModel</param>
        /// <returns>Json output</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,PatientDetailId,InvoiceDate")] InvoiceDetailsModel invoiceModel)
        {
            if (ModelState.IsValid)
            {

                InvoiceDetialsDTO invoiceDto = new InvoiceDetialsDTO { Id = invoiceModel.InvoiceId, InvoiceDate = invoiceModel.InvoiceDate, PatientDetailId = invoiceModel.PatientDetailId };
                var result = dbInvoice.InsertInvoice(invoiceDto);// get state of the saved as booleon value.
                if (result)
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
                //patient should be discharged after creating invoice. this is the way updating patient details as Isdischarged=true.
                #region Update Patient Discharged staus
                AllPatientDTO patientDto = dbPatient.ViewPatientRelatedData(invoiceModel.PatientDetailId);
                patientDto.IsDischarged = true;
                if (result)
                    dbPatient.EditPatientAdmissionDetails(patientDto);
                #endregion update


                return Json(new { Message = displayMessage });  // return Json result

            }


            return View(invoiceModel);
        }


        /// <summary>
        /// this is the post method to delete record. meaning of the invoice cancelation is deleting record. 
        /// this table have only foriegn keys of the others.
        /// </summary>
        /// <param name="invoiceId">int type</param>
        /// <returns>Json result</returns>
        public ActionResult DeleteConfirmed(int invoiceId)
        {
            if (Session["USERROLE"] == null)        // checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(invoiceId);
            if (invoiceDto == null) // check have object, if no object then pass error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
            }
            if (dbInvoice.DeleteInvoice(invoiceId))
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
            else
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            return Json(new { Message = displayMessage });
        }

        /// <summary>
        /// Get Invoice details
        /// </summary>
        /// <returns>return to "ViewInvoice" View</returns>
        public ActionResult ViewInvoices()
        {
            if (Session["USERROLE"] == null)// checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
             (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                IEnumerable<InvoiceDetailsModel> invoiceModel = dbInvoice.ViewtInvoice()
                  .Select(m => new InvoiceDetailsModel
                  {
                      InvoiceId = m.Id,
                      InvoiceDate = m.InvoiceDate,
                      PatientName = m.PatientName,
                      WardNo = m.WardNo,
                      WardFee = m.WardFee,
                      DoctorName = m.DoctorName,
                      DoctorCharges = m.DoctorCharges,
                      AdmitDate = m.AdmitDate,
                      TotalFee = dbInvoice.CalculateInvoice(m)
                  }).ToList();

                return View(invoiceModel);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// This is cancellation part of the invoice.
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns>Delete View</returns>
        public ActionResult Delete(int? id)
        {
            if (Session["USERROLE"] == null)// checking the login state. if not login, then pass the error page.
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
             (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null) //// check have any invoice id, if no one then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(id); // Get invoice by id
                if (invoiceDto == null) // check have object, if no object then pass error page.
                {
                    return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                InvoiceDetailsModel InvoiceModel = new InvoiceDetailsModel //mapping data
                {
                    InvoiceId = invoiceDto.Id,
                    InvoiceDate = invoiceDto.InvoiceDate,
                    PatientDetailId = invoiceDto.PatientDetailId,
                    PatientName = invoiceDto.PatientName,
                    WardNo = invoiceDto.WardNo,
                    WardFee = invoiceDto.WardFee,
                    DoctorName = invoiceDto.DoctorName,
                    DoctorCharges = invoiceDto.DoctorCharges,
                    TotalFee = dbInvoice.CalculateInvoice(invoiceDto)
                };
                return View(InvoiceModel);
            }
            else
            {
                return RedirectToAction(ERROR_TYPE, ERROR_MSG, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }
        #endregion

    }
}
