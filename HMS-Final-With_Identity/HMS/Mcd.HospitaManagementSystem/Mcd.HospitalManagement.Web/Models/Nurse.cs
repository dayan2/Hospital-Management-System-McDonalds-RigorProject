using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class Nurse
    {        
        [Required(ErrorMessage="Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public Nullable<int> WardId { get; set; }

        public Ward Ward { get; set; }
    }
}