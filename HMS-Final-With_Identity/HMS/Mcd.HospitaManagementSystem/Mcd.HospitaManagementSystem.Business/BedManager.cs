#region Using Directives
using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class BedManager : IBedManager
    {

        #region Public Fields
        /// <summary>
        /// Insert Bed entity type object to db
        /// </summary>
        /// <param name="bedDto">bed Dto type object</param>
        /// <returns>Return insertion success or failure</returns>
        public bool InsertBed(BedDTO bedDto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Map bed DTO type object to bed entity type object
                    Bed bed = new Bed()
                    {
                        BedTicketNo = bedDto.BedTicketNo,
                        WardId = bedDto.WardId
                    };

                    //Add new bed details to the system
                    db.Beds.Add(bed);
                    //Save changes return if it's success
                    if (db.SaveChanges() == 1)
                        return true;

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        ///  Delete Bed entity type object from db
        /// </summary>
        /// <param name="bedId">bed Id</param>
        /// <returns>Return insertion success or failure</returns>
        public bool DeleteBed(int bedId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Find bed detail by id
                    Bed bed = db.Beds.Find(bedId);

                    //Remove bed detail object
                    db.Beds.Remove(bed);
                    //Save changes return if it's success
                    if (db.SaveChanges() == 1)
                        return true;

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// Modify Bed entity type object in db
        /// </summary>
        /// <param name="bedDto">Bed dto type object</param>
        /// <returns>Modifying is successfully or not</returns>
        public bool EditBed(BedDTO bedDto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Map bed DTO type object to bed entity type object
                    Bed bed = new Bed()
                    {
                        Id = bedDto.Id,
                        BedTicketNo = bedDto.BedTicketNo,
                        WardId = bedDto.WardId
                    };
                    db.Entry(bed).State = EntityState.Modified;

                    //Save changes return if it's success
                    if (db.SaveChanges() == 1)
                        return true;

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }


        

        /// <summary>
        /// Get specific bed detail by bed id  
        /// </summary>
        /// <param name="bedId">Bed Id</param>
        /// <returns>BedDTO type object</returns>
        public BedDTO ViewtBedById(int? bedId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Find bed detail by bed id
                    Bed bed = db.Beds.Find(bedId);

                    //Map bed entity object to bed DTO  type object
                    BedDTO bedDto = new BedDTO()
                    {
                        Id = bed.Id,
                        BedTicketNo = bed.BedTicketNo,
                        WardNo = bed.Ward.WardNo,
                        WardId = bed.WardId

                    };

                    return bedDto;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get all bed deatils in sysytem
        /// </summary>
        /// <returns>IEnumerable bed dto type object</returns>
        public IEnumerable<BedDTO> ViewAllBed()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Get all bed details
                    var beds = db.Beds.ToList();

                    //Get all ward detail object
                    var wards = db.Wards.ToList();


                    //Map bed entity object to bed DTO  type object
                    IEnumerable<BedDTO> lstBeds = from lb in beds
                                                  from wb in wards
                                                  where lb.WardId == wb.Id
                                                  select new BedDTO()
                                                  {
                                                      Id = lb.Id,
                                                      BedTicketNo = lb.BedTicketNo,
                                                      WardId = wb.Id,
                                                      WardNo = wb.WardNo

                                                  };

                    return lstBeds;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

      
    }
}
