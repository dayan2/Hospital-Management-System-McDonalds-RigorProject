using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View("Error");
        }
        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewData["error_path"] = aspxerrorpath;

            return View();
        }

        public ActionResult AccessDenied(string ErrorMessage)
        {
            ViewData["ErrorMessage"] = ErrorMessage;

            return View("Error");
        }
	}
}