using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using PartnerPortal.Business.Email;
using PartnerPortal.Business.Security;
using PartnerPortal.Core.Attributes;
using PartnerPortal.Core.Enumerations;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Domain.Admin;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Gateway;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Models;
using PartnerPortal.Repository;
using PartnerPortal.Utility;

namespace PartnerPortal.Controllers
{
    /// <summary>
    /// Gateway Controller
    /// </summary>
    //[RedirectToHttps]
    public class AdminController : BaseController
    {
        private readonly IEFRepository<CMS_ElementProperty> _cmsElementProperty;
        private readonly IEFRepository<SystemConfig> _systemConfig;
        private readonly IEFRepository<MetaTags> _metaTags;
        private readonly IEFRepository<UploadInformation> _uploadInformationRepository;
        private readonly IEFRepository<SFAccounts> _sfAccounts;
        private readonly ISMNEmailService _emailService;
        private readonly IEFRepository<AdminUsers> _adminUsers;
        private readonly IEncryptionService _encryptionService;
        
        /// <summary>
        /// Gateway Controller
        /// </summary>
        /// <returns></returns>
        public AdminController(
            IEFRepository<CMS_ElementProperty>  cmsElementProperty,
            IEFRepository<SystemConfig> systemConfig,
            IEFRepository<MetaTags> metaTags,
            IEFRepository<UploadInformation> uploadInformationRepository,
            IEFRepository<SFAccounts> sfAccounts,
            ISMNEmailService emailService,
            IEFRepository<AdminUsers> adminUsers,
            IEncryptionService encryptionService
             
            )
        {
            _cmsElementProperty = cmsElementProperty;
            _systemConfig = systemConfig;
            _metaTags = metaTags;
            _uploadInformationRepository = uploadInformationRepository;
            _sfAccounts = sfAccounts;
            _emailService = emailService;
            _adminUsers = adminUsers;
            _encryptionService = encryptionService;
        }

        public ActionResult Index()
        {
            Request.Cookies.Clear();
            return View("Login",new GatewayVM());
        }

        [AuthorizeAdminLogin]
        public ActionResult Dashboard()
        {
            return View();
        }

        //star
        //star@1234
        [HttpPost]
        public ActionResult Login(GatewayVM model)
        {
            var account =
                _adminUsers.Get(
                    x => x.UserName == model.LoginId && x.UserPassword == model.LoginPassword && x.IsActive);
            if (account != null)
            {
                var userJson =
                    new JavaScriptSerializer().Serialize(new {LoginId = model.LoginId, UserId = account.Id});

                var encryptedTicket = _encryptionService.Encrypt(userJson);

                // Create a cookie and add the encrypted ticket to the cookie as data.
                Response.Cookies.Add(new HttpCookie("PPAdminUserSession", encryptedTicket));
                return RedirectToAction("Dashboard");

            }
            else
            {
                ModelState.AddModelError("LoginFailed",
                    "Either user name or password for user name " + model.LoginId + " is incorrect.");
                return View("Login", model);
            }
 
        }

            
        //star
        //star@1234
        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["PPAdminUserSession"] != null)
            {
                HttpCookie myCookie = new HttpCookie("PPAdminUserSession");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Index");
        }
    }
}
