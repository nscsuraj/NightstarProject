using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PartnerPortal.Domain.Pages;
using PartnerPortal.Models;
using PartnerPortal.Repository;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PartnerPortal.Core.Attributes;
using PartnerPortal.Core.Enumerations;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Domain.Admin;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Utility;

namespace PartnerPortal.Controllers
{
    //[RedirectionChecker]
    public class PagesController : BaseController
    {
        private readonly IEFRepository<PageInfo> _pageInfoRepository;
        //private readonly IEFRepository<SiteFooter> _siteFooter;
        private readonly IEFRepository<CMS_ElementProperty> _cmsElementProperty;
        private readonly IEFRepository<ProductDetails> _productDetails;
        private readonly IEFRepository<MetaTags> _metaTags;
        private readonly IEFRepository<UploadInformation> _uploadInformation;
        private readonly IEFRepository<MegaMenu> _megaMenu;

        private readonly IEFRepository<SFAccounts> _accounts;
        private readonly IEFRepository<SalesforceAuthentication> _authenticationToken;
        private readonly IEFRepository<SFTempSessionOpportunityData> _sessionOpportunityData;
        private readonly IEFRepository<SFTempSessionOpportunityProducts> _sessionOpportunityProduct;
        private readonly IEFRepository<SFTempSessionPurchaseByDistributors> _sessionPurchaseByDistributors;
        private readonly IEFRepository<SFTempSessionPurchaseByProductClasses> _sessionPurchaseByProductClasses;
        private readonly IEFRepository<SFTempSessionMdfData> _sessionMdfRepository;
        private readonly IEFRepository<PageViews> _pageViews;
        private readonly IEFRepository<SFTempSessionDelegateData> _delegateReport;
        private readonly IEFRepository<SFTempSessionDelegateRebateItemData> _delegateRebateItemReport;
        private readonly IEFRepository<Notifications> _notifications;
        private readonly IEFRepository<NotificationRecipients> _notificationRecipients;

        private CurrentIdentity _identity;

        public PagesController(IEFRepository<PageInfo> pageInfoRepository,
            //IEFRepository<SiteFooter> siteFooter,
            IEFRepository<CMS_ElementProperty> cmsElementProperty,
            IEFRepository<ProductDetails> productDetails,
            IEFRepository<MetaTags> metaTags,
            IEFRepository<MegaMenu> megaMenu,
            IEFRepository<UploadInformation> uploadInformation,

            IEFRepository<SFAccounts> accounts,
            IEFRepository<SalesforceAuthentication> authenticationToken,
            IEFRepository<SFTempSessionOpportunityData> sessionOpportunityData,
            IEFRepository<SFTempSessionOpportunityProducts> sessionOpportunityProduct,
            IEFRepository<SFTempSessionPurchaseByDistributors> sessionPurchaseByDistributors,
            IEFRepository<SFTempSessionPurchaseByProductClasses> sessionPurchaseByProductClasses,
            IEFRepository<SFTempSessionMdfData> sessionMdfRepository,
            IEFRepository<SFTempSessionDelegateData> delegateReport,
            IEFRepository<SFTempSessionDelegateRebateItemData> delegateRebateItemReport,
            IEFRepository<PageViews> pageViews,
            IEFRepository<Notifications> notifications,
            IEFRepository<NotificationRecipients> notificationRecipients
            )
        {
            _pageInfoRepository = pageInfoRepository;
            //_siteFooter = siteFooter;
            _cmsElementProperty = cmsElementProperty;
            _productDetails = productDetails;
            _metaTags = metaTags;
            _megaMenu = megaMenu;
            _uploadInformation = uploadInformation;
            _identity = new CurrentIdentity();

            _accounts = accounts;
            _authenticationToken = authenticationToken;
            _sessionOpportunityData = sessionOpportunityData;
            _sessionOpportunityProduct = sessionOpportunityProduct;
            _sessionPurchaseByDistributors = sessionPurchaseByDistributors;
            _sessionPurchaseByProductClasses = sessionPurchaseByProductClasses;
            _sessionMdfRepository = sessionMdfRepository;
            _pageViews = pageViews;
            _delegateReport = delegateReport;
            _delegateRebateItemReport = delegateRebateItemReport;
            _notifications = notifications;
            _notificationRecipients = notificationRecipients;
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            var page = _pageInfoRepository.GetById(id);
            
            if (page == null)
            {
                return new HttpNotFoundResult("Requested page does not exists");
            }

            var model = new PageInfoVM();
            model.Id = page.Id;
            model.Title = page.Title;
            model.TitleTag = page.TitleTag;
            model.PageHeader = page.PageHeader;
            model.LayoutType = page.LayoutType;
            model.Gateway = new GatewayVM();
            model.Gateway.MetaTags = _metaTags.GetMany(x => x.PageId == page.Id).ToList();
            model.PageSections = new List<PageInfo>();
            //var footer = _siteFooter.GetAll().FirstOrDefault();
            //if (footer != null)
            //{
            //    var html = string.Empty;
            //    //var ser = new JavaScriptSerializer();
            //    var jsonString = footer.FooterCms;

            //    var obj = JValue.Parse(jsonString);

            //    //var json = ser.Deserialize<dynamic>(jsonString);
            //    model.Gateway.FooterHtml = new JsonToHtmlParser(_cmsElementProperty,_productDetails,_uploadInformation).Parse(obj);
            //    model.Gateway.Footer = footer;
            //}
            if (page.PageType == (int)PageTypes.Hybrid)
            {
                // RedirectToAction(page.Title);
                model.PageSections =
                    _pageInfoRepository.GetAll()
                        .Where(x => x.PageType == (int) PageTypes.PageSections && x.PageId == id)
                        .ToList();
                return View(string.Format("~/Views/Pages/{0}.cshtml", id), model);
            }
            return View(model);
        }

