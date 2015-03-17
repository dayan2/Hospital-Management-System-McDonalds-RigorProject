using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserLogin
    {
        #region Private Fields
        private LP_HMSDbEntities db = new LP_HMSDbEntities();
        #endregion

        #region Public Methods
        public int CheckAuthentication(UserDTO userdto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    User user = new User()
                    {
                        UserName = userdto.UserName,
                        Password = userdto.Password
                    };

                    var userSelected = from usr in db.Users
                                       where usr.UserName == user.UserName && user.Password == user.Password
                                       select usr.Id;

                    List<int> selectedCollection = userSelected.ToList();

                    if (selectedCollection.Count == 0)
                    {
                        return 0;
                    }
                    return selectedCollection.FirstOrDefault();
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
