﻿#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class HMSInvoice : IHMSInvoice
    {

        #region Public Methods

        /// <summary>
        /// each Patient admission details has one invoice. so that details are saved in this mehtod
        /// </summary>
        /// <param name="InvoiceDTO">Get object of InvoiceDetialsDTO as parameter</param>
        public bool InsertInvoice(InvoiceDetialsDTO InvoiceDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Invoice Invoice = new Invoice 
                    {
                        Id = InvoiceDTO.Id, 
                        InvoiceDate = InvoiceDTO.InvoiceDate, 
                        PatientDetailId = InvoiceDTO.PatientDetailId 
                    };
                    db.Invoices.Add(Invoice);
                    if (db.SaveChanges() == 1)  // if saved success, return 1 from db.
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// modified invoice details are saved in this mehtod
        /// </summary>
        /// <param name="InvoiceDTO">Get object of InvoiceDetialsDTO as parameter</param>
        public bool EditInvoice(InvoiceDetialsDTO InvoiceDTO)
        {
            try
            {                
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Invoice Invoice = new Invoice 
                    { 
                        Id = InvoiceDTO.Id, 
                        InvoiceDate = InvoiceDTO.InvoiceDate, 
                        PatientDetailId = InvoiceDTO.PatientDetailId 
                    };

                    db.Entry(Invoice).State = EntityState.Modified;
                    if (db.SaveChanges() == 1) // if saved success, return 1 from db.
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// invoice cancelling in this method... this must check permission
        /// </summary>
        /// <param name="InvoiceId">invoice id for deleting</param>
        public bool DeleteInvoice(int InvoiceId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get Invoice by Invoice Id
                    Invoice Invoice = db.Invoices.Find(InvoiceId);
                    db.Invoices.Remove(Invoice);
                    if (db.SaveChanges() == 1) // if saved success, return 1 from db.
                        return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// get all invoice related details from ward, doctor
        /// </summary>
        /// <returns>InvoiceDetialsDTO</returns>
        public IEnumerable<InvoiceDetialsDTO> ViewtInvoice()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get Invoice List with PatientDetails,Wards, Doctors entities
                    IEnumerable<InvoiceDetialsDTO> Invoices = db.Invoices.
                        Join(db.PatientDetails, x => x.PatientDetailId, c => c.PatientDetailId, ((x, c) => new { x, c })).
                        Where(j => j.c.IsDischarged == true).
                        Join(db.Patients, cp => cp.c.PatientId, v => v.Id, ((cp, v) => new { cp, v })).
                        Join(db.Wards, sh => sh.cp.c.WardId, w => w.Id, ((sh, w) => new { sh, w })).
                        Join(db.Doctors, shd => shd.sh.cp.c.DoctorId, dd => dd.Id, ((shd, dd) => new { shd, dd })).
                        Select(m => new InvoiceDetialsDTO
                        {
                            Id = m.shd.sh.cp.x.Id,
                            InvoiceDate = m.shd.sh.cp.x.InvoiceDate,
                            PatientName = m.shd.sh.v.Name,
                            DoctorName = m.dd.Name,
                            DoctorCharges = m.dd.Charges,
                            WardNo = m.shd.w.WardNo,
                            WardFee = m.shd.w.WardFee,
                            AdmitDate = m.shd.sh.cp.c.AdmitDate

                        }).
                        ToList();


                    return Invoices;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Calling required entities and get deta to make invoice .Totol charges are calculated by this method. 
        /// all getting data bind to the object of AllDetails class.
        /// </summary>
        /// <param name="patientId">All patient activities relate with this para.</param>
        /// <returns>Object if the allDetailsDTO. There included invoice related all details</returns>
        public InvoiceDetialsDTO ViewInitialInoice(int? patientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    // Get Patient details which have admitted for making invoice.
                    IEnumerable<InvoiceDetialsDTO> allDetails = db.Patients.Where(c => c.Id == patientId)
                          .Join(db.PatientDetails.Where(f => f.IsDischarged == false), x => x.Id, s => s.PatientId,
                          ((x, s) => new { x, s }))
                          .Join(db.Wards, sh => sh.s.WardId, w => w.Id,
                          ((sh, w) => new { sh, w }))
                          .Join(db.Doctors, shd => shd.sh.s.DoctorId, dd => dd.Id,
                          ((shd, dd) => new { shd, dd }))
                          .Select(m => new InvoiceDetialsDTO
                          {
                              DoctorCharges = m.dd.Charges,
                              DoctorName = m.dd.Name,
                              InvoiceDate = DateTime.Now,
                              PatientName = m.shd.sh.x.Name,
                              PatientDetailId = m.shd.sh.s.PatientDetailId,
                              WardFee = m.shd.w.WardFee,
                              WardNo = m.shd.w.WardNo,
                              AdmitDate = m.shd.sh.s.AdmitDate,
                          }).ToList();


                    var invoiceDetail = allDetails.Last();

                    return invoiceDetail;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Calculate Total charges for Patient
        /// </summary>
        /// <param name="allDetails">InvoiceDetialsDTO</param>
        /// <returns>decimal</returns>
        public decimal CalculateInvoice(InvoiceDetialsDTO allDetails)
        {
            try
            {
                // calcaulate invoices charges. 
                return Convert.ToDecimal((Convert.ToDateTime(allDetails.InvoiceDate).Day - Convert.ToDateTime(allDetails.AdmitDate).Day) + 1)
                     * (Convert.ToDecimal(allDetails.WardFee) + Convert.ToDecimal(allDetails.DoctorCharges));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Get Invoice detaisl by id
        /// </summary>
        /// <param name="InvoiceId">int? type</param>
        /// <returns>InvoiceDetialsDTO</returns>
        public InvoiceDetialsDTO ViewtInvoiceById(int? InvoiceId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                   
                    ///get invoice details with doctor, ward details.
                    IEnumerable<InvoiceDetialsDTO> allDetails = db.Patients
                          .Join(db.PatientDetails, x => x.Id, s => s.PatientId,
                          ((x, s) => new { x, s }))
                          .Join(db.Invoices,p=>p.s.PatientDetailId,i=>i.PatientDetailId,
                          ((p,i)=>new {p,i}))
                          .Join(db.Wards, sh => sh.p.s.WardId, w => w.Id,
                          ((sh, w) => new { sh, w }))
                          .Join(db.Doctors, shd => shd.sh.p.s.DoctorId, dd => dd.Id,
                          ((shd, dd) => new { shd, dd }))
                          .Select(m => new InvoiceDetialsDTO
                          {
                              Id=m.shd.sh.i.Id,
                              DoctorCharges = m.dd.Charges,
                              DoctorName = m.dd.Name,
                              InvoiceDate = DateTime.Now,
                              PatientName = m.shd.sh.p.x.Name,
                              PatientDetailId = m.shd.sh.p.s.PatientDetailId,
                              WardFee = m.shd.w.WardFee,
                              WardNo = m.shd.w.WardNo,
                              AdmitDate = m.shd.sh.p.s.AdmitDate
                          }).ToList();

                    var invoiceDetail = allDetails.Last();
                    return invoiceDetail;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Patient might be already discharged, then no way to make invoice and patient admission details might be not in table,
        /// this method are using for checking availability
        /// </summary>
        /// <param name="patientId">Patient Master Table Id (FK)</param>
        /// <returns>bool value</returns>
        public bool CheckPatientAvalabilityForMakeInvoice(int? patientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    // Check Admitted patients available. If not return false. it check by getting count.
                    if (db.PatientDetails.Where(x => x.PatientId == patientId && x.IsDischarged == false).
                        Select(x => x.IsDischarged).ToList().Count() < 1)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
      
        /// <summary>
        /// check dates in patient admitted date and invoice dates, and return result
        /// </summary>
        /// <param name="admitDate">DateTime</param>
        /// <param name="invoiceDate">DateTime</param>
        /// <returns></returns>       
        public bool validateDatesForInvoices(DateTime admitDate, DateTime invoiceDate)
        {
            try
            {
                //check admit date larger than invoice date. if  admit date is high, return false.
                if (admitDate > invoiceDate)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
        }
       
        #endregion
    }
}