        private void ReloadSFData(string id)
        {
            var account = _accounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);

            var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProduct,
                  _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
                  _sessionMdfRepository, _accounts, _delegateReport, _delegateRebateItemReport, _identity.SessionKey);
            sfClient.ProcessAccountData(account);
            _accounts.Update(account);
            //MDF
            if (!string.IsNullOrEmpty(account.LoyaltyLevel) && (id.ToLower() == "mdfrequests" || id.ToLower() == "dashboard"))
            {
                if (account.LoyaltyLevel.ToLower().Contains("gold") ||
                    account.LoyaltyLevel.ToLower().Contains("platinum"))
                {
                    sfClient.ReloadSFMDFData(account);                    
                }
            }
        }

        [HttpGet]
        public ActionResult GetPageByPageName(string id)
        {
            var page = _pageInfoRepository.Get(x=> x.Title == id);
            if (page != null && !string.IsNullOrEmpty(page.Title) && page.Title.ToLower() == "loyaltyleadregistration")
            {
                if (!string.IsNullOrEmpty(page.AllowedPartnerTypes) && !page.AllowedPartnerTypes.Replace("Partner", "").ToLower().Contains(_identity.PartnerType.Replace("Partner", "").ToLower()))
                {
                    page = null;
                }
            }
            if (page == null)
            {
                var g = ConfigurationManager.AppSettings["ErrorPageName"];
                page = _pageInfoRepository.Get(x => x.Title == g);
            }            
            ReloadSFData(id);
            var menu = _megaMenu.GetAll().FirstOrDefault(x => x.PageTitle == id);
            var model = new PageInfoVM();
            model.Id = page.Id;
            model.Title = page.Title;
            model.TitleTag = page.TitleTag;
            model.LayoutType = page.LayoutType;
            model.Gateway = new GatewayVM();
            model.Gateway.MetaTags = _metaTags.GetMany(x => x.PageId == page.Id).ToList();
            model.PageSections = new List<PageInfo>();
            model.MenuName = string.Empty;
            if (menu != null) model.MenuName = menu.Title.Split('-')[0].Replace(" ","");
            model.PageHeader = page.PageHeader;

            var pv = new PageViews
            {
                PageName = id,
                PageId = page.Id,
                PartnerNumber = _identity.PartnerNumber,
                VisitDate = DateTime.Now
            };
            _pageViews.Add(pv);

            //var footer = _siteFooter.GetAll().FirstOrDefault();
            //if (footer != null)
            //{
            //    var html = string.Empty;
            //    //var ser = new JavaScriptSerializer();
            //    var jsonString = footer.FooterCms;

            //    var obj = JValue.Parse(jsonString);

            //    //var json = ser.Deserialize<dynamic>(jsonString);
            //    model.Gateway.FooterHtml = new JsonToHtmlParser(_cmsElementProperty, _productDetails, _uploadInformation).Parse(obj);
            //    model.Gateway.Footer = footer;
            //}
            if (page.PageType == (int)PageTypes.Hybrid)
            {
                model.PageSections =
                   _pageInfoRepository.GetAll()
                       .Where(x => x.PageType == (int)PageTypes.PageSections && x.PageId == model.Id && (string.IsNullOrEmpty(x.AllowedPartnerTypes) || x.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0)).OrderBy(m => m.SectionOrder)
                       .ToList();
               // RedirectToAction(page.Title);
                return View(string.Format("~/Views/Pages/{0}.cshtml",id), model);
            }
            return View("~/Views/Pages/Index.cshtml", model);
        }

        [HttpGet]
        public ActionResult NotificationDetail(int id)
        {
            var nt = _notifications.GetById(id);
            var nr = _notificationRecipients.GetAll().Where(x => x.NotificationId == id && (x.Recipient == _identity.PartnerNumber || x.Recipient == _identity.PartnerType)).ToList();
            var notf = _notifications.GetById(id);
            var model = new PageInfoVM();
            model.Id = id;
            if (nt.Type == 1 || (nr != null && nr.Count > 0))
            {
                model.Title = notf.Header;
            }
            else
            {
                model.Title = string.Empty;
            }

            return View("~/Views/Pages/NotificationDetail.cshtml", model);
        }

    }
}
