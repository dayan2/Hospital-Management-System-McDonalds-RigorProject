using Mcd.HospitalManagement.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Mcd.HospitalManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string[] cookies = Request.Cookies.AllKeys;
            //foreach (string cookie in cookies)
            //{
            //    if (Request.Cookies[cookie] != null)
            //    {
            //        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //    }
            //}
            if ((string)Session["USERROLE"] == null)
            {
                return View();
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Admin)
            {
                return RedirectToAction("Index", "AdminHome");
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Doctor)
            {
                return RedirectToAction("HomePage", "Doctor");   
            }
            if ((UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), Session["USERROLE"].ToString()) == UserRoleEnum.Cashier)
            {
                return RedirectToAction("HomePageForClerk", "HomePage");  
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}