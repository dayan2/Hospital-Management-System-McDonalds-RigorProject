#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcd.HospitalManagementSystem.Data;
# endregion
namespace Mcd.HospitaManagementSystem.Business
{
  public  interface IPatientManager
    {
      bool InsertPatient(PatientDTO patientDTO);
      bool EditPatient(PatientDTO patientDTO);
      PatientDTO ViewPatientDetails(int? patientID);
      IEnumerable<PatientDTO> ViewAllPatientDetails();
      bool Deletepatient(int? patientId);
      IEnumerable<PatientDTO> fillPatients();
      AllPatientDTO ViewPatientRelatedData(int? patientId);
      bool InsertPatientAdmissionDetails(PatientAdmissionDTO patientAdmissionDTO);
  
      PatientAdmissionDTO ViewPatientAdmissionDetails(int? patientID);
      IEnumerable<PatientAdmissionDTO> viewAllAdmissionPatientDetails();

      bool DeletePatientAdmissionDetails(int? patientId);


      bool EditPatientAdmissionDetails(AllPatientDTO patientAdmissionDTO);

      IEnumerable<AllPatientDTO >ViewAllPatientAdmissionDetails();

      bool CheckPatientAdmissionAvilabilty(int? patientID);
      bool CheckPatientAvilability(string nic);
      bool InsertPatientFeedBack(PatientFeedbackDTO patientFeedbackDto);
      bool CheckRelationshipBetweenPatientAndPatientDetails(int? patientId);
      IList<DoctorDetailDTO> ViewDoctorFeedback(int?id);
      PatientAdmissionDTO GetPatientDetailId(int? patientId);
      IEnumerable<AllPatientDTO> ViewDoctorAccordingToPatientId(int patientId);
      IEnumerable<PatientFeedbackDTO> ViewAllPatientFeedbackDetails();
      PatientFeedbackDTO ViewAllPatientFeedbackDetailsAccordingToId(int? patientFeedBackId);

      IEnumerable<PatientDoctorDTO> GetAllPatientDetailsForDoctor(int? userId);

      bool EditPatientsFeedbackDetails(PatientFeedbackDTO patientFeedback);

      IEnumerable<PatientDTO> ViewAllPatientDetailsAccordingToAdmission();
    }
}
