using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class DoctorDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DoctorSpecialityField { get; set; }
        public string PhoneNo { get; set; }
        public int WardId { get; set; }

        
    }
}
