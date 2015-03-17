﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Models
{
    public class PatientAdmissionDetailsModel
    {
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> AdmitDate { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int PatientDetailId { get; set; }
        public string WardNo { get; set; }
        public string DoctorName { get; set; }
        public string BedTicketNo { get; set; }
        public Nullable<int> BedId { get; set; }
        public Nullable<int> WardId { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public Nullable<bool> IsDischarged { get; set; }

        public string Address { get; set; }


        public string MobileNo { get; set; }





        public IEnumerable<PatientModel> patients { get; set; }
        public virtual IEnumerable<PatientAdmissionDetailsModel> patientAdmissonDetails { get; set; }
        public virtual IEnumerable<BedModel> bedDetails { get; set; }
        public virtual IEnumerable<DoctorModel> doctorDetails { get; set; }
        public virtual IEnumerable<WardModel> wardDetails { get; set; }
        public virtual IEnumerable<AllPatientDetailsModel> allPatientDetailsModel { get; set; }
        public virtual BedModel bedModel { get; set; }
        public virtual DoctorModel doctorModel { get; set; }
        public virtual PatientModel patientModel { get; set; }
        public virtual WardDetailsModel wardModel { get; set; }
        public virtual AllPatientDetailsModel allPatientModel { get; set; }
    }
}