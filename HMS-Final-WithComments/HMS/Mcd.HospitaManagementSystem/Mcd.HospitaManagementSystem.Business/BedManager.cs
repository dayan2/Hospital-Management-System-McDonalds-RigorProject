﻿using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class BedManager : IBedManager
    {

        #region Public Fields
        public bool InsertBed(BedDTO bedDto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Bed bed = new Bed()
                    {
                        BedTicketNo = bedDto.BedTicketNo,
                        WardId = bedDto.WardId
                    };
                    db.Beds.Add(bed);
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

        public bool DeleteBed(int bedId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Bed bed = db.Beds.Find(bedId);
                    db.Beds.Remove(bed);
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

        public bool EditBed(BedDTO bedDto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Bed bed = new Bed()
                    {
                        Id = bedDto.Id,
                        BedTicketNo = bedDto.BedTicketNo,
                        WardId = bedDto.WardId
                    };
                    db.Entry(bed).State = EntityState.Modified;
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



        IEnumerable<BedDTO> IBedManager.ViewAllBed()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    var beds = db.Beds.ToList();
                    var wards = db.Wards.ToList();

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


                    //Mapper.CreateMap<Bed, BedDTO>();
                    //return Mapper.Map<IEnumerable<Bed>, IEnumerable<BedDTO>>(lstBeds);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        BedDTO IBedManager.ViewtBedById(int BedId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Bed bed = db.Beds.Find(BedId);

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
        #endregion

        
    }
}