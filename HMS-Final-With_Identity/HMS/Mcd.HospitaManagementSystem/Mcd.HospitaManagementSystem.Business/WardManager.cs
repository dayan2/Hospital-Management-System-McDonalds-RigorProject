﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace Mcd.HospitaManagementSystem.Business
{
    public class WardManager : IWards
    {

        #region Private Fields
        private WardRegistration wardRegistration;
        #endregion

        #region Public Methods

        /// <summary>
        /// Call WardRegistration class for insert ward details
        /// </summary>
        /// <param name="ward">WardDTO type object</param>
        /// <returns>Return insertion success or failure</returns>
        public bool InsertWard(WardDTO ward)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.createWard(ward);
        }

        /// <summary>
        /// Call WardRegistration class for edit ward details
        /// </summary>
        /// <param name="ward">WardDTO type object</param>
        /// <returns>Return modifying success or failure</returns>
        public bool EditWard(WardDTO ward)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.editWard(ward);
        }
      
        /// <summary>
        /// Call WardRegistration class for delete ward details
        /// </summary>
        /// <param name="wardId">ward Id</param>
        /// <returns>Return deletion success or failure</returns>
        public bool DeleteWard(int wardId)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.deleteWard(wardId);
        }
        /// <summary>
        /// Call WardRegistration class for get all ward details
        /// </summary>
        /// <returns>Ward DTO type object</returns>
        public IEnumerable<WardDTO> ViewWardDetails()
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.viewAllWard();
        }
        /// <summary>
        /// Call WardRegistration class for get all ward details
        /// </summary>
        /// <param name="wardId">ward Id</param>
        /// <returns>WardDTO type object</returns>
        public WardDTO ViewWardById(int? wardId)
        {
            wardRegistration = new WardRegistration();
            return wardRegistration.viewWardById(wardId);

        }
        #endregion
 
    }
}