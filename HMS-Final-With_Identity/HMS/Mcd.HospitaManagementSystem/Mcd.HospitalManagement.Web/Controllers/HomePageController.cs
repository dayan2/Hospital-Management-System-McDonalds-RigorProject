#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
#endregion

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class HomePageController : Controller
    {
        //
        // GET: /HomePage/
       
        public ActionResult HomePageForClerk()
        {
            return View();
        }
	}
}