#region Using Directives
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
    public class InvoicesController : Controller
    {
        #region Private Fields
        IHMSInvoice dbInvoice;
        IPatientManager dbPatient;
        string displayMessage = string.Empty;
        #endregion

        #region Constructor Method

        public InvoicesController()
        {
            dbInvoice = new HMSInvoice();
            dbPatient = new PatientManager();
        }

        #endregion

        #region Public Methods
        // GET: Invoices
        public ActionResult Index()
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
               (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                IEnumerable<AllPatientDTO> allPatientDto = dbPatient.ViewAllPatientAdmissionDetails().Where(x => x.IsDischarged == false).Distinct();
                if (allPatientDto == null)
                {

                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }

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
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }



        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
               (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null)
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(id);
                if (invoiceDto == null)
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }

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
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        // GET: Invoices/Create
        public ActionResult Create(int? patientId)
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
              (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                InvoiceDetialsDTO invoiceDetialsDTO = dbInvoice.ViewInitialInoice(patientId);
                if (patientId == null || patientId == 0)
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = "Object Not found" });
                }
                if (!dbInvoice.CheckPatientAvalabilityForMakeInvoice(patientId))
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = "Patient not avaible to make invoice, First make patient admission details" });
                }
                if (!dbInvoice.validateDatesForInvoices((DateTime)invoiceDetialsDTO.AdmitDate, (DateTime)invoiceDetialsDTO.InvoiceDate))
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = "Invoice date must be greater than Admited date" });
                }
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

                //ViewBag.PatientDetailId = new SelectList(dbPatient.ViewAllAdmissionPatientDetails(), "PatientDetailId", "PatientDetailId");
                return View(invoiceModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,PatientDetailId,InvoiceDate")] InvoiceDetailsModel invoiceModel)
        {
            if (ModelState.IsValid)
            {

                InvoiceDetialsDTO invoiceDto = new InvoiceDetialsDTO { Id = invoiceModel.InvoiceId, InvoiceDate = invoiceModel.InvoiceDate, PatientDetailId = invoiceModel.PatientDetailId };
                var result = dbInvoice.InsertInvoice(invoiceDto);
                if (result)
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                else
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);

                #region Update Patient Discharged staus
                AllPatientDTO patientDto = dbPatient.ViewPatientRelatedData(invoiceModel.PatientDetailId);
                patientDto.IsDischarged = true;
                if (result)
                    dbPatient.EditPatientAdmissionDetails(patientDto);
                #endregion update


                return Json(new { Message = displayMessage });

            }

            //ViewBag.PatientDetailId = new SelectList(db.PatientDetails, "PatientDetailId", "PatientDetailId", invoice.PatientDetailId);
            return View(invoiceModel);
        }


        // POST: Invoices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int invoiceId)
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(invoiceId);
            if (invoiceDto == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
            }
            if (dbInvoice.DeleteInvoice(invoiceId))
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
            else
                displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
            return Json(new { Message = displayMessage });
            //return RedirectToAction("ViewInvoices");
        }
        //
        public ActionResult ViewInvoices()
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
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
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        public ActionResult Delete(int? id)
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin ||
             (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                if (id == null)
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.BadRequest) });
                }
                InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(id);
                if (invoiceDto == null)
                {
                    return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.NotFound) });
                }
                InvoiceDetailsModel InvoiceModel = new InvoiceDetailsModel
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
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }
        #endregion

        #region CommentEditMethod
        // this isn't access for any users. and no need this.
        // GET: Invoices/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (true)
        //        return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InvoiceDetialsDTO invoiceDto = dbInvoice.ViewtInvoiceById(id);
        //    if (invoiceDto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    InvoiceDetailsModel InvoiceModel = new InvoiceDetailsModel { InvoiceId = invoiceDto.Id, InvoiceDate = invoiceDto.InvoiceDate, PatientDetailId = invoiceDto.PatientDetailId };
        //    // ViewBag.PatientDetailId = new SelectList(db.PatientDetails, "PatientDetailId", "PatientDetailId", invoice.PatientDetailId);
        //    return View(InvoiceModel);
        //}

        // POST: Invoices/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,PatientDetailId,InvoiceDate")] InvoiceDetailsModel invoiceModel)
        //{
        //    InvoiceDetialsDTO invoiceDto = new InvoiceDetialsDTO { Id = invoiceModel.InvoiceId, InvoiceDate = invoiceModel.InvoiceDate, PatientDetailId = invoiceModel.PatientDetailId };
        //    if (ModelState.IsValid)
        //    {
        //        dbInvoice.EditInvoice(invoiceDto);
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.PatientDetailId = new SelectList(db.PatientDetails, "PatientDetailId", "PatientDetailId", invoice.PatientDetailId);
        //    return View(invoiceModel);
        //}

        // GET: Invoices/Delete/5
        #endregion



    }
}
