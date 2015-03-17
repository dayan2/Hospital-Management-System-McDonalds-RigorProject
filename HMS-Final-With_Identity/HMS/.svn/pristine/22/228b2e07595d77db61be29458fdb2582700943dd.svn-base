using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
  public  interface IPatientMedicalTestAllocation
    {
      bool InsertPatientMedicalTest(PatientMedicalTestDTO patientMedicalTestDTO);

      bool EditPatientMedicalTest(PatientMedicalTestDTO patientMedicalTestDTO);

      bool DeletePatientMedicalTest(int patientMedicalTestId);

      IEnumerable<PatientMedicalTestDTO> ViewtPatientMedicalTest();

      PatientMedicalTestDTO ViewtPatientMedicalTestById(int? patientMedicalTestId);

      IEnumerable<PatientDTO> FindPatientDetails(string name);

      bool CheckExistsMedicalTestForPatientAdmission(PatientMedicalTestDTO patientMedicalTestDTO);

      bool ExistsPatientMedicalTest(int id);

      bool ExitsNurseInMedicalAllocation(int NurseId);

      bool ExitsMedicalInMedicalAllocation(int MedicalTestId);
    }
}
