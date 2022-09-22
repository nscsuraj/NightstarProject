using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace PartnerPortal.Core.Attributes
{
    public class RedirectionCheckerAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var vd = ConfigurationManager.AppSettings["VirtualDirectory"].ToString();
            //var context = filterContext.HttpContext;
            //if (context.Request.Url != null && context.Request.UrlReferrer  != null && context.Request.UrlReferrer.OriginalString.Contains("/" + vd))
            //{
            //        var baseUrl = GetBaseUrl(context);
            //        var url = context.Request.Url.ToString();
            //        var redirectUrl = url.Replace(baseUrl, baseUrl + vd + "/");
            //        filterContext.Result =
            //        new RedirectResult(redirectUrl);
            //        filterContext.Result.ExecuteResult(filterContext);
            //}
            base.OnActionExecuting(filterContext);
        }

        public string GetBaseUrl(HttpContextBase context)
        {
            var request = context.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }
}
