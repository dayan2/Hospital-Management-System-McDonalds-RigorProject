#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserLogin
    {

        #region Public Methods

        /// <summary>
        /// Check Authentication details when users log in to the system
        /// </summary>
        /// <param name="userdto">userDto type object</param>
        /// <returns>Return int type value</returns>
        public int CheckAuthentication(UserDTO userdto)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //Map user Dto type object to user entity type object
                    User user = new User()
                    {
                        UserName = userdto.UserName,
                        Password = userdto.Password
                    };

                    //Get user details that exists in the system with same user type object details
                    var userSelected = from usr in db.Users
                                       where usr.UserName == user.UserName && usr.Password == user.Password
                                       select usr.Id;

                    //Put into List type integer collection
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
