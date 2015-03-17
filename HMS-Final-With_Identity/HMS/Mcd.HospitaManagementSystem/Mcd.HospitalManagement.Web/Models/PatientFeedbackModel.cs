#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Models
{
    public class PatientFeedbackModel
    {
        public int Id { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public int PatientDetailId { get; set; }

        [Required]
        public Nullable<System.DateTime> FeedbackDate { get; set; }

         [Required]
        public string FeedbackDescription { get; set; }

         [Required]
        public string DoctorName { get; set; }
        public int PatientId { get; set; }

         [Required]
        public string PatientName { get; set; }

        public virtual IEnumerable<PatientFeedbackModel> doctors { get; set; }
        public virtual PatientFeedbackModel doctorss { get; set; }

    }
}