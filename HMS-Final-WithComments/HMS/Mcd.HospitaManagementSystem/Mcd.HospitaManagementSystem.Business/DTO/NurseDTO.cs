using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
  public  class NurseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> WardId { get; set; }
        public string  WardNo { get; set; }
    }
}
