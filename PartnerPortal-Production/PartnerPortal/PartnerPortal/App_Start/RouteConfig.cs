using MvcCodeRouting;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartnerPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute(".woff");

            routes.MapCodeRoutes(
                rootController: typeof (Controllers.GatewayController),
                settings: new CodeRoutingSettings
                {
                    UseImplicitIdToken = true
                });

            routes.MapCodeRoutes("cms",
                rootController:typeof(Controllers.CMS.LoginController));

            routes.MapCodeRoutes("admin",
                rootController: typeof(Controllers.AdminController));

            routes.MapRoute(
              "Pages", "Pages/{Id}",
              new { controller = "Pages", action = "Index" },
              new { Id = @"\d+" }
              );

            routes.MapRoute(
              "PagesByPageName", "Pages/{Id}",
              new { controller = "Pages", action = "GetPageByPageName" }
              );
            //routes.MapRoute(
            //  "DoRememberLogin", "dorememberlogin/{Id}",
            //  new { controller = "DoRememberLogin", action = "PPLogIn" }
            //  );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

        }
    }
}