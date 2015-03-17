﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public interface IBedManager
    {
        #region UserRole Function

        bool InsertBed(BedDTO bedDto);

        bool DeleteBed(int bedId);

        bool EditBed(BedDTO bedDto);

        IEnumerable<BedDTO> ViewAllBed();

        BedDTO ViewtBedById(int BedId);

        #endregion 
    }
}