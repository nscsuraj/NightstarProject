using System.Linq;
using System.Web.Mvc;

namespace PartnerPortal.Core.Attributes
{
    public class NamespaceAwareViewEngine : RazorViewEngine
    {
        public NamespaceAwareViewEngine()
        {
            // Define the location of the View file
            this.ViewLocationFormats = new string[] { "~/Views/##replace##/{1}/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            this.PartialViewLocationFormats = new string[] { "~/Views/##replace##/{1}/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            var path = GetTokenFromControllerNamespace(controllerContext.Controller as Controller);
            if (partialPath.Contains("##replace##"))
                partialPath = partialPath.Replace("##replace##", path);
            return base.CreatePartialView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var path = GetTokenFromControllerNamespace(controllerContext.Controller as Controller);
            if (viewPath.Contains("##replace##"))
                viewPath = viewPath.Replace("##replace##", path);
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            var path = GetTokenFromControllerNamespace(controllerContext.Controller as Controller);
            if (virtualPath.Contains("##replace##"))
                virtualPath = virtualPath.Replace("##replace##", path);
            return base.FileExists(controllerContext, virtualPath);
        }

        private string GetTokenFromControllerNamespace(Controller controller)
        {
            string[] tokens = controller.GetType().FullName.Split('.');
            if (tokens.Length > 1)
            {
                var arr =
                    tokens.Select((x, index) => new {Key = x, Index = index})
                        .Where(m => m.Index > 1 && m.Index != tokens.Length - 1)
                        .Select(n => n.Key.ToString())
                        .ToArray().Aggregate((current,next)=> current + "/" + next);
                return arr;
            }
            else
                return "";
        }

    }
}