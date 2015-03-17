
#region Using Directives
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
using Mcd.HospitalManagement.Web.Models;
using AutoMapper;
using Mcd.HospitalManagement.Web.Enums;
using Mcd.HospitalManagement.Web;
#endregion


namespace Mcd.HospitalManagement.Web.Controllers
{
    public class UserController : BaseController
    {
        #region Private Fields
        private IUserManager usermanager;
        private LP_HMSDbEntities db = new LP_HMSDbEntities();
        private IUserManager userManager;
        #endregion

        #region Constructor
        
        public UserController()
            : base()
        {
            usermanager = new UserManager();
           
        }

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;

        }
        #endregion

        #region Public Methods
        
        /// <summary>
        /// View all users data in index view
        /// </summary>
        /// <returns>All system user data </returns>
        public ActionResult Index()
        {
            if (Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            } 
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
            IEnumerable<UserDTO> user = usermanager.ViewtAllUser();
            var users = from c in user
                        select new UserModel
                        {
                            Id = c.Id,
                            UserName = c.UserName,
                            Password = c.Password,
                            UserRole =c.UserRole,
                            UserRoleType = c.UserRoleType,
                            UserRoleId = c.UserRoleId
                        };
           
            return View(users);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Select specific user by id 
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>specific user data</returns>
        public ActionResult Details(int id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            } 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserDTO userdto = usermanager.ViewtUserById(id);

            UserModel usermodel = new UserModel()
            {
                Id = userdto.Id,
                UserName = userdto.UserName,
                Password = userdto.Password,
                UserRoleType = userdto.UserRoleType,
                UserRoleId = userdto.UserRoleId
            };

            if (usermodel == null)
            {
                return HttpNotFound();
            }
            return View(usermodel);
        }

        /// <summary>
        /// Redirect to create view 
        /// </summary>
        /// <param name="Id">User Id</param>
        /// <returns>User model data</returns>
        public ActionResult Create(int? Id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Role");
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
           
        }

        /// <summary>
        /// Add new user to the user table
        /// </summary>
        /// <param name="usermodel">new user model data</param>
        /// <returns>Newly added user role data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,UserRoleId,ConfirmPassword")] UserModel usermodel)
        {
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                UserDTO userdto = new UserDTO()
                {
                    Id = usermodel.Id,
                    UserName = usermodel.UserName,
                    Password = usermodel.Password,
                    UserRoleId = usermodel.UserRoleId,
                    ConfirmPassword = usermodel.ConfirmPassword

                };

                if (ModelState.IsValid)
                {
                    string displayMessage = string.Empty;
                    if (!usermanager.InsertUser(userdto))
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(UserErrorMessage.AlreadyExistUser);
                        return Json(new { Message = displayMessage });
                    }
                    else
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationInsertedMsg);
                        return Json(new { Message = displayMessage });
                    }
                }
                ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Role", usermodel.UserRoleId);
                return View(usermodel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Select specific user to edit
        /// </summary>
        /// <param name="id">Specific users id</param>
        /// <returns>Selected user to modified</returns>
        public ActionResult Edit(int id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                usermanager = new UserManager();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserDTO userdto = usermanager.ViewtUserById(id);

                UserModel usermodel = new UserModel()
                {
                    Id = userdto.Id,
                    UserName = userdto.UserName,
                    Password = userdto.Password,
                    UserRoleId = userdto.UserRoleId
                };

                if (usermodel == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Role", usermodel.UserRoleId);
                return View(usermodel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }
      
        /// <summary>
        /// Added modified users data to the table 
        /// </summary>
        /// <param name="user">User view model data</param>
        /// <returns>Modified users data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,UserRoleId,ConfirmPassword")] UserModel user)
        {
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                UserDTO userdto = new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    UserRoleId = user.UserRoleId,
                    ConfirmPassword = user.ConfirmPassword
                };

                if (ModelState.IsValid)
                {
                    string displayMessage = string.Empty;
                    if (!usermanager.EditUser(userdto))
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(UserErrorMessage.AlreadyExistUser);
                        return Json(new { Message = displayMessage });
                    }
                    else
                    {
                        displayMessage = CustomEnumMessage.GetStringValue(ConfirmationMessages.ConfirmationUpdatedMsg);
                        return Json(new { Message=displayMessage});
                     
                    }
                }
                ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Role", user.UserRoleId);
                return View(user);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Select specific data to modified
        /// </summary>
        /// <param name="id">Specific users id</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            } 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                UserDTO userdto = usermanager.ViewtUserById(id);

                UserModel usermodel = new UserModel()
                {
                    Id = userdto.Id,
                    UserName = userdto.UserName,
                    Password = userdto.Password,
                    UserRoleId = userdto.UserRoleId
                };

                if (usermodel == null)
                {
                    return HttpNotFound();
                }
                return View(usermodel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
        /// Delete user specific user data on confirmation
        /// </summary>
        /// <param name="id">Selected user id to be deleted</param>
        /// <returns>Nothing</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                usermanager = new UserManager();
                string displayMessage = string.Empty;

                if (!usermanager.DeleteUser(id))
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
            else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }

        /// <summary>
       /// Get selected users data according to user role for display in partial view
       /// </summary>
       /// <param name="filterId">User role id</param>
       /// <returns>Selected users data</returns>
        public ActionResult RenderFilteredUserGrid(int? filterId)
        {
            if (filterId == 0)
            {
                IEnumerable<UserDTO> user = usermanager.ViewtAllUser();
                var users = from c in user
                            select new UserModel
                            {
                                Id = c.Id,
                                UserName = c.UserName,
                                Password = c.Password,
                                UserRoleType = c.UserRoleType,
                                UserRoleId = c.UserRoleId
                            };

                return PartialView("UserDetailsPartial", users);

            }

            if (filterId == 1)
            {
                IEnumerable<UserDTO> user = usermanager.ViewtUserByUserRoleId("Admin");

                var users = from c in user
                            select new UserModel
                            {
                                Id = c.Id,
                                UserName = c.UserName,
                                Password = c.Password,
                                UserRoleType = c.UserRoleType,
                                UserRoleId = c.UserRoleId
                            };


                return PartialView("UserDetailsPartial", users);
          
            }

            if (filterId == 2)
            {
                IEnumerable<UserDTO> user = usermanager.ViewtUserByUserRoleId("Doctor");

                var users = from c in user
                            select new UserModel
                            {
                                Id = c.Id,
                                UserName = c.UserName,
                                Password = c.Password,
                                UserRoleType = c.UserRoleType,
                                UserRoleId = c.UserRoleId
                            };

                return PartialView("UserDetailsPartial", users);
            }
            if (filterId == 3)
            {
                IEnumerable<UserDTO> user = usermanager.ViewtUserByUserRoleId("Cashier");

                var users = from c in user
                            select new UserModel
                            {
                                Id = c.Id,
                                UserName = c.UserName,
                                Password = c.Password,
                                UserRoleType = c.UserRoleType,
                                UserRoleId = c.UserRoleId
                            };

                return PartialView("UserDetailsPartial", users);
            }
            return null;
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index", "AdminHome"); 
        }
        #endregion

    }
}
