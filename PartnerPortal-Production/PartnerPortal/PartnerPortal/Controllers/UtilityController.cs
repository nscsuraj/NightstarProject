using Newtonsoft.Json.Linq;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Pages;
using PartnerPortal.Repository;
using PartnerPortal.Utility;
using System.Web.Mvc;
using System.Linq;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Domain.Accounts;
namespace PartnerPortal.Controllers
{
    public class UtilityController : BaseController
    {
        private readonly IEFRepository<PageInfo> _pageInfoRepository;
        private readonly IEFRepository<CMS_ElementProperty> _cmsElementProperty;
        private readonly IEFRepository<ProductDetails> _productDetails;
        private readonly IEFRepository<UploadInformation> _uploadInformationRepository;
        private readonly IEFRepository<Notifications> _notifications;
        private readonly IEFRepository<NotificationRecipients> _notificationRecipients;
        private readonly IEFRepository<SFAccounts> _accounts;
        private CurrentIdentity _identity;

        public UtilityController(IEFRepository<PageInfo> pageInfoRepository,
            IEFRepository<CMS_ElementProperty> cmsElementProperty,
            IEFRepository<ProductDetails> productDetails,
            IEFRepository<UploadInformation> uploadInformationRepository,
             IEFRepository<Notifications> notifications,
             IEFRepository<NotificationRecipients> notificationRecipients,
             IEFRepository<SFAccounts> accounts
            )
        {
            _pageInfoRepository = pageInfoRepository;
            _cmsElementProperty = cmsElementProperty;
            _productDetails = productDetails;
            _uploadInformationRepository = uploadInformationRepository;
            _notifications = notifications;
            _notificationRecipients = notificationRecipients;
            _identity = new CurrentIdentity();

            _accounts = accounts;
        }

        public ActionResult GetPageHtml(int id)
        {
            var html = string.Empty;
            //var ser = new JavaScriptSerializer();
            var jsonString = _pageInfoRepository.Get(x => x.Id == id).CMSJson;

            var obj = JValue.Parse(jsonString);

            //var json = ser.Deserialize<dynamic>(jsonString);
            return Content(new JsonToHtmlParser(_cmsElementProperty, _productDetails, _uploadInformationRepository).Parse(obj), "text/html");
        }

        [ChildActionOnly]
        public ActionResult GetFileContent(string path)
        {
            return new FilePathResult(path, "text/html; charset=utf-8");
        }

        public string GetJSCssInline()
        {
            var resCachedKey = "StarResponsiveCachedJSCSS";
            var cachedRes = HttpContext.Cache[resCachedKey] as string;
            if (cachedRes == null)
            {
                cachedRes = RenderPartialViewToString("Partials/ExternalResourceFileContents");
                HttpContext.Cache[resCachedKey] = cachedRes;
            }
            return cachedRes;
        }

        public ActionResult NotificationDetail(int id)
        {
            var ac = _accounts.GetAll().Where(x => x.PartnerNumber == _identity.PartnerNumber).FirstOrDefault();
            if (ac != null)
            {
                var nt = _notifications.GetById(id);
                var nr = _notificationRecipients.GetAll().Where(x => x.NotificationId == id && (x.Recipient == ac.PartnerNumber || x.Recipient == ac.PartnerType)).ToList();
                if ( nt.Type == 1 || (nr != null && nr.Count > 0))
                {
                    var html = string.Empty;
                    //var ser = new JavaScriptSerializer();
                    var jsonString = _notifications.Get(x => x.Id == id).Detail;

                    var obj = JValue.Parse(jsonString);

                    //var json = ser.Deserialize<dynamic>(jsonString);
                    return Content(new JsonToHtmlParser(_cmsElementProperty, _productDetails, _uploadInformationRepository).Parse(obj), "text/html");
                }
                else
                {
                    return Content("Notification Not Found", "text/html");
                }
            }
            else
            {
                return Content("Notification Not Found","text/html");
            }
        }

    }
}
