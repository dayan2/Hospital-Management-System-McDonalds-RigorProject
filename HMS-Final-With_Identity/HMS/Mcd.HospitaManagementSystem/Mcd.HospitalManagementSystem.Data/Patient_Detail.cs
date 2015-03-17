//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mcd.HospitalManagementSystem.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient_Detail
    {
        public Patient_Detail()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Patient_Medical_Test = new HashSet<Patient_Medical_Test>();
            this.PatientFeedbacks = new HashSet<PatientFeedback>();
        }
    
        public int Patient_Detail_Id { get; set; }
        public Nullable<System.DateTime> AdmitDate { get; set; }
        public Nullable<int> BedId { get; set; }
        public Nullable<int> WardId { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<bool> IsDischarged { get; set; }
    
        public virtual Bed Bed { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Ward Ward { get; set; }
        public virtual ICollection<Patient_Medical_Test> Patient_Medical_Test { get; set; }
        public virtual ICollection<PatientFeedback> PatientFeedbacks { get; set; }
    }
}
