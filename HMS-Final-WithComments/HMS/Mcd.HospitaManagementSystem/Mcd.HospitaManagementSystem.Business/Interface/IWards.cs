using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mcd.HospitaManagementSystem.Business
{
   public interface IWards
    {
      
       bool InsertWard(WardDTO ward);

       bool EditWard(WardDTO ward);

       WardDTO SelectWard(int wardId);

       WardDTO SelectToDeleteWard(int wardId);

       bool DeleteWard(int wardId);

       IEnumerable<WardDTO> ViewWardDetails();

       WardDTO ViewWardById(int wardId);
    }
}
