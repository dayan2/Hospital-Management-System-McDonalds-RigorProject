using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class DoctorAddModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        public int DoctorSpecialityId { get; set; }
        [Required]
        public Nullable<decimal> Charges { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        [Display(Name = "Ward")]
        public int WardId { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
    }
}