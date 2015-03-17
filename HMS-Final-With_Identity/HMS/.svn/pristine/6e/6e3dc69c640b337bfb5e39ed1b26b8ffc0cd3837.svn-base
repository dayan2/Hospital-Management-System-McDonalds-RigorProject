using Mcd.HospitalManagement.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class AdminHomeController : Controller
    {
        //
        // GET: /AdminHome/
        public ActionResult Index()
        {
            if ((string)Session["USERROLE"] == null)
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.InvalidLogin) });
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                return View("AdminHome");
            }
             else
            {
                return RedirectToAction("AccessDenied", "Error", new { ErrorMessage = CustomEnumMessage.GetStringValue(PageStatusEnum.AccessDenied) });
            }
        }
        
    }
}
