using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    
    public class DoctorModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Id Reqiured")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Specialized Field Reqiured")]
        [Display(Name = "Specialized Field")]
        public int DoctorSpecialityId { get; set; }
        [Required(ErrorMessage = "Charges Reqiured")]
        public Nullable<decimal> Charges { get; set; }
        [Required(ErrorMessage = "Phone Number Reqiured")]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Ward ID Reqiured")]
        [Display(Name = "Ward")]
        public int WardId { get; set; }
        [Required(ErrorMessage = "User ID Reqiured")]
        [Display(Name = "User ID")]
        public int UserId { get; set; }
    }
}