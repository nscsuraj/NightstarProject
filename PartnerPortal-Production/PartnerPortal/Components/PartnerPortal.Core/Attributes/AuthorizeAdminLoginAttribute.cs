using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartnerPortal.Core.Attributes
{
    public class AuthorizeAdminLoginAttribute : AuthorizeAttribute
    {
        public bool Terminated { get; set; }
        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var valid = (httpContext.Request.Cookies["PPAdminUserSession"] != null);
            return valid;
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Admin",
                                action = "Index",
                                Terminated = Terminated
                            })
                        );
        }
    }
}
