using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class WardDetails
    {
        public int Id { get; set; }
        public string WardNo { get; set; }
        public Nullable<decimal> WardFee { get; set; }
    }
}