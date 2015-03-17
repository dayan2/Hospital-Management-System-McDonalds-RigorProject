﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Models
{
    public class PatientModel
    {
        [Required(ErrorMessage = "enter id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nic is Required")]
        //[RegularExpression(@"^\d{9}(X|V)$", ErrorMessage = "Not a valid NIC Number")]
        public string NIC { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Moblie Number is Required")]
       // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNo { get; set; }

        public virtual IEnumerable<PatientModel> Patients { get; set; }
    }

}