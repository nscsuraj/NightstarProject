using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PartnerPortal.Business.Security;
using PartnerPortal.Core.Attributes;
using PartnerPortal.Domain.Pages;
using PartnerPortal.Models;
using PartnerPortal.Repository;

namespace PartnerPortal.Controllers.CMS
{
    [AuthorizeCms]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        private readonly IEFRepository<PageInfo> _pageInfoRepository;
        private readonly IEncryptionService _encryptionService;

        public DashboardController(IEFRepository<PageInfo> pageInfoRepository,
            IEncryptionService encryptionService)
        {
            _pageInfoRepository = pageInfoRepository;
            _encryptionService = encryptionService;
        }


        [HttpGet]
        public ActionResult Index()
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
            return View();
        }

        [HttpGet]
        public ActionResult Editor()
        {
            var model = new EditorVM
            {
                Templates = _pageInfoRepository.GetMany(x=> x.IsTemplate).ToList(),
                CustomTemplates = _pageInfoRepository.GetMany(x => x.IsCustomTemplate).ToList()
            };


            return View(model);
        }


    }
}
