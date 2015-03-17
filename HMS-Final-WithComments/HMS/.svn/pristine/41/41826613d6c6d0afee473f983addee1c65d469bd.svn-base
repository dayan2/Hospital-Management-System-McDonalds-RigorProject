#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public interface INurse
    {
        bool InsertNurse(NurseDTO Nurse);

        bool EditNurse(NurseDTO Nurse);

        bool DeleteNurse(int NurseId);

        IEnumerable<NurseDTO> ViewtNurse();

        NurseDTO ViewtNurseById(int? NurseId);

        bool CheckExistsNurse(int? NurseId);
    }
}
