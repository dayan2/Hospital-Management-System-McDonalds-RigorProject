﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcd.HospitalManagementSystem.Data;
using AutoMapper;
namespace Mcd.HospitaManagementSystem.Business
{
 public class WardDetails:IWards
    {
     private LP_HMSDbEntities db = new LP_HMSDbEntities();
     Ward wards;
     WardDTO wardDto;
     public IEnumerable<WardDTO> ViewWardDetails()
     {
         IList<Ward> ward = db.Wards.ToList();
         Mapper.CreateMap<Ward, WardDTO>();
         var wardList = Mapper.Map<IEnumerable<Ward>, IEnumerable<WardDTO>>(ward);
         return wardList;
     }

     public void InsertWard(WardDTO ward)
     {
         throw new NotImplementedException();
     }

     public void EditWard(WardDTO ward)
     {
         throw new NotImplementedException();
     }

     public WardDTO SelectWard(int wardId)
     {
         throw new NotImplementedException();
     }

     public WardDTO SelectToDeleteWard(int wardId)
     {
         throw new NotImplementedException();
     }

     public void DeleteWard(int userRoleId)
     {
         throw new NotImplementedException();
     }

     public WardDTO ViewWardById(int? wardId)
     {
         throw new NotImplementedException();
     }

     bool IWards.InsertWard(WardDTO ward)
     {
         throw new NotImplementedException();
     }

     bool IWards.EditWard(WardDTO ward)
     {
         throw new NotImplementedException();
     }

     bool IWards.DeleteWard(int wardId)
     {
         throw new NotImplementedException();
     }
    }
}
