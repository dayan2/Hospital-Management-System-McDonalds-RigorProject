using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class InvoiceModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Select Patient")]
        public Nullable<int> patientDetailId { get; set; }
        
       // [Required(ErrorMessage="Enter Date")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> invoiceDate { get; set; }

        public PatientAdmissionDetailsModel patientDetailsModel { get; set; }
        //private DateTime _date = DateTime.Now;
        //public DateTime InvoiceDate
        //{
        //    get { return _date; }
        //    set { _date = value; }
        //}
    }
}