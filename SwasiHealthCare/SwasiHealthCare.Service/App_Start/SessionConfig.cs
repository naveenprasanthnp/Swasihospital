using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwasiHealthCare.Service.App_Start
{
    public class FilterAttribute
    {
    }
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["UserId"] == null || HttpContext.Current.Session["UserId"].ToString() == string.Empty
                || HttpContext.Current.Session["RoleId"] == null || HttpContext.Current.Session["RoleId"].ToString() == string.Empty)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}