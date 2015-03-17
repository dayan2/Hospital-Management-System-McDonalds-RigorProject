#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class PatientFeedbackDTO
    {
        public int Id { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public int PatientDetailId { get; set; }
        public Nullable<System.DateTime> FeedbackDate { get; set; }
        public string FeedbackDescription { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DoctorRoleDTO Doctor { get; set; }
     
    }
}
