using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class InvoiceDetailsModel
    {  
        public int InvoiceId { get; set; }
        public int PatientId { get; set; }

       
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        public Nullable<int> PatientDetailId { get; set; }

        [Display(Name = "Admit Date")]
        public Nullable<System.DateTime> AdmitDate { get; set; }


        [Required(ErrorMessage="Enter Date")]
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> InvoiceDate { get; set; }

        [Display(Name = "Total Fee")]
        public decimal TotalFee { get; set; }

        [Display(Name = "Ward No")]
        public string WardNo { get; set; }

        [Display(Name = "Ward Fee")]
        public Nullable<decimal> WardFee { get; set; }

        public int DoctorId { get; set; }

        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Display(Name = "Doctor Charges")]
        public decimal? DoctorCharges { get; set; }

        public IEnumerable<PatientModel> Patients { get; set; }

}
}