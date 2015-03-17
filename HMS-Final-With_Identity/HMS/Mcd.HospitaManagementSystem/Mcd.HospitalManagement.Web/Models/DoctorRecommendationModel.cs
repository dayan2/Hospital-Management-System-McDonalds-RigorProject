using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class DoctorRecommendationModel
    {
        //[Display(Name = "Recomended Doctor ID")]
        //public int Id { get; set; }

        //public int? PatientId { get; set; }

        //public int? RecomendedDoctorId { get; set; }

        //[Display(Name = "Current Doctor ID")]
        //public int? CurrentDoctorId { get; set; }

        //public string Reason { get; set; }


        //public string RecomendedDoctorName { get; set; }
        //public string CurrentDoctorName { get; set; }
        //public string PatientName { get; set; }

        public int Id { get; set; }
        public int? RecomendedDoctorId { get; set; }
        public string RecomendedDoctorName { get; set; }
        public int? CurrentDoctorId { get; set; }
        public string CurrentDoctorName { get; set; }
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public string Reason { get; set; }
    }
}