using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Shared.CustomModels;

namespace School.Web.App_Start
{
    public class SessionExpireAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check  sessions here
            if (HttpContext.Current.Session[CommonStrings.UserSession] == null)
            {
                HttpContext.Current.Session.Abandon();
                filterContext.Result = new System.Web.Mvc.RedirectResult("~/Login/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}