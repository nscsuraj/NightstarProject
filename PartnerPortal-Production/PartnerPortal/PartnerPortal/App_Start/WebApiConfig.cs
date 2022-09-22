using PartnerPortal.AppRegistration.StructureMap;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace PartnerPortal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiForType",
                routeTemplate: "api/{controller}/{action}/{type}",
                defaults: new { type = RouteParameter.Optional }
            );


            config.Services
                  .Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}