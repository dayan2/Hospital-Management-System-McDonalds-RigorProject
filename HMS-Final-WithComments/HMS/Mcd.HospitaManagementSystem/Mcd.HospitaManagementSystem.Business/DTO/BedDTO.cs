using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
public class BedDTO
    {
        public int Id { get; set; }
        public Nullable<int> WardId { get; set; }
        public string BedTicketNo { get; set; }

        public string WardNo { get; set; }
        
    }
}
