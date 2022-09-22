using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Web.Administration;
using PartnerPortal.Business.Security;
using PartnerPortal.Core.Attributes;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;

namespace PartnerPortal.Controllers.CMS
{
    [AuthorizeCms]
    //[RedirectionChecker]
    public class ConfigureController : Controller
    {

        private readonly IEFRepository<MegaMenu> _menuRepository;
        private readonly IEFRepository<SystemConfig> _systemConfig;
        private readonly IEncryptionService _encryptionService;

        public ConfigureController(IEFRepository<MegaMenu> menuRepository,
            IEFRepository<SystemConfig> systemConfig,
            IEncryptionService encryptionService)
        {
            _menuRepository = menuRepository;
            _systemConfig = systemConfig;
            _encryptionService = encryptionService;
          
        }

        private void SetUserLevel()
        {
            ViewBag.IsAdmin = false;
            var c = Request.Cookies["PPCMSUserSession"];
            if (c != null)
            {
                var v = new JavaScriptSerializer().Deserialize<PartnerPortal.Domain.Admin.UserProfile>(_encryptionService.Decrypt(c.Value));
                if (v.UserLevel == 1)
                {
                    ViewBag.IsAdmin = true;
                }
            }
        }

        #region PP Methods
        [HttpGet]
        public ActionResult SiteHeaderContent()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SiteBannerContent()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SiteGatewayContent()
        {
            return View();
        }

        public ActionResult PasswordEmail()
        {
            SetUserLevel();
            return View();
        }

        #endregion


        public ActionResult Index()
        {
            SetUserLevel();
            return View();
        }

        [HttpGet]
        public ActionResult MegaMenu()
        {
            SetUserLevel();
            return View();
        }

        
        [HttpGet]
        public ActionResult DeletedMegaMenuItem()
        {
            SetUserLevel();
            return View();
        }
        
        [HttpGet]
        public ActionResult ManageCommonPages()
        {
            SetUserLevel();
            return View();
        }
        [HttpGet]
        public ActionResult ManagePageProperties()
        {
            SetUserLevel();
            return View();
        }
        [HttpGet]
        public ActionResult ManageHybridPages()
        {
            SetUserLevel();
            return View();
        }

        //[HttpGet]
        //public ActionResult SEOLinks()
        //{
        //    SetUserLevel();
        //    return View();
        //}


        [HttpGet]
        public ActionResult TagMegaMenuAndPage()
        {
            SetUserLevel();
            return View();
        }

        [HttpGet]
        public ActionResult ManageAllowedPartnerTypesForMenu()
        {
            SetUserLevel();
            return View();
        }
        [HttpGet]
        public ActionResult ManageAllowedPartnerTypesForPageSections()
        {
            SetUserLevel();
            return View();
        }
        [HttpGet]
        public ActionResult NotificationManager()
        {
            SetUserLevel();
            return View();
        }
        //[HttpGet]
        //public ActionResult FeaturedStarProduct()
        //{
        //    SetUserLevel();
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult FooterCms()
        //{
        //    SetUserLevel();
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult FeaturedStarProductDetail(int Id)
        //{
        //    SetUserLevel();
        //    var fspId = Id;
        //    ViewBag.FSPDId = Id;
        //    return View();
        //}

        [HttpGet]
        public ActionResult RestartApplication()
        {
            SetUserLevel();
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Cache)
            {
                HttpContext.Cache.Remove((string)entry.Key);
            }
            //ServerManager serverManager = new ServerManager();
            //ApplicationPoolCollection appPools = serverManager.ApplicationPools;
            //foreach (ApplicationPool ap in appPools)
            //{
            //    if (ap.Name == "SMN2015")
            //        ap.Recycle();
            //}
            var cnf = _systemConfig.Get(x => x.ConfigKey == "RemoveCache");
            cnf.ConfigValue = "1";
            _systemConfig.Update(cnf);
            return RedirectToAction("Index","Dashboard");
        }
        
        public ActionResult Users()
        {
            SetUserLevel();
            return View();
        }

        public ActionResult MetaTagManagement()
        {
            SetUserLevel();
            return View();
        }
        public ActionResult FileTypeManagement()
        {
            SetUserLevel();
            return View();
        }
        public ActionResult LibraryCategoryManagement()
        {
            SetUserLevel();
            return View();
        }
        public ActionResult LibraryFileManagement()
        {
            SetUserLevel();
            return View();
        }
    }
}
