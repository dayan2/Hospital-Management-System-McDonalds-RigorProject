using AutoMapper;
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class WardRegistration
    {
        #region Private Fields
        private LP_HMSDbEntities db = new LP_HMSDbEntities();
        #endregion

        #region Public Methods
        public IEnumerable<WardDTO> viewAllWard()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    IEnumerable<Ward> ward = db.Wards.ToList();

                    Mapper.CreateMap<Ward, WardDTO>();
                    var wardList = Mapper.Map<IEnumerable<Ward>, IEnumerable<WardDTO>>(ward);
                    return wardList;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public WardDTO viewWardById(int wardId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Ward ward = db.Wards.Find(wardId);

                    WardDTO wardDto = new WardDTO()
                    {
                        Id = ward.Id,
                        WardFee = ward.WardFee,
                        WardNo = ward.WardNo
                    };

                    return wardDto;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public bool createWard(WardDTO wardDto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Ward ward = new Ward()
                    {
                        WardFee = wardDto.WardFee,
                        WardNo = wardDto.WardNo
                    };


                    db.Wards.Add(ward);
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

        public bool editWard(WardDTO wardDto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Ward ward = new Ward()
                    {
                        Id = wardDto.Id,
                        WardFee = wardDto.WardFee,
                        WardNo = wardDto.WardNo
                    };

                    db.Entry(ward).State = EntityState.Modified;
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

        public bool deleteWard(int id)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    Ward ward = db.Wards.Find(id);
                    db.Wards.Remove(ward);
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
        #endregion

    }
}
