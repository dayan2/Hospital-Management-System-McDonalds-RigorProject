using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
   public class HMSMedicalTestType : IHMSMedicalTestType
    {
        private LP_HMSDbEntities db = new LP_HMSDbEntities();

        public void InsertMedicalTestType(MedicalTestTypeDTO medicalTestTypeDTO)
        {
            MedicalTestType medicalTestType = new MedicalTestType { Id = medicalTestTypeDTO.id, Description = medicalTestTypeDTO.description };
            db.MedicalTestTypes.Add(medicalTestType);
            db.SaveChanges();

        }

        public void EditMedicalTestType(MedicalTestTypeDTO medicalTestTypeDTO)
        {
            MedicalTestType medicalTestType = new MedicalTestType { Id = medicalTestTypeDTO.id, Description = medicalTestTypeDTO.description };
            db.Entry(medicalTestType).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteMedicalTestType(int medicalTestTypeId)
        {
            MedicalTestType medicalTestType = db.MedicalTestTypes.Find(medicalTestTypeId);
            db.MedicalTestTypes.Remove(medicalTestType);
            db.SaveChanges();
        }

        public IEnumerable<MedicalTestTypeDTO> ViewtMedicalTestType()
        {
            var medicalTestTypes = db.MedicalTestTypes.ToList();
            IEnumerable<MedicalTestTypeDTO> medicalTestTypeList = from c in medicalTestTypes
                                              select new MedicalTestTypeDTO
                                              {
                                                  id = c.Id,
                                                   description=c.Description
                                              };

            return medicalTestTypeList;
        }

        public MedicalTestTypeDTO ViewtMedicalTestTypeById(int? medicalTestTypeId)
        {
            MedicalTestType medicalTestType = db.MedicalTestTypes.Find(medicalTestTypeId);

            MedicalTestTypeDTO MedicalTestTypeDTO = new MedicalTestTypeDTO
            {
                id = medicalTestType.Id,
                description= medicalTestType.Description
            };
            return MedicalTestTypeDTO;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
