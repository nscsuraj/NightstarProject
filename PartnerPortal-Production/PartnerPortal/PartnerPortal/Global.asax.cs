using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MvcCodeRouting;
using PartnerPortal.AppRegistration.StructureMap;
using PartnerPortal.AutoMapper;
using StructureMap;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.IO;
using System.Web.Script.Serialization;
using PartnerPortal.Business.Security;

namespace PartnerPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// member variables.
        /// </summary>
        private IContainer _iocContainer;
        private readonly IList<string> _ExtensionBlackList;
        private readonly IList<string> _RequestURLsBlackList;

        public MvcApplication()
        {
            _ExtensionBlackList = GetExtensionBlackList();
            _RequestURLsBlackList = GetRequestURLsBlackList();
        }

        protected void Application_Start()
        {
            var registrar = new StructureMapIoCContainerRegistrar();
            _iocContainer = registrar.SetupAppContainer();

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(_iocContainer));

            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.Services
            //      .Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));
            
            AutoMapperBootStrapper.Initialize();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            RegisterViewEngines(ViewEngines.Engines);
        }

        public void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
        {
            bool foundExtensionToIgnore = _ExtensionBlackList.Any(i => i.ToLower() == Path.GetExtension(Request.FilePath).ToLower());

            bool foundURLsToIgnore = Request.RawUrl.Length == 1 || _RequestURLsBlackList.Any(i => Request.RawUrl.ToLower().Contains(i.ToLower()));

            if (Request.RawUrl.ToLower().EndsWith("partnerportal") ||
                Request.RawUrl.ToLower().EndsWith("partnerportal/"))
            {
                foundURLsToIgnore = true;
            }

            if (foundExtensionToIgnore  || foundURLsToIgnore)
            {
                return;
            }

            DoFormsAuthentication();
        }

        protected void Application_BeginRequest()
        {
            //if (!Request.IsSecureConnection)
            //{
            //    if (Request.Url.AbsoluteUri.Contains("/es/"))
            //    {
            //        if (Request.Url.AbsoluteUri == "http://starmicronics/es" ||
            //            Request.Url.AbsoluteUri == "http://starmicronics/es/")
            //        {
            //           // Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            //        }
            //    }
            //    else
            //    {
            //       // Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            //    }
            //}
        }
        void RegisterViewEngines(ViewEngineCollection viewEngines)
        {

            // Call AFTER you are done making changes to viewEngines
            viewEngines.EnableCodeRouting();
        }

        /// <summary>
        /// Does the forms authentication.
        /// </summary>
        private void DoFormsAuthentication()
        {
            bool needLogin = true;
            string myUsername = string.Empty;
            string myID = string.Empty;
            //check to see if SEIS Cookie exists          
            //user has valid Star session key.  Setup .net principal and identity for user
            var ppCookie = Request.Cookies["PPCookie"];
            if (ppCookie != null)
            {
                try
                {
                    var _encryptionService = new EncryptionService();
                    var v = new JavaScriptSerializer().Deserialize<PartnerPortal.Domain.Accounts.PPUsers>(_encryptionService.Decrypt(ppCookie.Value));

                    myUsername = v.PartnerNumber;
                    myID = v.Id.ToString();

                    var expTime = Convert.ToInt32(ConfigurationManager.AppSettings["cookieExpireTime"].ToString());

                    var userJsonTemp = new JavaScriptSerializer().Serialize(new { Id = v.Id, AccountId = v.AccountId, SessionKey = v.SessionKey, PartnerNumber = v.PartnerNumber, PartnerType = v.PartnerType, RememberMe = v.RememberMe });
                    var encryptedTicketTemp = _encryptionService.Encrypt(userJsonTemp);
                    var cookieName = new HttpCookie("PPCookie", encryptedTicketTemp);
                    //cookieName["UserName"] = myUsername;
                    //cookieName["Id"] = Convert.ToString(myID);
                    //cookieName["IsAdmin"] = Convert.ToString(cokkie["IsAdmin"]);
                    //cookieName["IsCoordinator"] = Convert.ToString(cokkie["IsCoordinator"]);
                    //cookieName["StaffID"] = Convert.ToString(cokkie["StaffID"]);
                    cookieName.Expires = DateTime.Now.AddDays(expTime);
                    Response.Cookies.Add(cookieName);
                    needLogin = false;

                }
                catch (Exception)
                {
                    needLogin = true;
                }


            }

            if (needLogin)
            {
                RedirectToLogin();
            }
        }

        /// <summary>
        /// Sets up the URL for redirecting the user and redirects them.
        /// </summary>
        /// <param name="action">Additional Query String Parameters</param>
        private void RedirectToLogin()
        {
            //string redurl = ConfigurationManager.AppSettings["rootUrl"].ToString() + "/Logon/Index";
            string redurl = ConfigurationManager.AppSettings["rootUrl"].ToString();
            Response.Redirect(redurl);
        }

        public IList<string> GetExtensionBlackList()
        {
            var extensionBlackList = new List<string>
										 {
											 ".asp",
                                             ".aspx",
											 ".axd",
											 ".gif",
											 ".png",
											 ".ascx",
											 ".asmx",
											 ".jpg",
											 ".jpeg",
											 ".css",
											 ".vbs",
                                             ".xml",
                                             ".txt",
											 ".js",
											 ".manifest",
											 ".pdf"
										 };

            return extensionBlackList;
        }

        public IList<string> GetRequestURLsBlackList()
        {
            var requestURLsBlackList = new List<string>
										   {                                              
                                               "CMS",
                                               "Admin",
                                               "admin",
                                               "api",
                                               "Register",
                                               "sitemap",
                                               "404-error",
                                               "api/pageinfo",
											   "Login/Index",
                                               "404",
                                               "error",
                                               "ForgotPassword",
                                               "SendResetPasswordLink",
                                               "ResetPassword",
                                               "Login", 
                                               "dorememberlogin",
                                               "Home/Index",
                                               "Images/UploadPreview",
                                               "Images/ShowPhotoTemp",
                                               "Images"
											};

            return requestURLsBlackList;
        }
    }
}