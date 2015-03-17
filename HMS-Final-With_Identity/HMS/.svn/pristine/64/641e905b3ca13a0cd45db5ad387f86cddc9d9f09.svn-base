#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
#endregion

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class ErrorController : Controller
    {

        #region public Methods

        /// <summary>
        /// Redirect to Error view
        /// </summary>
        /// <returns>Error view</returns>
        public ActionResult Index()
        {
            return View("Error");
        }
        /// <summary>
        /// Redirect to NotFound Error page
        /// </summary>
        /// <param name="aspxerrorpath">Error path</param>
        /// <returns></returns>
        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewData["error_path"] = aspxerrorpath;

            return View();
        }
        /// <summary>
        /// Redirect to Error view
        /// </summary>
        /// <param name="ErrorMessage">Error Message going to display</param>
        /// <returns>Error view</returns>
        public ActionResult AccessDenied(string ErrorMessage)
        {
            ViewData["ErrorMessage"] = ErrorMessage;

            return View("Error");
        }
        #endregion
    }
}