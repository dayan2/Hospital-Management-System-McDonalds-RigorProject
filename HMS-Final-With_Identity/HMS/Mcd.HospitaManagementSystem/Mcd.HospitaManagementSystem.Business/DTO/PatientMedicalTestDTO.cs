﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
  public  class PatientMedicalTestDTO
    {
        public int Id { get; set; }

        public int PatientDetailId { get; set; }

        public Nullable<int> MedicalTestId { get; set; }

        public int NurseId { get; set; }

        public string NurseName { get; set; }

        public string MedicalTest { get; set; }

        public string  PatientName { get; set; }

    }
}
