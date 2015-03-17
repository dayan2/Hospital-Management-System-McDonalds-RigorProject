using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business.DTO
{
    public class AllDetialsDTO
    {

        public PatientAdmissionDTO patientDetailsDto {get;set;}
        private PatientDTO patientDto{get;set;}
        private InvoiceDTO invoiceDto{get;set;}
        private DoctorRoleDTO doctorDto{get;set;}
        private WardDTO wordDto{get;set;}
       
        
        
        
        
        
        
        
        //public AllDetialsDTO(PatientDTO patient, PatientAdmissionDTO patientD)
        //{
        //    this.patientDetailsDto = patientD;
        //    this.patientDto = patient;
        //}

        //public AllDetialsDTO(PatientDTO patient, PatientAdmissionDTO patientD, InvoiceDTO invoice, DoctorRoleDTO doctor, WardDTO ward)
        //{
        //    this.patientDetailsDto = patientD;
        //    this.patientDto = patient;
        //    this.invoiceDto = invoice;
        //    this.doctorDto = doctor;
        //    this.wordDto = ward;
        //}
    }


}
