#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class HMSMedicalTestType : IHMSMedicalTestType
    {
        #region Public Methods

        /// <summary>
        /// Insert medical type in MedicalTestTypes
        /// </summary>
        /// <param name="medicalTestTypeDTO"></param>
        /// <returns>True/False value. If saved success, give true as a result</returns>
        public bool InsertMedicalTestType(MedicalTestTypeDTO medicalTestTypeDTO)
        {
            try
            {                
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    MedicalTestType medicalTestType = new MedicalTestType { Id = medicalTestTypeDTO.Id, Description = medicalTestTypeDTO.Description };
                    db.MedicalTestTypes.Add(medicalTestType);
                    if(db.SaveChanges()==1)
                        return true;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// modify medical details
        /// </summary>
        /// <param name="medicalTestTypeDTO"></param>
        /// <returns>True/False value,If saved success, give true as a result</returns>
        public bool EditMedicalTestType(MedicalTestTypeDTO medicalTestTypeDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    MedicalTestType medicalTestType = new MedicalTestType { Id = medicalTestTypeDTO.Id, Description = medicalTestTypeDTO.Description };
                    db.Entry(medicalTestType).State = EntityState.Modified;
                    if(db.SaveChanges()==1)
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// Delete medical Test Type
        /// </summary>
        /// <param name="medicalTestTypeId"></param>
        /// <returns>True/False value. If delete and saved success, A result shuold give true</returns>
        public bool DeleteMedicalTestType(int medicalTestTypeId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    MedicalTestType medicalTestType = db.MedicalTestTypes.Find(medicalTestTypeId);
                    db.MedicalTestTypes.Remove(medicalTestType);
                    if(db.SaveChanges()==1)
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// filtering all medical test data 
        /// </summary>
        /// <returns>bool</returns>
        public IEnumerable<MedicalTestTypeDTO> ViewtMedicalTestType()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    var medicalTestTypes = db.MedicalTestTypes.ToList();
                    IEnumerable<MedicalTestTypeDTO> medicalTestTypeList = from c in medicalTestTypes
                                                                          select new MedicalTestTypeDTO
                                                                          {
                                                                              Id = c.Id,
                                                                              Description = c.Description
                                                                          };

                    return medicalTestTypeList;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// filtering medical test data by medicalTestTypeId 
        /// </summary>
        /// <param name="medicalTestTypeId"></param>
        /// <returns></returns>
        public MedicalTestTypeDTO ViewtMedicalTestTypeById(int? medicalTestTypeId)
        {
            try
            {                
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    MedicalTestType medicalTestType = db.MedicalTestTypes.Find(medicalTestTypeId);

                    MedicalTestTypeDTO MedicalTestTypeDTO = new MedicalTestTypeDTO
                    {
                        Id = medicalTestType.Id,
                        Description = medicalTestType.Description
                    };
                    return MedicalTestTypeDTO;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
