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
    public class HMSNurse : INurse
    {
        #region Public Methods

        /// <summary>
        /// Save nurese details. It needs for Medical Test arrangement to patients
        /// </summary>
        /// <param name="nurseDTO">NurseDTO</param>
        public bool InsertNurse(NurseDTO nurseDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Nurse Nurse = new Nurse
                    {
                        Id = nurseDTO.Id,
                        Name = nurseDTO.Name,
                        WardId = nurseDTO.WardId
                    };
                    db.Nurses.Add(Nurse);
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
        /// Modifiying Nurse details using nurse details
        /// </summary>
        /// <param name="nurseDTO">NurseDTO</param>
        /// <returns>bool value</returns>
        public bool EditNurse(NurseDTO nurseDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Nurse nurse = new Nurse
                    {
                        Id = nurseDTO.Id,
                        Name = nurseDTO.Name,
                        WardId = nurseDTO.WardId
                    };
                    db.Entry(nurse).State = EntityState.Modified;

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
        /// delete Nurse using id
        /// </summary>
        /// <param name="nurseId">int type</param>
        /// <returns>bool value</returns>
        public bool DeleteNurse(int nurseId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Nurse nurse = db.Nurses.Find(nurseId);
                    db.Nurses.Remove(nurse);
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
        /// get all nurses details 
        /// </summary>
        /// <returns> IEnumerable NurseDTO list</returns> 
        public IEnumerable<NurseDTO> ViewtNurse()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get Nurse List with Ward Number.               
                    IEnumerable<NurseDTO> NurseList = db.Nurses.
                        Join(db.Wards, n => n.WardId, w => w.Id,
                        ((n, w) => new { n, w })).
                        Select(c => new NurseDTO
                        {
                            Id = c.n.Id,
                            Name = c.n.Name,
                            WardId = c.w.Id,
                            WardNo = c.w.WardNo
                        }).ToList();

                    return NurseList;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// get nurse details by id
        /// </summary>
        /// <param name="nurseId">int? type</param>
        /// <returns>NurseDTO</returns> 
        public NurseDTO ViewtNurseById(int? nurseId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //check have nurse by id. if null, return exception.
                    if (db.Nurses.Find(nurseId) == null)
                    {
                        throw new ArgumentNullException("Null object");
                    }
                    //Get Nurse by id with Ward no.
                    IEnumerable<NurseDTO> NurseList = db.Nurses.Where(b => b.Id == nurseId).
                        Join(db.Wards, n => n.WardId, w => w.Id,
                        ((n, w) => new { n, w })).
                        Select(c => new NurseDTO
                        {
                            Id = c.n.Id,
                            Name = c.n.Name,
                            WardId = c.w.Id,
                            WardNo = c.w.WardNo
                        }).ToList();

                    return NurseList.First(); ;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// varify nurse is exists or not
        /// </summary>
        /// <param name="nurseId">int? type</param>
        /// <returns>bool value</returns>
        public bool CheckExistsNurse(int? nurseId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    // find nurse by id. If not, return false.
                    if (db.Nurses.Find(nurseId) == null)
                    {
                        return false;
                    }
                    return true;
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

