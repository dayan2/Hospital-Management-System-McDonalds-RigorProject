using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business.DTO
{
  public  class InvoiceDTO
    {
        public int id { get; set; }
        public Nullable<int> patientDetailId { get; set; }
        public Nullable<System.DateTime> invoiceDate { get; set; }
    }
}
