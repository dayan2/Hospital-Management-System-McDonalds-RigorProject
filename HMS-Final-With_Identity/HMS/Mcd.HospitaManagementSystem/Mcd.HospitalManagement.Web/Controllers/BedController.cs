﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitaManagementSystem.Business.Interface;
using AutoMapper;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitalManagement.Web.Enums;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
#endregion

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class BedController : BaseController
    {

        #region constant
        const string ACCESSDENIED = "AccessDenied";
        const string ERROR = "Error";
        #endregion

        #region Private Fields
        private IBedManager bedManager;
        #endregion

        #region Constructor
        public BedController()
            : base()
        {
            bedManager = new BedManager();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Select all beds saved in table 
        /// </summary>
        /// <returns>Bed data model object</returns>
        public ActionResult Index()
        {
            //Check user alredy logged or not 
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }

            //Check user role that logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                //Get all Bed details
                IEnumerable<BedDTO> lstBed = bedManager.ViewAllBed();


                //Map Bed dto to bed model
                var bedsModel = from c in lstBed
                                select new BedDetailsModel()
                                {
                                    Id = c.Id,
                                    BedTicketNo = c.BedTicketNo,
                                    WardId = c.WardId,
                                    WardNo = c.WardNo
                                };
                return View(bedsModel.ToList());
            }
            //Redirect to error page
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }

        }

        /// <summary>
        /// Select specific Bed information
        /// </summary>
        /// <param name="id">Bed id</param>
        /// <returns>Bed data model type</returns>
        public ActionResult Details(int? id)
        {
            //Check user alredy logged or not 
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Get specific bed detail by bed id
            BedDTO bedDto = bedManager.ViewtBedById(id);

            //Map Bed dto to bed model
            BedDetailsModel bedModel = new BedDetailsModel()
            {
                BedTicketNo = bedDto.BedTicketNo,
                Id = bedDto.Id,
                WardNo = bedDto.WardNo,
                WardId = bedDto.WardId
            };

            if (bedModel == null)
            {
                return HttpNotFound();
            }
            return View(bedModel);
        }

        /// <summary>
        /// Select data to add new bed information 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            //Check user alredy logged or not 
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            //Check user role that logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                BedModel bedModel = new BedModel();
                IWards wardManager = new WardManager();
                IEnumerable<WardDTO> wardList = wardManager.ViewWardDetails();

                bedModel.Wards = wardList.Select(m => new WardModel
                {
                    Id = m.Id,
                    WardNo = m.WardNo
                }).ToList();

                ViewBag.WardId = new SelectList(wardList, "Id", "WardNo");
                return View();
            }
            //Redirect to error page
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

            }
        }


        /// <summary>
        /// Add new bed information to table
        /// </summary>
        /// <param name="bedmodel">Bed data model type object</param>
        /// <returns>Newly added bed information</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WardId,BedTicketNo")] BedDetailsModel bedmodel)
        {
            //Check user role that logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                if (ModelState.IsValid)
                {
                    //Map Bed dto to bed model
                    BedDTO bedDto = new BedDTO()
                    {
                        BedTicketNo = bedmodel.BedTicketNo,
                        WardId = bedmodel.WardId
                    };
                    string displayMessage = string.Empty;

                    //Insert Bed details to the system and return Json result on success
                    if (!bedManager.InsertBed(bedDto))
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
                        return Json(new { Message = displayMessage });
                    }
                    //return Json result on insertion faliure
                    else
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                        return Json(new { Message = displayMessage });
                    }

                }

                //ViewBag.WardId = new SelectList(db.Wards, "Id", "WardNo", bedmodel.WardId);
                return View(bedmodel);
            }
            //Redirect to error page if user dont have permission to this function
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

            }

        }

        /// <summary>
        /// Select specific bed information to edit
        /// </summary>
        /// <param name="id">Specific bed id</param>
        /// <returns>Bed model data</returns>
        public ActionResult Edit(int? id)
        {
            //Check user alredy logged or not 
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Check user role have permission that already logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                //Get bed details by bed id
                BedDTO bedDto = bedManager.ViewtBedById(id);

                //Map Bed dto to bed model
                BedDetailsModel bedModel = new BedDetailsModel()
                {
                    BedTicketNo = bedDto.BedTicketNo,
                    WardId = bedDto.WardId
                };

                if (bedModel == null)
                {
                    return HttpNotFound();
                }
                BedModel bedModelDetails = new BedModel();
                IWards wardManager = new WardManager();

                //Get all ward details
                IEnumerable<WardDTO> wardList = wardManager.ViewWardDetails();

                //Map and WardDTO object to WardModel and put into  bedModelDetails.Wards list
                bedModelDetails.Wards = wardList.Select(m => new WardModel
                {
                    Id = m.Id,
                    WardNo = m.WardNo
                }).ToList();

                //return ward list data to view
                ViewBag.WardId = new SelectList(wardList, "Id", "WardNo", bedModel.WardId);
                return View(bedModel);
            }
            //Redirect to error page
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

            }

        }

        /// <summary>
        /// Add modified bed information to table
        /// </summary>
        /// <param name="bedModel">Bed model data</param>
        /// <returns>Modified bed information</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WardId,BedTicketNo")] BedDetailsModel bedModel)
        {
            //Check user role that logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                if (ModelState.IsValid)
                {
                    //Map Bed dto to bed model
                    BedDTO bedDto = new BedDTO()
                    {
                        BedTicketNo = bedModel.BedTicketNo,
                        Id = bedModel.Id,
                        WardId = bedModel.WardId
                    };
                    string displayMessage = string.Empty;
                    //return Json result on success or faliure
                    if (!bedManager.EditBed(bedDto))
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
                        return Json(new { Message = displayMessage });
                    }
                    else
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
                        return Json(new { Message = displayMessage });
                    }
                }
                return View(bedModel);
            }
            //Redirect to error page
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

            }
        }

        /// <summary>
        /// Select bed informtion to delete
        /// </summary>
        /// <param name="id">Select bed id for delete</param>
        /// <returns>Selected bed information to be deteted</returns>
        public ActionResult Delete(int? id)
        {
            //Check user alredy logged or not 
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Check user role that logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                BedDTO bedDto = bedManager.ViewtBedById(id);

                //Map Bed dto to bed model
                BedDetailsModel bedModel = new BedDetailsModel()
                {
                    BedTicketNo = bedDto.BedTicketNo,
                    WardId = bedDto.WardId
                };


                if (bedModel == null)
                {
                    return HttpNotFound();
                }
                return View(bedModel);
            }
            //Redirect to error page
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

            }
        }

        /// <summary>
        /// Delete bed record on confirmation
        /// </summary>
        /// <param name="id">Bed id</param>
        /// <returns>Record delete confirmation message</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Check user role that logged on
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                string displayMessage = string.Empty;
                //return Json result on success or faliure
                if (!bedManager.DeleteBed(id))
                {
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationErrorMsg);
                    return Json(new { Message = displayMessage });
                }
                else
                {
                    displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationRemovedMsg);
                    return Json(new { Message = displayMessage });
                }
            }
            //Redirect to error page
            else
            {
                return RedirectToAction(ACCESSDENIED, ERROR, new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });

            }

        }
        #endregion

    }
}