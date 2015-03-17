using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mcd.HospitalManagement.Web.UserIdentityScope
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult(); // Try this but i'm not sure
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary 
            {
                { "action", "AccessDenied" },
                { "controller", "Error" },
                { "ErrorMessage", "Access Denied" },
                { "returnUrl", filterContext.HttpContext.Request.RawUrl}
             });

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}