using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mcd.HospitalManagement.Web.Models
{
    public class PatientMedicalTestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient Detail is required")]
        public int PatientDetailId { get; set; }

        [Required(ErrorMessage = "Medical Test is required")]
        public Nullable<int> MedicalTestId { get; set; }

        [Required(ErrorMessage = "Nurse is required")]
        public int NurseId { get; set; }

        public PatientAdmissionDetailsModel PatientAdmissionDetails { get; set; }

        public List<SelectListItem> Nurse { get; set; }

        public List<SelectListItem> MedicalTestType { get; set; }

        [Display(Name = "Nurse Name")]
        public string NurseName { get; set; }

        [Display(Name = "Medical Test Description")]
        public string MedicalTestDescription { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

    }
}