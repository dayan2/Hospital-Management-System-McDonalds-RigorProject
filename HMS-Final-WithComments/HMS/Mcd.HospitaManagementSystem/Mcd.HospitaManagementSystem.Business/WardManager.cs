using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class WardManager : IWards
    {

        #region Private Fields
        private WardRegistration wardRegistration;
        #endregion


        #region Public Methods
        public bool InsertWard(WardDTO ward)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.createWard(ward);
        }

        public bool EditWard(WardDTO ward)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.editWard(ward);
        }

        public WardDTO SelectWard(int wardId)
        {
            throw new NotImplementedException();
        }

        public WardDTO SelectToDeleteWard(int wardId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWard(int wardId)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.deleteWard(wardId);
        }

        public IEnumerable<WardDTO> ViewWardDetails()
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.viewAllWard();
        }

        public WardDTO ViewWardById(int wardId)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.viewWardById(wardId);

        }
        #endregion
 
    }
}
