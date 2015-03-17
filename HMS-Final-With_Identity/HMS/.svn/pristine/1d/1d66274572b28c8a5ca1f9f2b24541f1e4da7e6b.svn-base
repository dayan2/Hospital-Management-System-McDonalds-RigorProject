using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class DoctorManager : IDoctorManager
    {
        #region Private Fields
        private DoctorDetails doctorDetails;
        #endregion
        #region Constructor
        public DoctorManager()
        {
            this.doctorDetails = new DoctorDetails();
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Retrieving Doctor Details From the DoctorDetails class
        /// </summary>
        /// <returns name="doctorList">
        /// DoctorRoleDTO type of list is returning from the method containing all doctor details
        /// </returns>
        IEnumerable<DoctorRoleDTO> IDoctorManager.GetDoctors()
        {
            IEnumerable<DoctorRoleDTO> doctorList = doctorDetails.GetDoctors();
            return doctorList;
        }

        /// <summary>
        /// Add new Doctor Details
        /// </summary>
        /// <param name="doctor">
        /// DoctorRoleDTO object is passed here which will then redirect to the DoctorDetails class 
        /// </param>
        bool IDoctorManager.AddDoctor(DoctorRoleDTO doctor)
        {
            return doctorDetails.AddDoctor(doctor);
        }

        /// <summary>
        /// Remove an existing Doctor 
        /// </summary>
        /// <param name="id">
        /// Doctor object is passed here which will then redirect to the DoctorDetails class 
        /// </param>
        bool IDoctorManager.RemoveDoctor(int id)
        {
            return doctorDetails.RemoveDoctor(id);
        }

        /// <summary>
        /// Updating an existing Doctor 
        /// </summary>
        /// <param name="doctor">
        /// DoctorRoleDTO object is passed here which will then redirect to the DoctorDetails class 
        /// </param>
        bool IDoctorManager.UpdateDoctor(DoctorRoleDTO doctor)
        {
            return doctorDetails.UpdateDoctor(doctor);

        }

        /// <summary>
        /// Retrieving Doctor Details By DoctorId
        /// </summary>
        /// <param name="id">
        /// Doctor Id is passed here to get the specified doctor details 
        /// </param>
        /// <returns name="doctor">
        /// DoctorRoleDTO type of object is returning from the method containing specified doctor details
        /// </returns>
        DoctorRoleDTO IDoctorManager.GetDoctorsById(int id)
        {
            var doctor = doctorDetails.GetDoctorsById(id);
            return doctor;
        }
        //public void TransferDoctor(DoctorRecommendationDTO doctor)
        //{
        //    doctorDetails.TransferDoctor(doctor);
        //}

        /// <summary>
        /// Transfering Patients to other Doctors
        /// </summary>
        /// <param name="doctor">
        /// DoctorRecommendationDTO doctor object is passed here which will then redirected to the DoctorDetails class
        /// </param>
        bool IDoctorManager.TransferDoctor(DoctorRecommendationDTO doctor)
        {
            return doctorDetails.TransferDoctor(doctor);
        }

        /// <summary>
        /// Retrieving All DoctorSpeciality Details 
        /// </summary>
        /// <returns name="doctorDTOlist">
        /// DoctorSpecialityDTO type of object is returning from the method containing all DoctorSpeciality details
        /// </returns>
        IEnumerable<DoctorSpecialityDTO> IDoctorManager.GetAllDoctorSpecialityTypes()
        {
            var doctorList = doctorDetails.GetAllDoctorSpecialityTypes();
            return doctorList;
        }

        /// <summary>
        /// Retrieving Doctor Details for the Profile Info Page in DoctorDetail Page
        /// </summary>
        /// <param name="id">
        /// Doctor Id is passed here to get the Profile details for each doctor, Id will be redirected to the DoctorDetails class
        /// </param>
        /// <returns name="doctorList">
        /// DoctorDetailDTO object is returning from the method containing specified doctor details
        /// </returns>
        DoctorDetailDTO IDoctorManager.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(int id)
        {
            var doctorList = doctorDetails.GetDoctorByDoctorIdWithSpecializeAreaForProfileDetail(id);
            return doctorList;
        }

        /// <summary>
        /// Retrieving Doctor Details for the Doctor Profile Info Page 
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>DoctorDetailDTO type of object is passed</returns>
        DoctorDetailDTO IDoctorManager.GetDoctorByUserIdForProfileDetail(int id)
        {
            var doctor = doctorDetails.GetDoctorByUserIdForProfileDetail(id);
            return doctor;
        }

        /// <summary>
        /// Retrieving All Doctor Details for the Json methods in DoctorController
        /// </summary>
        /// <returns name="doctorList">
        /// DoctorDetailDTO type of list is returning from the method containing all doctor details
        /// </returns>
        //IEnumerable<DoctorRoleDTO> IDoctorManager.GetAllDoctorDetailsForJson()
        //{
        //    var doctorList = doctorDetails.GetDoctors();
        //    return doctorList;
        //}

        string IDoctorManager.DoctorAlreadyExist(string name)
        {
            string message = string.Empty;
            return message = doctorDetails.DoctorAlreadyExist(name);
        }
        #endregion





        
    }
}
