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
        /// <param name="medicalTestTypeDTO">MedicalTestTypeDTO</param>
        /// <returns>True/False value. If saved success, give true as a result</returns>
        public bool InsertMedicalTestType(MedicalTestTypeDTO medicalTestTypeDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //mapping MedicalTestTypeDTO to MedicalTestType
                    MedicalTestType medicalTestType = new MedicalTestType
                    {
                        Id = medicalTestTypeDTO.Id,
                        Description = medicalTestTypeDTO.Description
                    };
                    db.MedicalTestTypes.Add(medicalTestType);
                    if (db.SaveChanges() == 1) // if saved success, return 1 from db.
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
        /// <param name="medicalTestTypeDTO">MedicalTestTypeDTO</param>
        /// <returns>True/False value,If saved success, give true as a result</returns>
        public bool EditMedicalTestType(MedicalTestTypeDTO medicalTestTypeDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    MedicalTestType medicalTestType = new MedicalTestType 
                    { 
                        Id = medicalTestTypeDTO.Id, 
                        Description = medicalTestTypeDTO.Description 
                    };
                    db.Entry(medicalTestType).State = EntityState.Modified;
                    if (db.SaveChanges() == 1) // if saved success, return 1 from db.
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
        /// <param name="medicalTestTypeId">int type</param>
        /// <returns>True/False value. If delete and saved success, A result shuold give true</returns>
        public bool DeleteMedicalTestType(int medicalTestTypeId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    MedicalTestType medicalTestType = db.MedicalTestTypes.Find(medicalTestTypeId);
                    db.MedicalTestTypes.Remove(medicalTestType);
                    if (db.SaveChanges() == 1) // if saved success, return 1 from db.
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
                    //Get all Medical Test details
                    var medicalTestTypes = db.MedicalTestTypes.ToList();
                    IEnumerable<MedicalTestTypeDTO> medicalTestTypeList = medicalTestTypes.Select
                    (c => new MedicalTestTypeDTO
                    {
                        Id = c.Id,
                        Description = c.Description
                    }).ToList();

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
        /// <param name="medicalTestTypeId">int? type</param>
        /// <returns>MedicalTestTypeDTO object</returns>
        public MedicalTestTypeDTO ViewtMedicalTestTypeById(int? medicalTestTypeId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //MedicalTestType find by id
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
