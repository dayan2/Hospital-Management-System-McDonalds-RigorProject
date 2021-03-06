﻿using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class BedDetailsModel
    {
        [Required(ErrorMessage = "Id Reqiured")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Ward Required")]
        public Nullable<int> WardId { get; set; }

        [Required(ErrorMessage="Bed Ticket No Reqiured")]
        [Display(Name = "Bed Ticket No")]
        public string BedTicketNo { get; set; }

        [Display(Name = "Ward No")]
        public  string WardNo { get; set; }

        public string JsonData { get; set; }
       

    }
}