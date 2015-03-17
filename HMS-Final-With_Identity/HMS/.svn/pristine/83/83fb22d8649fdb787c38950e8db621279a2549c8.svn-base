#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcd.HospitalManagementSystem.Data;
using AutoMapper;
using System.Data.Entity;
using System.Collections;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public class PatientAdmissionDetails
    {
        #region private Variable
        private IPatientManager patientManager;
        private PatientDetail patientDetails;
        private Invoice invoice;
        private PatientFeedback patientFeedback;
        private PatientMedicalTest patientMedicalTest;
        #endregion


        #region Public Methods
        /// <summary>
        /// Use to insert admission details for patient
        /// </summary>
        /// <param name="patientAdmissionDTO"></param>
        /// 
        public bool InsertPatientAdmissionDetails(PatientAdmissionDTO patientAdmissionDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    patientDetails = new PatientDetail()
                          {
                              PatientDetailId = patientAdmissionDTO.PatientDetailId,
                              AdmitDate = patientAdmissionDTO.AdmitDate,
                              BedId = patientAdmissionDTO.BedId,
                              WardId = patientAdmissionDTO.WardId,
                              DoctorId = patientAdmissionDTO.DoctorId,
                              PatientId = patientAdmissionDTO.PatientId,
                              IsDischarged = patientAdmissionDTO.IsDischarged,


                          };

                    //add patient admission details to PatientDetails table
                    db.PatientDetails.Add(patientDetails);
                    //return 1 if save success
                    if (db.SaveChanges() == 1)
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
        /// retriving all patient
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientDTO> FillPatients()
        {
            try
            {
                patientManager = new PatientManager();
                //view all patient details
                return patientManager.ViewAllPatientDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// use to edit patient admission details
        /// </summary>
        /// <param name="PatientAdmissionDTO"></param>
        public bool EditPatientAdmissionDetails(AllPatientDTO PatientAdmissionDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    patientDetails = new PatientDetail()
                     {
                         PatientDetailId = PatientAdmissionDTO.PatientDetailId,
                         AdmitDate = PatientAdmissionDTO.AdmitDate,
                         BedId = PatientAdmissionDTO.BedId,
                         WardId = PatientAdmissionDTO.WardId,
                         DoctorId = PatientAdmissionDTO.DoctorId,
                         PatientId = PatientAdmissionDTO.PatientId,
                         IsDischarged = PatientAdmissionDTO.IsDischarged,

                     };


                    db.Entry(patientDetails).State = EntityState.Modified;
                    //return 1 if save success
                    if (db.SaveChanges() == 1)
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
        /// use to view Patient Admission Details according t0 patientDetailId
        /// </summary>
        /// <param name="PatientDetailID"></param>
        /// <returns name=patientAdmissionDTO></returns>
        public PatientAdmissionDTO ViewPatientAdmissionDetails(int? PatientDetailID)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get patientdetails records according to patientdetailsId
                    patientDetails = db.PatientDetails.Find(PatientDetailID);
                    PatientAdmissionDTO patientAdmissionDTO; patientAdmissionDTO = new PatientAdmissionDTO()
                    {
                        PatientDetailId = patientDetails.PatientDetailId,
                        AdmitDate = patientDetails.AdmitDate,
                        BedId = patientDetails.BedId,
                        WardId = patientDetails.WardId,
                        DoctorId = patientDetails.DoctorId,
                        PatientId = patientDetails.PatientId,
                        IsDischarged = patientDetails.IsDischarged,

                    };

                    return patientAdmissionDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }



        /// <summary>
        /// use to view all admission details
        /// </summary>
        /// <returns name=patientAdmissionList></returns>
        public IEnumerable<PatientAdmissionDTO> ViewAllAdmissionPatientDetails()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get all patient admission details form patientdetails table
                    IList<PatientDetail> patientDetails = db.PatientDetails.ToList();
                    Mapper.CreateMap<PatientDetail, PatientAdmissionDTO>();
                    var patientAdmissionList = Mapper.Map<IEnumerable<PatientDetail>, IEnumerable<PatientAdmissionDTO>>(patientDetails);
                    return patientAdmissionList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// use to delete patient admission details according to patient id
        /// </summary>
        /// <param name="PatientId"></param>
        public bool DeletePatientAdmissionDetails(int? PatientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get patient admission details form PatientDetails according to patient id
                    patientDetails = db.PatientDetails.Find(PatientId);
                    db.PatientDetails.Remove(patientDetails);
                    //return 1 if save success
                    if (db.SaveChanges() == 1)
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
        /// view patient admission details according to patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns name=patientAdmissionDetails></returns>
        public AllPatientDTO viewPatientRelatedData(int? patientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get patient details according to patient id by joing Patients,Doctors,Wards,Beds
                    IEnumerable<AllPatientDTO> allDetails = db.PatientDetails.Where(c => c.PatientDetailId == patientId)
                                .Join(db.Patients, x => x.PatientId, s => s.Id,
                                  ((x, s) => new { x, s }))
                                     .Join(db.Doctors, p => p.x.DoctorId, d => d.Id,
                                ((p, d) => new { p, d }))
                                    .Join(db.Wards, n => n.p.x.WardId, a => a.Id,
                                    ((n, a) => new { n, a }))
                                   .Join(db.Beds, d => d.n.p.x.BedId, f => f.Id,
                                   ((d, f) => new { d, f }))
                                .Select(m => new AllPatientDTO
                                {
                                    PatientDetailId = m.d.n.p.x.PatientDetailId,
                                    PatientId = m.d.n.p.s.Id,
                                    Address = m.d.n.p.s.Address,
                                    MobileNo = m.d.n.p.s.MobileNo,
                                    AdmitDate = m.d.n.p.x.AdmitDate,
                                    Name = m.d.n.p.s.Name,
                                    WardNo = m.d.a.WardNo,
                                    BedTicketNo = m.f.BedTicketNo,
                                    DoctorName = m.d.n.d.Name,
                                    DoctorId = m.d.n.d.Id,
                                    WardId = m.d.a.Id,
                                    BedId = m.f.Id,
                                    IsDischarged = m.d.n.p.x.IsDischarged,
                                }).ToList();

                    var patientAdmissionDetails = allDetails.First();
                    return patientAdmissionDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }



        /// <summary>
        /// view doctor according to patientid
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns name=doctorlist></returns>
        public IEnumerable<AllPatientDTO> ViewDoctorAccordingToPatientId(int patientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //joining patientdetails and doctors tables and get doctor details according to patient id
                    IEnumerable<AllPatientDTO> allDetails = db.PatientDetails.Where(j => j.PatientId == patientId)
                                  .Join(db.Doctors, x => x.DoctorId, s => s.Id,
                                    ((x, s) => new { x, s })).Select(m => new AllPatientDTO
                          {
                              DoctorId = m.s.Id,
                              DoctorName = m.s.Name,
                          }).Distinct().ToList();

                    var doctorlist = allDetails;
                    return doctorlist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// view all patients admission details
        /// </summary>
        /// <returns name=patientAdmissionDetails></returns>
        public IEnumerable<AllPatientDTO> ViewAllPatientAdmissionDetails()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get all pateint details by joining Patients,Doctors,Wards,Beds tables
                    IEnumerable<AllPatientDTO> allDetails = db.PatientDetails
                                       .Join(db.Patients, x => x.PatientId, s => s.Id,
                                         ((x, s) => new { x, s }))
                                            .Join(db.Doctors, p => p.x.DoctorId, d => d.Id,
                                       ((p, d) => new { p, d }))
                                           .Join(db.Wards, n => n.p.x.WardId, a => a.Id,
                                           ((n, a) => new { n, a }))
                                          .Join(db.Beds, d => d.n.p.x.BedId, f => f.Id,
                                          ((d, f) => new { d, f }))
                                       .Select(m => new AllPatientDTO
                                       {
                                           PatientDetailId = m.d.n.p.x.PatientDetailId,
                                           PatientId = m.d.n.p.s.Id,
                                           Address = m.d.n.p.s.Address,
                                           MobileNo = m.d.n.p.s.MobileNo,
                                           AdmitDate = m.d.n.p.x.AdmitDate,
                                           Name = m.d.n.p.s.Name,
                                           WardNo = m.d.a.WardNo,
                                           BedTicketNo = m.f.BedTicketNo,
                                           DoctorName = m.d.n.d.Name,
                                           DoctorId = m.d.n.d.Id,
                                           WardId = m.d.a.Id,
                                           BedId = m.f.Id,
                                           IsDischarged = m.d.n.p.x.IsDischarged,
                                       }).ToList();

                    var patientAdmissionDetails = allDetails;
                    return patientAdmissionDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// use to view patient feedback details
        /// </summary>
        /// <returns name=patientFeedbackDetails></returns>
        public IEnumerable<PatientFeedbackDTO> ViewAllPatientFeedbackDetails()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //view all patient feedback detils by joining patients and doctors table
                    IEnumerable<PatientFeedbackDTO> allFeedBackDetails = db.PatientFeedbacks
                                     .Join(db.PatientDetails, x => x.PatientDetailId, s => s.PatientDetailId,
                                       ((x, s) => new { x, s }))
                                          .Join(db.Doctors, p => p.x.DoctorId, d => d.Id,
                                     ((p, d) => new { p, d }))
                                          .Join(db.Patients, n => n.p.s.PatientId, a => a.Id,
                                         ((n, a) => new { n, a }))
                                     .Select(m => new PatientFeedbackDTO
                                     {
                                         Id = m.n.p.x.id,
                                         PatientDetailId = m.n.p.s.PatientDetailId,
                                         DoctorId = m.n.d.Id,
                                         DoctorName = m.n.d.Name,
                                         FeedbackDate = m.n.p.x.FeedbackDate,
                                         FeedbackDescription = m.n.p.x.FeedbackDescription,
                                         PatientName = m.a.Name
                                     }).ToList();

                    var patientFeedbackDetails = allFeedBackDetails;
                    return patientFeedbackDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// view patient feedback details according to patient feedback id
        /// </summary>
        /// <param name="patientFeedBackId"></param>
        /// <returns name=feedback></returns>
        public PatientFeedbackDTO ViewAllPatientFeedbackDetailsAccordingToId(int? patientFeedBackId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //view patient feedback details according to feedback id 
                    IEnumerable<PatientFeedbackDTO> allFeedBackDetails = db.PatientFeedbacks.Where(w => w.id == patientFeedBackId)
                                   .Join(db.PatientDetails, x => x.PatientDetailId, s => s.PatientDetailId,
                                     ((x, s) => new { x, s }))
                                        .Join(db.Doctors, p => p.x.DoctorId, d => d.Id,
                                   ((p, d) => new { p, d }))
                                        .Join(db.Patients, n => n.p.s.PatientId, a => a.Id,
                                       ((n, a) => new { n, a }))
                                   .Select(m => new PatientFeedbackDTO
                                   {
                                       Id = m.n.p.x.id,
                                       PatientDetailId = m.n.p.s.PatientDetailId,
                                       DoctorId = m.n.d.Id,
                                       DoctorName = m.n.d.Name,
                                       FeedbackDate = m.n.p.x.FeedbackDate,
                                       FeedbackDescription = m.n.p.x.FeedbackDescription,
                                       PatientId = m.a.Id,
                                       PatientName = m.a.Name
                                   }).ToList();

                    var feedback = allFeedBackDetails.First();
                    return feedback;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// check avilabilty of patient discharge status according to patiient id
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public bool CheckPatientAdmissionAvilabilty(int? patientID)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    //check there are records in the PatientDetails table according to id
                    bool p = db.PatientDetails.Any(x => x.PatientId == patientID);
                    if (p == true)
                    {
                        //get patient admission details according to patientid and order by decending order according to admitdate
                        patientDetails = db.PatientDetails.Where(x => x.PatientId == patientID).OrderByDescending(t => t.AdmitDate).FirstOrDefault();
                        //check patient is discharged or not 
                        if ((bool)patientDetails.IsDischarged)
                            return true;
                    }
                    else
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// check relationship between invoice and patientdetails
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        public bool CheckRelationshipBetweenPatientDetailsAndInvoice(int? detailId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    //check if there is invoice according to patientdetailId
                    bool p = db.Invoices.Any(x => x.PatientDetailId == detailId);
                    if (p == true)
                    {
                        //get invoice details according to patientdetailid
                        invoice = db.Invoices.Where(x => x.PatientDetailId == detailId).OrderByDescending(t => t.InvoiceDate).FirstOrDefault();
                        return true;
                    }
                    else

                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// check relationship between patientdetails and feedback
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns>status of avilability of feedback</returns>
        public bool CheckRelationshipBetweenPatientDetailsAndpatientFeedback(int? detailId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    //check feedback avilabilty according to patientdetailId
                    bool p = db.PatientFeedbacks.Any(x => x.PatientDetailId == detailId);
                    if (p == true)
                    {
                        //get feedback details according to PatientDetailId
                        patientFeedback = db.PatientFeedbacks.Where(x => x.PatientDetailId == detailId).FirstOrDefault();
                        return true;
                    }
                    else

                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// check relationship between patientdetails and medical test
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns>status of ralationship between patient and patient medical test</returns>
        public bool CheckRelationshipBetweenPatientDetailsAndpatientMediaclTest(int? detailId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    //check the availability of medical test according to patientdetailId
                    bool p = db.PatientMedicalTests.Any(x => x.PatientDetailId == detailId);
                    if (p == true)
                    {
                        //get the details of medical test according to patientdetailId
                        patientMedicalTest = db.PatientMedicalTests.Where(x => x.PatientDetailId == detailId).FirstOrDefault();
                        return true;
                    }
                    else

                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// check retaionship between patient and patient admissiondetils according to patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>status of relationship between patient aND PATIENT DETAILS</returns>
        public bool CheckRelationshipBetweenPatientAndPatientDetails(int? patientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //check status of availble data for patientId in PatientDetails  table
                    var avilability = db.PatientDetails.Any(x => x.PatientId == patientId);
                    if (avilability == true)
                    {
                        //get patient details according to patient id
                        patientDetails = db.PatientDetails.Where(x => x.PatientId == patientId).OrderByDescending(t => t.AdmitDate).FirstOrDefault();
                        if (patientDetails.PatientId != 0 && patientDetails.PatientId != null)
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


        /// <summary>
        /// get patientdetailID according to PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns name=patientList></returns>
        public PatientAdmissionDTO GetPatientDetailId(int? patientId)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //check patient details accoding to patient id
                    var avilability = db.PatientDetails.Any(x => x.PatientId == patientId);
                    if (avilability == true)
                    {
                        //get patient details according to patient id
                        patientDetails = db.PatientDetails.Where(x => x.PatientId == patientId).OrderByDescending(t => t.AdmitDate).FirstOrDefault();

                    }
                    var patientList = new PatientAdmissionDTO()
                    {
                        PatientDetailId = patientDetails.PatientDetailId,
                        AdmitDate = patientDetails.AdmitDate,
                        IsDischarged = patientDetails.IsDischarged
                    };
                    return patientList;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// use to insert patient feedback
        /// </summary>
        /// <param name="patientFeedbackDTO"></param>
        public bool InsertPatientFeedBack(PatientFeedbackDTO patientFeedbackDTO)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    patientFeedback = new PatientFeedback()
                    {
                        DoctorId = patientFeedbackDTO.DoctorId,
                        PatientDetailId = patientFeedbackDTO.PatientDetailId,
                        FeedbackDate = patientFeedbackDTO.FeedbackDate,
                        FeedbackDescription = patientFeedbackDTO.FeedbackDescription,


                    };
                    db.PatientFeedbacks.Add(patientFeedback);
                    //return 1 if save success
                    if (db.SaveChanges() == 1)
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
        /// get all patient details for doctor by userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>patient details</returns>
        public IEnumerable<PatientDoctorDTO> GetAllPatientDetailsForDoctor(int? userId)
        {
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                if (userId == null)
                    throw new NullReferenceException();
                //get doctor according to userid
                var doctor = db.Doctors.Where(e => e.UserId == userId).FirstOrDefault();
                //get patientdetails according to doctor id
                var patientDetailList = db.PatientDetails.Where(e => e.DoctorId == doctor.Id).ToList();
                //get patients
                var patientList = db.Patients.ToList();

                var returningPatientList = patientDetailList.Join(patientList,
                    e => e.PatientId, m => m.Id,
                    (e, m) => new { e, m }).Where(y => y.e.PatientId == y.m.Id).OrderBy(c => c.e.AdmitDate).
                    Select(x => new PatientDoctorDTO
                    {
                        Id = x.m.Id,
                        Name = x.m.Name,
                        AdmitDate = x.e.AdmitDate,
                        Gender = x.m.Gender,
                        Bed = x.e.BedId,
                        Ward = x.e.WardId,
                        IsDischarged = x.e.IsDischarged
                    }).ToList();
                return returningPatientList;
            }
        }

        /// <summary>
        /// view doctor feedback
        /// </summary>
        /// <param name="id"></param>
        /// <returns name=selectedDoctorDetail></returns>
        public IList<DoctorDetailDTO> ViewDoctorFeedback(int? id)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //get patient details for doctors by joining doctor table
                    var doctorList = db.PatientDetails.
                           Join(db.PatientFeedbacks, pd => pd.PatientDetailId, pf => pf.PatientDetailId, ((pd, pf) => new { pd, pf })).
                           Where(x => x.pd.DoctorId != x.pf.DoctorId && x.pd.PatientDetailId == x.pf.PatientDetailId).
                           Join(db.Doctors, p => p.pd.DoctorId, d => d.Id, ((p, d) => new { p, d }))
                           .Select(m => new DoctorDetailDTO
                           {
                               Id = m.d.Id,
                               Name = m.d.Name

                           }).ToList();

                    var selectedDoctorDetail = doctorList;
                    return selectedDoctorDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// use to edit patient feedback details
        /// </summary>
        /// <param name="PatientAdmissionDTO"></param>
        public bool EditPatientFeedbackDetails(PatientFeedbackDTO patientFeedback)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    PatientFeedback patientFeedBackDeails = new PatientFeedback()
                    {
                        id = patientFeedback.Id,
                        PatientDetailId = patientFeedback.PatientDetailId,
                        DoctorId = patientFeedback.DoctorId,
                        FeedbackDate = patientFeedback.FeedbackDate,
                        FeedbackDescription = patientFeedback.FeedbackDescription


                    };
                    db.Entry(patientFeedBackDeails).State = EntityState.Modified;
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
        /// use to edit patient feedback details
        /// </summary>
        /// <param name="PatientAdmissionDTO"></param>
        public bool EditPatientsFeedbackDetails(PatientFeedbackDTO patientFeedback)
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {

                    PatientFeedback patientFeedBackDeails = new PatientFeedback()
                    {
                        id = patientFeedback.Id,
                        PatientDetailId = patientFeedback.PatientDetailId,
                        DoctorId = patientFeedback.DoctorId,
                        FeedbackDate = patientFeedback.FeedbackDate,
                        FeedbackDescription = patientFeedback.FeedbackDescription



                    };
                    db.Entry(patientFeedBackDeails).State = EntityState.Modified;
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
        /// view all patients according to patient patient admission
        /// </summary>
        /// <returns name=patientAdmissionDetails></returns>
        public IEnumerable<PatientDTO> ViewAllPatientDetailsAccordingToAdmission()
        {
            try
            {
                using (LP_HMSDbEntities db = new LP_HMSDbEntities())
                {
                    //join patient and patientdetails tables and get patients that are avilable in patient details
                    IEnumerable<PatientDTO> allDetails = db.Patients
                                                .Join(db.PatientDetails, x => x.Id, s => s.PatientId,
                                                  ((x, s) => new { x, s }))

                                                .Select(m => new PatientDTO
                                                {
                                                    Id = m.x.Id,
                                                    Name = m.x.Name
                                                }).Distinct().ToList();

                    var patientAdmissionDetails = allDetails;
                    return patientAdmissionDetails;
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
