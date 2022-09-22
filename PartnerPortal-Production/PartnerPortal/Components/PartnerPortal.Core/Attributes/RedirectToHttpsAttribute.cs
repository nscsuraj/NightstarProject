using System;
using System.Configuration;
using System.Web.Mvc;

namespace PartnerPortal.Core.Attributes
{
    public class RedirectToHttpsAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsSecureConnection && Convert.ToBoolean(ConfigurationManager.AppSettings["IsHttpsRequest"]))
            {
                filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.ToString().Replace("http:", "https:"));
                filterContext.Result.ExecuteResult(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
