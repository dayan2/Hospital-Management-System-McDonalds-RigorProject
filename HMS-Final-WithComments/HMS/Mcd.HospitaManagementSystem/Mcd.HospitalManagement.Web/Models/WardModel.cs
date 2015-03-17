using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class WardModel
    {
        [Required(ErrorMessage = "Id Required")]
        public int Id { get; set; }

        [Required(ErrorMessage="Ward No Required")]
        [Display(Name = "Ward No")]
        public string WardNo { get; set; }

        [Required(ErrorMessage = "Ward Fee Required")]
        [Display(Name = "Ward Fee")]
        public Nullable<decimal> WardFee { get; set; }
    }
}