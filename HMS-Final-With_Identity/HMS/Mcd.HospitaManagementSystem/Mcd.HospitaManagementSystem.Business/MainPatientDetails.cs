#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcd.HospitalManagementSystem.Data;
using AutoMapper;
using System.Data.Entity;
# endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class MainPatientDetails
    {
        #region private Variable
        private Patient patient;
        #endregion

        #region Public Methods
        /// <summary>
        /// used to insert all patient basic details of patient 
        /// </summary>
        /// <param name="patientDTO"></param>
        public bool InsertPatient(PatientDTO patientDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    patient = new Patient
                       {
                           Name = patientDTO.Name,
                           NIC = patientDTO.NIC,
                           Address = patientDTO.Address,
                           Gender = patientDTO.Gender,
                           MobileNo = patientDTO.MobileNo,
                       };
                    db.Patients.Add(patient);
                    if (db.SaveChanges() == 1)  //return 1 if save success
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// retrive specific patient basic details
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns name=patientDTO></returns>
        public PatientDTO ViewPatientDetails(int? patientID)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get patients according to patientid
                    patient = db.Patients.Find(patientID);
                    var patientDTO = new PatientDTO()
                      {
                          Id = patient.Id,
                          Name = patient.Name,
                          NIC = patient.NIC,
                          Address = patient.Address,
                          Gender = patient.Gender,
                          MobileNo = patient.MobileNo,

                      };
                    return patientDTO;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// retrive all patient details 
        /// </summary>
        /// <returns name=patientList></returns>
        public IEnumerable<PatientDTO> ViewAllPatientDetails()
        {
            try
            {

                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get all patient details 
                    IEnumerable<PatientDTO> patients = db.Patients.Select(x => new PatientDTO
                    {

                        Id = x.Id,
                        Name = x.Name,
                        NIC = x.NIC,
                        Address = x.Address,
                        Gender = x.Gender,
                        MobileNo = x.MobileNo
                    }).ToList();

                    var patientList = patients;
                    return patientList;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// use to edit patient basic details 
        /// </summary>
        /// <param name="patientDTO"></param>
        public bool EditPatient(PatientDTO patientDTO)
        {

            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    patient = new Patient()
                    {
                        Id = patientDTO.Id,
                        Name = patientDTO.Name,
                        NIC = patientDTO.NIC,
                        Address = patientDTO.Address,
                        Gender = patientDTO.Gender,
                        MobileNo = patientDTO.MobileNo,
                    };

                    db.Entry(patient).State = EntityState.Modified;
                    if (db.SaveChanges() == 1)  //return 1 if save success
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        /// <summary>
        /// use to delete basic patient details
        /// </summary>
        /// <param name="PatientId"></param>
        public bool Deletepatient(int? PatientId)
        {

            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get patient according to patient id
                    patient = db.Patients.Find(PatientId);
                    //delete patient according to patient object
                    db.Patients.Remove(patient);
                    if (db.SaveChanges() == 1)  //return 1 if save success
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;

        }
        /// <summary>
        /// check the availabe patient according to nic
        /// </summary>
        /// <param name="nic"></param>
        /// <returns>status of patient avilability using nic</returns>
        public bool CheckPatientAvilability(string nic)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //check patient is available or not according to nic
                    var patient = db.Patients.Where(x => x.NIC == nic).ToList();
                    if (patient.Count() >= 1)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

    }
}
