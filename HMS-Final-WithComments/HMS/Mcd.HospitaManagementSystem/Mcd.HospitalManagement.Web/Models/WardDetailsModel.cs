using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class WardDetailsModel
    {
        public int id { get; set; }
        public string wardNo { get; set; }
        public Nullable<decimal> wardFee { get; set; }
    }
}