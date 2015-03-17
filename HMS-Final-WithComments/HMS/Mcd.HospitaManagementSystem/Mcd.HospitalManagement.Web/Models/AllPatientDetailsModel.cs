using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mcd.HospitalManagement.Web.Models
{
    public class AllPatientDetailsModel
    {
        private PatientModel PatientModel;
        private PatientAdmissionDetailsModel PatientAdmissionModel;
        private WardDetailsModel WardDetails;
        private BedModel BedModel;
        private DoctorModel DoctorModel;

        public AllPatientDetailsModel()
        {

        }

        public AllPatientDetailsModel(PatientModel patients,PatientAdmissionDetailsModel admission,DoctorModel doctors,WardDetailsModel wards,BedModel beds)
        {
            this.PatientModel = patients;
            this.PatientAdmissionModel = admission;
            this.DoctorModel = doctors;
            this.WardDetails = wards;
            this.BedModel = beds;
        }

    }
}