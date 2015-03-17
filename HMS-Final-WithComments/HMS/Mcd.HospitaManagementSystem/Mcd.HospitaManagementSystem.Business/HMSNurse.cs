﻿#region Using Directives
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
        /// <param name="NurseDTO"></param>
        public bool InsertNurse(NurseDTO NurseDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Nurse Nurse = new Nurse { Id = NurseDTO.Id, Name = NurseDTO.Name, WardId = NurseDTO.WardId };
                    db.Nurses.Add(Nurse);
                    if (db.SaveChanges() == 1)
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
        /// <param name="NurseDTO"></param>
        /// <returns></returns>
        public bool EditNurse(NurseDTO NurseDTO)
        {
            try
            {


                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Nurse nurse = new Nurse { Id = NurseDTO.Id, Name = NurseDTO.Name, WardId = NurseDTO.WardId };
                    db.Entry(nurse).State = EntityState.Modified;
                    if (db.SaveChanges() == 1)
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
        /// <param name="NurseId"></param>
        /// <returns>bool value</returns>
        public bool DeleteNurse(int NurseId)
        {
            try
            {


                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Nurse nurse = db.Nurses.Find(NurseId);
                    db.Nurses.Remove(nurse);
                    if (db.SaveChanges() == 1)
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
        /// <returns></returns> 
        public IEnumerable<NurseDTO> ViewtNurse()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {                    
                    IEnumerable<NurseDTO> NurseList = db.Nurses.Join(db.Wards, n => n.WardId, w => w.Id,
                        ((n, w) => new { n, w })).Select(c => new NurseDTO
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
        /// <param name="NurseId"></param>
        /// <returns></returns> 
        public NurseDTO ViewtNurseById(int? NurseId)
        {
            try
            {


                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Nurse Nurse = db.Nurses.Find(NurseId);

                    //NurseDTO NurseDTO = new NurseDTO
                    //{
                    //    Id = Nurse.Id,
                    //    Name = Nurse.Name,
                    //    WardId = Nurse.WardId
                    //};

                    if (db.Nurses.Find(NurseId) == null)
                    {
                        throw new ArgumentNullException("Null object");
                    }
                    IEnumerable<NurseDTO> NurseList = db.Nurses.Where(b => b.Id == NurseId).Join(db.Wards, n => n.WardId, w => w.Id,
                        ((n, w) => new { n, w })).Select(c => new NurseDTO
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
        /// <param name="NurseId"></param>
        /// <returns>bool value</returns>
        public bool CheckExistsNurse(int? NurseId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    if (db.Nurses.Find(NurseId) == null)
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
