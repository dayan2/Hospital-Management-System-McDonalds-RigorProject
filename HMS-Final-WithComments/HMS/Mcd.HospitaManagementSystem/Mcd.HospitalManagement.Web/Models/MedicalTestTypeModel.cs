using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class MedicalTestTypeModel
    {
        public int Id { get; set;}

        [Required(ErrorMessage="Description is required")]
        public string Description { get; set; }
    }
}