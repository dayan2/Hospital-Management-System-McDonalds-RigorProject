#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Mcd.HospitalManagement.Web.Models;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagement.Web.Enums;
using System.Security.Principal;
using Mcd.HospitalManagementSystem.Data;
using System.Globalization;
using System.Web.Security;
#endregion
namespace Mcd.HospitalManagement.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Constant
        int UserId;
        const string DOCTOR = "Doctor";
        const string ADMIN = "Admin";
        const string CASHIER = "Cashier";
        const string HOMEPAGE = "HomePage";
        const string ADMINHOMEINDEX = "Index";
        const string ADMINHOME = "AdminHome";
        const string HOMEPAGEFORCLERK = "HomePageForClerk";
        const string INDEX= "Index";
        const string HOME = "Home";
        const string LOGIN = "Login";
        const string ACCOUNT = "Account";
        private const string XsrfKey = "XsrfId"; // Used for XSRF protection when adding external logins

        #endregion

        #region public variables
        public IUserManager usermanager;
        public UserManager<ApplicationUser> UserManager { get; private set; }

        #endregion

        #region public constructors
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
            : base()
        {
            UserManager = userManager;

        }
        #endregion

        #region public methods

        /// <summary>
        /// Redirect to Login view
        /// </summary>
        /// <returns>Login view</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Check authentication details on user login instance
        /// </summary>
        /// <param name="model">Login model details on login instance</param>
        /// <returns>Instance that logged in</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                //Map login model details for to user objects
                UserDTO userDTO = new UserDTO()
                   {
                       UserName = model.UserName,
                       Password = model.Password
                   };


                usermanager = new UserManager();

                //Get authenticated user id
                UserId = usermanager.CheckUserAuthentication(userDTO);

                //check authenticated user is valid or in valid 
                if (UserId == 0)
                {
                    ModelState.AddModelError("", CustomEnumMessage.GetStringValue(ConfirmationMessages.LoginErrorMsg));
                }
                else
                {
                    SetupFormsAuthTicket(UserId, true);

                    //get authorized user details
                    UserDTO userdto = usermanager.ViewtUserById(UserId);

                    //map data to user model
                    UserModel usermodel = new UserModel()
                    {
                        Id = userdto.Id,
                        UserName = userdto.UserName,
                        Password = userdto.Password,
                        UserRoleId = userdto.UserRoleId,
                        UserRole = userdto.UserRole
                    };

                    if (usermodel == null)
                    {
                        return HttpNotFound();
                    }

                    //put specific data to sessions
                    Session["USERID"] = usermodel.Id;
                    Session["USERROLEID"] = usermodel.UserRoleId;
                    Session["USERROLE"] = usermodel.UserRole.Role;


                    //refer user to specific home directory according to user role
                    if (usermodel.UserRole.Role == DOCTOR)
                    {
                        //Redirect to doctor home page
                        System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction(HOMEPAGE, DOCTOR);
                    }
                    if (usermodel.UserRole.Role == ADMIN)
                    {
                        //Redirect to doctor admin home page
                        System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction(ADMINHOMEINDEX, ADMINHOME);
                    }
                    if (usermodel.UserRole.Role == CASHIER)
                    {
                        //Redirect to doctor cashier home page
                        System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction(HOMEPAGEFORCLERK, HOMEPAGE);
                    }


                }

            }
            return View(model);
            // If we got this far, something failed, redisplay form
        }

        private User SetupFormsAuthTicket(int Id, bool persistanceFlag)
        {
            User user;
            using (var dbContext = new LP_HMSDbEntities())
            {
                user = dbContext.Users.Find(Id);
            }
            var userId = user.Id;
            var userData = userId.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                user.UserName, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration
                                persistanceFlag, //Persistent
                                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return user;
        }


        /// <summary>
        /// Redirect user to account login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return RedirectToAction(LOGIN, ACCOUNT);
        }

        /// <summary>
        /// Account log off function
        /// </summary>
        /// <returns>Redirect to account login view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            System.Web.Security.FormsAuthentication.SignOut();
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction(LOGIN, ACCOUNT);
        }
        #endregion

        #region Helpers
        
        #region Private Propertise
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Private methods 
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
       

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(INDEX, HOME);
            }
        }
        #endregion

        #region Public methods
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        #endregion


        #region private class
        private class ChallengeResult : HttpUnauthorizedResult
        {
            #region public variables
            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            #endregion

            #region public methods
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

           

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
            #endregion

        #endregion

        #endregion


    }
}