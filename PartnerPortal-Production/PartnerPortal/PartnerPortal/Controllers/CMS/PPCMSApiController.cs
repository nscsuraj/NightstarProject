using Microsoft.Ajax.Utilities;
using PartnerPortal.Business.Email;
using PartnerPortal.Business.Security;
using PartnerPortal.Core.Enumerations;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Domain.Admin;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Pages;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;
using PartnerPortal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Script.Serialization;
using PartnerPortal.Domain.Import;
using Newtonsoft.Json.Linq;
using System.Web;

namespace PartnerPortal.Controllers.CMS
{
    [RoutePrefix("api/ppcmsapi")]
    public class PPCMSApiController : BaseApiController
    {
        private readonly IEFRepository<UserProfile> _users;
        private readonly IEFRepository<MetaTags> _metaTags;
        private readonly IEFRepository<MegaMenu> _menu;
        private readonly IEFRepository<PageInfo> _pages;
        private readonly IEncryptionService _encryptionService;
        private readonly IEFRepository<SFTempSessionData> _sessionData;
        private readonly IEFRepository<SFAccounts> _sfAccounts;
        private readonly IEFRepository<SystemConfig> _systemConfig;
        private readonly IEFRepository<SFTempSessionOpportunityData> _sessionOpportunityData;
        private readonly IEFRepository<SFTempSessionOpportunityProducts> _sessionOpportunityProducts;
        private readonly IEFRepository<SalesforceAuthentication> _authenticationToken;
        private readonly IEFRepository<RegistrationInfo> _RegistrationInfo;
        private readonly IEFRepository<SFTempSessionPurchaseByDistributors> _sessionPurchaseByDistributors;
        private readonly IEFRepository<SFTempSessionPurchaseByProductClasses> _sessionPurchaseByProductClasses;
        private readonly IEFRepository<PortalItemMaster> _portalItemMaster;
        private readonly IEFRepository<SFTempSessionMdfData> _sessionMdfRepository;
        private readonly IEFRepository<ProductClassToDisplay> _productClassToDisplay;
        private readonly IEFRepository<FileTypeConfig> _fileTypeConfig;
        private readonly IEFRepository<LibraryCategory> _libraryCategory;
        private readonly IEFRepository<LibraryFiles> _libraryFiles;
        private readonly IEFRepository<OpportunityAdded> _opportunityAdded;
        private readonly IEFRepository<MdfAdded> _mdfAdded;

        private readonly IEFRepository<SFUserSession> _sfUserSession;
        private readonly IEFRepository<PasswordGeneraionLog> _passwordGeneraionLog;
        private readonly IEFRepository<PageViews> _pageViews;
        private readonly ISMNEmailService _emailService;
        private readonly IEFRepository<SFTempSessionDelegateData> _delegateReport;
        private readonly IEFRepository<SFTempSessionDelegateRebateItemData> _delegateRebateItemReport;
        private readonly IEFRepository<RebateRequest> _rebateRequest;
        private readonly IEFRepository<RebateRequestDetail> _rebateRequestDetail;
        private readonly IEFRepository<RebateRequestFiles> _rebateRequestFiles;
        private readonly IEFRepository<Notifications> _notifications;
        private readonly IEFRepository<NotificationRecipients> _notificationRecipients;
        private readonly IEFRepository<NotificationReadBy> _notificationReadBy;
        private CurrentIdentity _identity;

        private readonly IEFRepository<CMS_ElementProperty> _cmsElementProperty;
        private readonly IEFRepository<ProductDetails> _productDetails;
        private readonly IEFRepository<UploadInformation> _uploadInformationRepository;
        private readonly IEFRepository<SFTempSessionLoyaltRegistration> _sfTempSessionLoyaltRegistration;
        private readonly IEFRepository<SFTempSessionDemoUnitRequested> _sfTempSessionDemoUnitRequested;
        private readonly IEFRepository<SFTempSessionTrainingRequested> _sfTempSessionTrainingRequested;

        private readonly IEFRepository<ImportedMarketingMaterial> _importedMarketingMaterial;
        private readonly IEFRepository<ImportedMarketingMaterialCategories> _importedMarketingMaterialCategories;
        private readonly IEFRepository<ImportedMarketingMaterialImages> _importedMarketingMaterialImages;
        private readonly IEFRepository<ImportedResources> _importedResource;
        public PPCMSApiController(
             IEFRepository<UserProfile> users,
             IEncryptionService encryptionService,
             IEFRepository<MetaTags> metaTags,
             IEFRepository<PageInfo> pages,
             IEFRepository<SFTempSessionData> sessionData,
             IEFRepository<RegistrationInfo> RegistrationInfo,
             IEFRepository<SFAccounts> sfAccounts,
             IEFRepository<SFTempSessionOpportunityData> sessionOpportunityData,
             IEFRepository<SFTempSessionOpportunityProducts> sessionOpportunityProducts,
             IEFRepository<SalesforceAuthentication> authenticationToken,
             IEFRepository<SFTempSessionPurchaseByDistributors> sessionPurchaseByDistributors,
             IEFRepository<SFTempSessionPurchaseByProductClasses> sessionPurchaseByProductClasses,
             IEFRepository<PortalItemMaster> portalItemMaster,
             IEFRepository<SFTempSessionMdfData> sessionMdfRepository,
             IEFRepository<ProductClassToDisplay> productClassToDisplay,
             IEFRepository<MegaMenu> menu,
             IEFRepository<FileTypeConfig> fileTypeConfig,
             IEFRepository<LibraryCategory> libraryCategory,
             IEFRepository<LibraryFiles> libraryFiles,
             IEFRepository<OpportunityAdded> opportunityAdded,
             IEFRepository<MdfAdded> mdfAdded,
             IEFRepository<SystemConfig> systemConfig,
             IEFRepository<SFUserSession> sfUserSession,
             IEFRepository<PageViews> pageViews,
             ISMNEmailService emailService,
             IEFRepository<SFTempSessionDelegateData> delegateReport,
             IEFRepository<SFTempSessionDelegateRebateItemData> delegateRebateItemReport,
             IEFRepository<RebateRequest> rebateRequest,
             IEFRepository<RebateRequestDetail> rebateRequestDetail,
             IEFRepository<RebateRequestFiles> rebateRequestFiles,
             IEFRepository<PasswordGeneraionLog> passwordGeneraionLog,
             IEFRepository<Notifications> notifications,
             IEFRepository<NotificationRecipients> notificationRecipients,
             IEFRepository<NotificationReadBy> notificationReadBy,
              IEFRepository<CMS_ElementProperty> cmsElementProperty,
            IEFRepository<ProductDetails> productDetails,
            IEFRepository<UploadInformation> uploadInformationRepository,
            IEFRepository<SFTempSessionLoyaltRegistration> sfTempSessionLoyaltRegistration,
            IEFRepository<SFTempSessionDemoUnitRequested> sfTempSessionDemoUnitRequested,
            IEFRepository<SFTempSessionTrainingRequested> sfTempSessionTrainingRequested,
            IEFRepository<ImportedMarketingMaterial> importedMarketingMaterial,
            IEFRepository<ImportedMarketingMaterialCategories> importedMarketingMaterialCategories,
            IEFRepository<ImportedMarketingMaterialImages> importedMarketingMaterialImages,
            IEFRepository<ImportedResources> importedResource
        )
        {
            _users = users;
            _encryptionService = encryptionService;
            _metaTags = metaTags;
            _pages = pages;
            _sessionData = sessionData;
            _RegistrationInfo = RegistrationInfo;
            _identity = new CurrentIdentity();
            _systemConfig = systemConfig;
            _sfAccounts = sfAccounts;
            _sessionOpportunityData = sessionOpportunityData;
            _sessionOpportunityProducts = sessionOpportunityProducts;
            _sessionPurchaseByDistributors = sessionPurchaseByDistributors;
            _authenticationToken = authenticationToken;
            _sessionPurchaseByProductClasses = sessionPurchaseByProductClasses;
            _portalItemMaster = portalItemMaster;
            _menu = menu;
            _sessionMdfRepository = sessionMdfRepository;
            _productClassToDisplay = productClassToDisplay;
            _fileTypeConfig = fileTypeConfig;
            _libraryCategory = libraryCategory;
            _libraryFiles = libraryFiles;
            _opportunityAdded = opportunityAdded;
            _mdfAdded = mdfAdded;
            _sfUserSession = sfUserSession;
            _passwordGeneraionLog = passwordGeneraionLog;
            _pageViews = pageViews;
            _emailService = emailService;
            _delegateReport = delegateReport;
            _delegateRebateItemReport = delegateRebateItemReport;
            _rebateRequest = rebateRequest;
            _rebateRequestDetail = rebateRequestDetail;
            _rebateRequestFiles = rebateRequestFiles;
            _notifications = notifications;
            _notificationRecipients = notificationRecipients;
            _notificationReadBy = notificationReadBy;
            _cmsElementProperty = cmsElementProperty;
            _productDetails = productDetails;
            _uploadInformationRepository = uploadInformationRepository;
            _sfTempSessionLoyaltRegistration = sfTempSessionLoyaltRegistration;
            _sfTempSessionDemoUnitRequested = sfTempSessionDemoUnitRequested;
            _sfTempSessionTrainingRequested = sfTempSessionTrainingRequested;
            _importedMarketingMaterial = importedMarketingMaterial;
            _importedMarketingMaterialCategories = importedMarketingMaterialCategories;
            _importedMarketingMaterialImages = importedMarketingMaterialImages;
            _importedResource = importedResource;
        }

        [Route("GetUsers")]
        [System.Web.Http.HttpGet]
        public object GetUsers()
        {
            var users = new List<dynamic>();
            var result = _users.GetAll().ToList();
            foreach (var u in result)
            {
                dynamic output = new ExpandoObject();
                output.Id = u.Id;
                output.LoginId = u.LoginId;
                output.LoginPassword = _encryptionService.Decrypt(u.LoginPassword);
                output.FirstName = u.FirstName;
                output.LastName = u.LastName;
                output.Email = u.Email;
                output.Phone = u.Phone;
                output.UserLevel = u.UserLevel.ToString();
                output.Additional = u.Additional;
                users.Add(output);
            }
            return users;
        }

        [Route("SaveUser")]
        [System.Web.Http.HttpPost]
        public object SaveUser(dynamic user)
        {
            if (user.Id != null && user.Id > 0)
            {
                var newUser = _users.GetById((int)user.Id);
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Email = user.Email;
                newUser.LoginId = user.LoginId;
                newUser.Phone = user.Phone;
                newUser.LoginPassword = _encryptionService.Encrypt(Convert.ToString(user.LoginPassword));
                newUser.UserLevel = user.UserLevel;
                newUser.Additional = user.Additional;

                _users.Update(newUser);
            }
            else
            {
                var newUser = new UserProfile();
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Email = user.Email;
                newUser.LoginId = user.LoginId;
                newUser.Phone = user.Phone;
                newUser.LoginPassword = _encryptionService.Encrypt(Convert.ToString(user.LoginPassword));
                newUser.UserLevel = user.UserLevel;
                newUser.Additional = user.Additional;

                _users.Add(newUser);
                user.Id = newUser.Id;

            }
            return user;
        }

        [Route("DeleteUser")]
        [System.Web.Http.HttpPost]
        public void DeleteUser(dynamic user)
        {
            var id = (int)user.Id;

            _users.Delete(x => x.Id == id);
        }


        [Route("GetMetaTags")]
        [System.Web.Http.HttpGet]
        public object GetMetaTags()
        {
            var tags = new List<dynamic>();
            var result = _metaTags.GetAll().OrderBy(x => x.PageId).ToList();
            var prevPageId = -1;
            foreach (var u in result)
            {
                dynamic output = new ExpandoObject();
                output.Id = u.Id;
                output.PageId = u.PageId.ToString();
                output.PageTitle = _pages.GetById(u.PageId).Title; ;
                output.HideThisRow = true;
                if (prevPageId != u.PageId)
                {
                    output.HideThisRow = false;
                }
                //output.PageTitleOnRowHide = _pages.GetById(u.PageId).Title;
                prevPageId = u.PageId;
                output.TagType = u.TagType.ToString();
                output.TagTypeTitle = u.TagType == 0 ? "Open Graph Protocol" : "Normal";
                output.TagKey = u.TagKey;
                output.TagValue = u.TagValue;
                output.Expanded = false;
                tags.Add(output);
            }
            return tags;
        }

        [Route("GetMetaTagsByPageId/{id:int}")]
        [System.Web.Http.HttpGet]
        public object GetMetaTagsByPageId(int id)
        {
            var tags = new List<dynamic>();
            var result = _metaTags.GetMany(x => x.PageId == id).ToList();
            foreach (var u in result)
            {
                dynamic output = new ExpandoObject();
                output.Id = u.Id;
                output.PageId = u.PageId.ToString();
                output.TagType = u.TagType.ToString();
                output.TagTypeTitle = u.TagType == 0 ? "Open Graph Protocol" : "Normal";
                output.TagKey = u.TagKey;
                output.TagValue = u.TagValue;
                output.Expanded = false;
                tags.Add(output);
            }
            return tags;
        }

        [Route("SaveMetaTag")]
        [System.Web.Http.HttpPost]
        public object SaveMetaTag(dynamic tag)
        {
            if (tag.Id != null && tag.Id > 0)
            {
                var newTag = _metaTags.GetById((int)tag.Id);
                newTag.PageId = tag.PageId;
                newTag.TagType = tag.TagType;
                newTag.TagKey = tag.TagKey;
                newTag.TagValue = tag.TagValue;
                _metaTags.Update(newTag);
            }
            else
            {
                var newTag = new MetaTags();
                newTag.PageId = tag.PageId;
                newTag.TagType = tag.TagType;
                newTag.TagKey = tag.TagKey;
                newTag.TagValue = tag.TagValue;

                _metaTags.Add(newTag);
                tag.Id = newTag.Id;

            }
            return tag;
        }

        [Route("DeleteUser")]
        [System.Web.Http.HttpPost]
        public void DeleteMetaTag(dynamic tag)
        {
            var id = (int)tag.Id;

            _metaTags.Delete(x => x.Id == id);
        }


        [Route("GetHybridPageSections")]
        [System.Web.Http.HttpGet]
        public object GetHybridPageSections()
        {
            var ser = new JavaScriptSerializer();
            var sections = new List<dynamic>();
            var result = _pages.GetAll().Where(x => x.PageType == 4 && x.PageId.HasValue).OrderBy(m => m.PageId).ThenBy(x => x.SectionOrder).ToList();
            var prevPageId = -1;
            foreach (var u in result)
            {
                var pageId = u.PageId.HasValue ? u.PageId.Value : 0;
                dynamic output = new ExpandoObject();
                output.Id = u.Id;
                output.PageId = pageId.ToString();
                output.PageTitle = _pages.GetById(pageId).Title; ;
                output.HideThisRow = true;
                if (prevPageId != u.PageId)
                {
                    output.HideThisRow = false;
                }
                //output.PageTitleOnRowHide = _pages.GetById(u.PageId).Title;
                prevPageId = pageId;
                output.Title = u.Title;
                output.TitleTage = u.TitleTag;
                output.PageHeader = u.PageHeader;
                output.Description = u.Description;
                output.Status = u.Status;
                output.CreateDate = u.CreateDate;
                output.LayoutType = u.LayoutType;
                output.IsTemplate = u.IsTemplate;
                output.SectionOrder = u.SectionOrder;
                output.IsCustomTemplate = u.IsCustomTemplate;
                output.SearchTextGlobal = u.Title + " " + u.Description;
                output.Sites = (string.IsNullOrEmpty(u.Sites) ? string.Empty : u.Sites);
                output.PageJson = string.IsNullOrEmpty(u.CMSJson) ? "" : ser.Deserialize<dynamic>(Convert.ToString(u.CMSJson));
                output.Expanded = false;
                sections.Add(output);
            }
            return sections;
        }

        [Route("GetAccountFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetAccountFromSessionData()
        {

            //var data = _sessionData.GetAll()
            //    .Where(
            //        x =>
            //            x.SessionKey ==
            //                _identity.SessionKey && x.AccountId == _identity.AccountId &&
            //                x.PartnerNumber == _identity.PartnerNumber)
            //    .FirstOrDefault();
            var data = _sfAccounts.GetAll()
                .Where(
                    x => x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber)
                .FirstOrDefault();
            if (data != null)
            {
                return new
                {
                    Id = data.Id,
                    AccountName = data.AccountName,
                    AccountId = data.AccountId,
                    AccountEmail = data.AccountEmail,
                    PartnerNumber = data.PartnerNumber,
                    AccountExpert = data.AccountExpert,
                    AccountExpertPhone = data.AccountExpertPhone,
                    AccountExpertEmail = string.IsNullOrEmpty(data.AccountExpertEmail) ? "empower@starmicronics.com" : data.AccountExpertEmail,
                    SupportExpert = data.SupportExpert,
                    SupportExpertPhone = data.SupportExpertPhone,
                    SupportExpertEmail = string.IsNullOrEmpty(data.SupportExpertEmail) ? "empower@starmicronics.com" : data.SupportExpertEmail,
                    PartnerProgramStatus = data.PartnerProgramStatus,
                    PartnerType = string.IsNullOrEmpty(data.PartnerType) ? string.Empty : data.PartnerType,
                    LoyaltyLevel = string.IsNullOrEmpty(data.LoyaltyLevel) ? string.Empty : data.LoyaltyLevel,
                    DiscountRate = string.IsNullOrEmpty(data.DiscountRate) ? "0%" : data.DiscountRate,
                    Revenue6Month = ConvertSFMoneyToDouble(data.Revenue6Month).ToString("C0"),
                    Revenue12Month = ConvertSFMoneyToDouble(data.Revenue12Month).ToString("C0"),
                    Rate612 = data.Rate612,
                    AccountPassword = data.AccountPassword,
                    AccountShortName = data.AccountShortName,
                    Region = data.Region,
                    Growth = GetGrowthPercentage(data.Revenue6Month, data.Revenue12Month),
                    DistributorDiscount = data.DistributorDiscount,
                    SuggestedMarkup = data.SuggestedMarkup,
                    IsStockingPartner = data.IsStockingPartner.HasValue ? data.IsStockingPartner.Value : false
                };
            }
            return new SFAccounts();
        }

        [Route("GetOpportunitiesFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetOpportunitiesFromSessionData()
        {
            var data = _sessionOpportunityData.GetAll()
                .Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber && filterOpportunity(x.Status, x.CancelledDate)).Select(m => new
                            {
                                SessionKey = m.SessionKey,
                                AccountId = m.AccountId,
                                PartnerNumber = m.PartnerNumber,
                                AccountName = m.AccountName,
                                OpportunityId = m.OpportunityId,
                                OpportunityName = m.OpportunityName,
                                OpportunityType = m.OpportunityType,
                                TotalOpportunityQuantity = m.TotalOpportunityQuantity,
                                TotalProductQuantity = m.TotalProductQuantity,
                                Amount = m.Amount,
                                StageName = m.StageName,
                                CloseDate = m.CloseDate,
                                IsClosed = m.IsClosed,
                                Description = m.Description,
                                CreatedDate = m.CreatedDate.ConvertToDateTime(),
                                Products = _sessionOpportunityProducts.GetAll().Where(c => c.SessionKey == _identity.SessionKey && c.AccountId == _identity.AccountId && c.PartnerNumber == _identity.PartnerNumber && c.OpportunityId == m.OpportunityId && c.RefId == m.Id).ToList(),
                                ProductOfInterest = string.IsNullOrEmpty(m.ProductInterest) ? new List<string>() : m.ProductInterest.Split(';').ToList(),
                                BusinessCase = m.BusinessCase,
                                Distributor = m.Distributor,
                                ProductQuantity = m.ProductQuantity,

                                Status = m.Status,
                                SupportRequest = m.SupportRequest,
                                StarExpert = m.StarExpert,
                                StarExpertEmail = m.StarExpertEmail,
                                StarUpdate = m.StarUpdate,
                                CancelledDate = m.CancelledDate

                            }).OrderByDescending(m => m.CreatedDate)

                            .ToList();
            if (data != null) return data;
            return new List<SFTempSessionOpportunityData>();
        }

        private bool filterOpportunity(string status, string cancelledDate)
        {

            if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(cancelledDate))
            {
                return true;
            }

            if (status.ToLower() == "Cancelled".ToLower())
            {
                if (string.IsNullOrEmpty(cancelledDate))
                {
                    return true;
                }
                else
                {
                    try
                    {
                        var dt = Convert.ToDateTime(cancelledDate);
                        if ((DateTime.Now - dt).Days > 90)
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        return true;
                    }
                }
            }
            return true;
        }
        [Route("ReloadOpportunity")]
        [System.Web.Http.HttpGet]
        public object ReloadOpportunity()
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
            //Opportunity
            var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProducts,
               _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
               _sessionMdfRepository, _sfAccounts, _delegateReport, _delegateRebateItemReport,
               _identity.SessionKey);
            sfClient.ReloadSFOpportunityData(account);


            var data = _sessionOpportunityData.GetAll()
                .Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber && filterOpportunity(x.Status, x.CancelledDate)).Select(m => new
                            {
                                SessionKey = m.SessionKey,
                                AccountId = m.AccountId,
                                PartnerNumber = m.PartnerNumber,
                                AccountName = m.AccountName,
                                OpportunityId = m.OpportunityId,
                                OpportunityName = m.OpportunityName,
                                OpportunityType = m.OpportunityType,
                                TotalOpportunityQuantity = m.TotalOpportunityQuantity,
                                TotalProductQuantity = m.TotalProductQuantity,
                                Amount = m.Amount,
                                StageName = m.StageName,
                                CloseDate = m.CloseDate,
                                IsClosed = m.IsClosed,
                                Description = m.Description,
                                CreatedDate = m.CreatedDate.ConvertToDateTime(),
                                Products = _sessionOpportunityProducts.GetAll().Where(c => c.SessionKey == _identity.SessionKey && c.AccountId == _identity.AccountId && c.PartnerNumber == _identity.PartnerNumber && c.OpportunityId == m.OpportunityId && c.RefId == m.Id).ToList(),
                                ProductOfInterest = string.IsNullOrEmpty(m.ProductInterest) ? new List<string>() : m.ProductInterest.Split(';').ToList(),
                                BusinessCase = m.BusinessCase,
                                Distributor = m.Distributor,
                                ProductQuantity = m.ProductQuantity,

                                Status = m.Status,
                                SupportRequest = m.SupportRequest,
                                StarExpert = m.StarExpert,
                                StarExpertEmail = m.StarExpertEmail,
                                StarUpdate = m.StarUpdate,
                                CancelledDate = m.CancelledDate

                            }).OrderByDescending(m => m.CreatedDate)

                            .ToList();
            if (data != null) return data;
            return new List<SFTempSessionOpportunityData>();
        }


        [Route("GetOpportunitiesCountSessionData")]
        [System.Web.Http.HttpGet]
        public int GetOpportunitiesCountSessionData()
        {
            return _sessionOpportunityData.GetAll().Count(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber && filterOpportunity(x.Status, x.CancelledDate));

        }

        [Route("AddOpportunity")]
        [System.Web.Http.HttpPost]
        public void AddOpportunity(dynamic opp)
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
            var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProducts,
                _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
                _sessionMdfRepository, _sfAccounts, _delegateReport, _delegateRebateItemReport,
                _identity.SessionKey);
            // dynamic res = sfClient.GetAccountDetail(account.AccountId);
            dynamic resOpportunity = sfClient.AddOpportunity(opp, account.AccountId);
            try
            {
                var tsd = new SFTempSessionOpportunityData
                {
                    SessionKey = _identity.SessionKey,
                    AccountId = opp.AccountId,
                    AccountName = account != null ? account.AccountName : "",
                    PartnerNumber = _identity.PartnerNumber,
                    OpportunityId = resOpportunity.id,
                    OpportunityName = opp.Name,
                    OpportunityType = opp.Type,
                    Description = "",
                    TotalOpportunityQuantity = opp.Quantity__c,
                    TotalProductQuantity = 0,
                    Amount = 0,
                    StageName = opp.StageName,
                    CloseDate = opp.CloseDate,
                    CreatedDate = DateTime.Now.ToString(),
                    IsClosed = false,

                    Distributor = opp.Distributor_Mark__c,
                    ProductInterest = opp.Product_s_of_Interest__c,
                    ProductQuantity = opp.Reseller_Contact_Name__c,
                    CompetitiveProducts = opp.Competitive_product_s__c,
                    Software = opp.Softwares__c,
                    RollOutBulkBuy = opp.Roll_out_or_Bulk_buy__c,
                    LaunchDate = opp.TimeFrame_Launch_Date__c,
                    DemoProductRequested = opp.Reseller_Contact_Phone__c,
                    SupportRequest = opp.How_can_we_support_you__c,
                    SpecialPricingRequested = opp.Reseller_Contact_Email__c,
                    EndUser = opp.End_user__c,
                    BusinessCase = opp.Business_case__c,

                };
                var p = tsd.MapTo<OpportunityAdded>(new OpportunityAdded());
                _sessionOpportunityData.Add(tsd);
                _opportunityAdded.Add(p);
            }
            catch (Exception ex)
            {


            }
        }

        [Route("GetConfigValue/{id:int}")]
        [System.Web.Http.HttpGet]
        public string GetConfigValue(string id)
        {
            var cnf = _systemConfig.GetAll().FirstOrDefault(x => x.ConfigKey == id);
            return cnf == null ? string.Empty : cnf.ConfigValue;
        }

        [Route("SetConfigValue")]
        [System.Web.Http.HttpPost]
        public void SetConfigValue(dynamic obj)
        {
            if (obj.ConfigKey != null && obj.ConfigKey != "")
            {
                var cnf = _systemConfig.GetAll().FirstOrDefault(x => x.ConfigKey == Convert.ToString(obj.ConfigKey));
                if (cnf != null)
                {
                    cnf.ConfigValue = Convert.ToString(obj.ConfigValue);
                    _systemConfig.Update(cnf);
                }
                else
                {
                    var newCnf = new SystemConfig
                    {
                        ConfigKey = Convert.ToString(obj.ConfigKey),
                        ConfigValue = Convert.ToString(obj.ConfigValue),
                    };
                    _systemConfig.Add(newCnf);
                }

            }

        }


        [Route("GetPurchaseByDistributorFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetPurchaseByDistributorFromSessionData()
        {
            var obj = new List<dynamic>();
            var data = _sessionPurchaseByDistributors.GetAll()
                .Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber).ToList();

            foreach (var m in data)
            {
                var commonCheck = _sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId);
                var rpt = commonCheck != null ?
                                        string.IsNullOrEmpty(_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).PartnerType) ?
                                            "-" : (_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).PartnerType) : "-";
                var rpn = _sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId && ((x.PartnerProgramStatus == "Requested") || (x.PartnerProgramStatus == "Contract Pending") || (x.PartnerProgramStatus == "Signed"))) != null ?
                                        "In Progress" : string.IsNullOrEmpty(m.ResellerPartnerNumber) ? "Not Registered" : m.ResellerPartnerNumber;
                var rd = rpn == "In Progress" ? "-" : (commonCheck != null ?
                                            string.IsNullOrEmpty(_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).DiscountRate) ?
                                            "-" : (_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).DiscountRate + "%") : "-");
                var sm = rpn == "In Progress" ? "-" : (commonCheck != null ?
                                    string.IsNullOrEmpty(_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).SuggestedMarkup) ?
                                        "-" : (_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).SuggestedMarkup + "%") : "-");
                var dct = _passwordGeneraionLog.GetAll().LastOrDefault(x => x.PartnerNumber == rpn);
                obj.Add(new
                {
                    SessionKey = m.SessionKey,
                    AccountId = m.AccountId,
                    PartnerNumber = m.PartnerNumber,
                    ResellerAccountId = m.ResellerAccountId,
                    ResellerAccountName = m.ResellerAccountName,
                    ResellerPartnerType = rpt,
                    ResellerPartnerNumber = rpn,
                    CustomerName = m.CustomerName,
                    DistributorShortName = m.DistributorShortName,
                    SixMonthsSale = m.SixMonthsSale,
                    TwelveMonthsSale = m.TwelveMonthsSale,
                    TwentyFourMonthsSale = m.TwentyFourMonthsSale,
                    //ResellerDiscount = string.IsNullOrEmpty(m.ResellerDiscount) ? "Not Registered" : m.ResellerDiscount,
                    ResellerDiscount = rd,
                    SuggestedMarkup = sm,
                    DistributorDiscount = rpn == "In Progress" ? "-" : (commonCheck != null ?
                                        string.IsNullOrEmpty(_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).DistributorDiscount) ? "-" :
                                            (_sfAccounts.GetAll().FirstOrDefault(x => x.AccountId == m.ResellerAccountId).DistributorDiscount + "%") : "-"),
                    DateCreated = dct != null ? dct.PasswordGenerationDate.Value.ToShortDateString() : string.Empty
                });
            }

            return obj;
            //return new List<SFTempSessionPurchaseByDistributors>();
        }

        [Route("GetDelegateData")]
        [System.Web.Http.HttpGet]
        public object GetDelegateData()
        {
            //var obj = new List<dynamic>();
            return _delegateReport.GetAll()
                .Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber).ToList()
                            .Select(n => new
                            {
                                AccountId = n.AccountId,
                                PartnerNumber = n.PartnerNumber,
                                AccountName = n.AccountName,
                                TechnologyPartnerEPN = n.TechnologyPartnerEPN,
                                DelegatePartnerEPN = n.DelegatePartnerEPN,
                                TechnologyPartnerAccountName = n.TechnologyPartnerAccountName,
                                DelegatePartnerAccountName = n.DelegatePartnerAccountName

                            }).ToList().Distinct();

            //.DistinctBy(m=> m.DelegatePartnerEPN).di;



            //return obj;
            //return new List<SFTempSessionPurchaseByDistributors>();
        }

        [Route("GetDelegateRebateItemData")]
        [System.Web.Http.HttpGet]
        public object GetDelegateRebateItemData()
        {
            //var obj = new List<dynamic>();
            return _delegateRebateItemReport.GetAll()
                .Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber).ToList()
                            .Select(n => new
                            {
                                AccountId = n.AccountId,
                                PartnerNumber = n.PartnerNumber,
                                AccountName = n.AccountName,
                                TechnologyPartnerEPN = n.TechnologyPartnerEPN,
                                DelegatePartnerEPN = n.DelegatePartnerEPN,
                                TechnologyPartnerAccountName = n.TechnologyPartnerAccountName,
                                DelegatePartnerAccountName = n.DelegatePartnerAccountName,
                                DPN = n.DPN,
                                PartNumber = n.PartNumber,
                                ItemName = n.ItemName,
                                DelegateRebate = n.DelegateRebate,
                                DelegateRelationship = n.DelegateRelationship,
                                SapCardCode = n.SapCardCode
                            }).ToList().Distinct();
        }

        [Route("GetPurchaseByProductClassesFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetPurchaseByProductClassesFromSessionData()
        {
            var prods =
                _productClassToDisplay.GetAll().OrderBy(x => x.OrderOfDisplay).ToList();
            var purchProds = _sessionPurchaseByProductClasses.GetAll()
                .Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber).ToList();
            var result = new List<SFTempSessionPurchaseByProductClasses>();
            foreach (var item in prods)
            {
                var matchedProd =
                    purchProds.FirstOrDefault(
                        x => (x.ProductClass == item.ProductClass) || (x.ProductClass.Contains(item.ProductClass)));
                if (matchedProd == null)
                {
                    result.Add(new SFTempSessionPurchaseByProductClasses
                    {
                        ProductClass = item.ProductClassDisplay,
                        SessionKey = _identity.SessionKey,
                        AccountId = _identity.AccountId,
                        PartnerNumber = _identity.PartnerNumber,
                        AccountName = string.Empty,
                        SixMonthsSale = "$0.00",
                        TwelveMonthsSale = "$0.00",
                        TwentyFourMonthsSale = "$0.00"
                    });
                }
                else
                {
                    matchedProd.ProductClass = item.ProductClassDisplay;
                    result.Add(matchedProd);
                }
            }


            var data = result.Select(m => new
            {
                SessionKey = m.SessionKey,
                AccountId = m.AccountId,
                PartnerNumber = m.PartnerNumber,
                AccountName = m.AccountName,
                ProductClass = m.ProductClass,
                SixMonthsSale = m.SixMonthsSale,
                TwelveMonthsSale = m.TwelveMonthsSale,
                TwentyFourMonthsSale = m.TwentyFourMonthsSale,
                //Growth = GetGrowthPercentage(m.SixMonthsSale,m.TwelveMonthsSale)
                Growth = string.IsNullOrEmpty(m.GrowthRate) ? 0 : Convert.ToDouble(m.GrowthRate)
            })

                            .ToList();
            if (data != null) return data;
            return new List<SFTempSessionPurchaseByDistributors>();
        }

        private double GetGrowthPercentage(string sixMonth, string twelveMonth)
        {
            double sixMonthSale = 0;
            double twelveMonthSale = 0;
            if (!string.IsNullOrEmpty(sixMonth))
            {
                var t = sixMonth.Replace("$", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out sixMonthSale);
            }
            if (!string.IsNullOrEmpty(twelveMonth))
            {
                var t = twelveMonth.Replace("$", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out twelveMonthSale);
            }
            double result = 0;
            if (twelveMonthSale > sixMonthSale)
            {
                if (sixMonthSale == 0)
                {
                    result = 0;
                }
                else
                {
                    result = ((twelveMonthSale - sixMonthSale) / sixMonthSale) * 100;
                }
            }
            if (twelveMonthSale < sixMonthSale)
            {
                if (twelveMonthSale == 0)
                {
                    result = -(sixMonthSale / 100);
                }
                else
                {
                    result = -(((sixMonthSale - twelveMonthSale) / twelveMonthSale) * 100);
                }
            }
            if ((sixMonthSale == 0) && (twelveMonthSale == 0))
            {
                result = 0;
            }
            if (sixMonthSale == twelveMonthSale)
            {
                result = 0;
            }
            return result == 0 ? 0 : Math.Round(result, 2);
        }


        [Route("GetProductPriceList")]
        [System.Web.Http.HttpGet]
        public object GetProductPriceList()
        {
            var dataAc = _sfAccounts.GetAll()
                .Where(
                    x => x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber)
                .FirstOrDefault();

            var data = GetfilteredProductList(dataAc)
                    .Select(m => new
                    {
                        ItemName = string.IsNullOrEmpty(m.ItemName) ? string.Empty : m.ItemName.Trim(),
                        ItemCode = m.ItemCode,
                        ProductClass = m.ProductClass,
                        ProductSubClass = m.ProductSubClass,
                        SalesDescription =
                            string.IsNullOrEmpty(m.SalesDescription) ? "Description not found" : m.SalesDescription,
                        ListPrice = string.IsNullOrEmpty(m.ListPrice) ? "$0" : string.Format("{0:C}", Convert.ToDecimal(m.ListPrice)),
                        MAP = string.IsNullOrEmpty(m.MAP) ? "$0" : string.Format("{0:C}", Convert.ToDecimal(m.MAP)),
                        //ListPrice = m.ListPrice,
                        //MAP = m.MAP,
                        DiscountRate = dataAc != null ? dataAc.DiscountRate : "0%",
                        DiscountedPrice = dataAc != null ? GetDiscountedPrice(dataAc.DiscountRate, m.ListPrice) : 0,
                        ShowDescription = false
                    })
                    .Distinct()
                    .ToList()
                    .OrderBy(x => x.ProductClass)
                    //.OrderByDescending(x => GetItemCode(x.ItemCode))
                    //.ThenBy(m => m.ProductClass)
                    .ToList();
            if (data != null) return data;

            return new List<PortalItemMaster>();
        }

        private List<PortalItemMaster> GetfilteredProductList(SFAccounts ac)
        {
            // var res = _portalItemMaster.GetAll().ToList();;
            var res = new List<PortalItemMaster>();
            if (!string.IsNullOrEmpty(ac.Region))
            {
                if (ac.Region.ToLower() == "USA/Canada".ToLower())
                {
                    res = _portalItemMaster.GetAll().Where(x => !string.IsNullOrEmpty(x.ValidUSCA) && (x.ValidUSCA.ToLower() == "y")).ToList();

                }
                if (ac.Region.ToLower() == "Mexico".ToLower() || ac.Region.ToLower() == "ROLA".ToLower())
                {
                    res = _portalItemMaster.GetAll().Where(x => !string.IsNullOrEmpty(x.ValidLA) && (x.ValidLA.ToLower() == "y")).ToList();

                }
            }
            return res;
        }

        private Int64 GetItemCode(string itemCode)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                return 0;
            }
            Int64 result = 0;
            Int64.TryParse(itemCode, out result);
            return result;
        }

        private double GetDiscountedPrice(string discountRate, string map)
        {
            double calcDiscRate = 0;
            double calcMap = 0;
            if (!string.IsNullOrEmpty(discountRate))
            {
                var t = discountRate.Replace("%", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out calcDiscRate);
            }
            if (calcDiscRate <= 0) return 0;
            if (!string.IsNullOrEmpty(map))
            {
                var t = map.Replace("$", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out calcMap);
            }
            if (calcMap <= 0) return 0;
            double result = 0;

            result = calcMap - ((calcMap * calcDiscRate) / 100);

            return result == 0 ? 0 : Math.Round(result, 2);
        }


        [Route("GetPurchaseByDistributorCountFromSessionData")]
        [System.Web.Http.HttpGet]
        public int GetPurchaseByDistributorCountFromSessionData()
        {
            return _sessionPurchaseByDistributors.GetAll().Count(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber);

        }



        [Route("GetMdfFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetMdfFromSessionData()
        {
            var mdf = _sessionMdfRepository.GetAll().Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber);
            try
            {

                if (mdf != null)
                {
                    var obj = mdf.OrderByDescending(x => ConvertStringToInteger(x.MdfAwardYear)).ThenByDescending(m => ConvertStringToInteger(m.MdfAwardQuarter.Replace("Q", ""))).FirstOrDefault();
                    if (obj != null)
                    {
                        var mdfYear = obj != null ? obj.MdfAwardYear : string.Empty;
                        var mdfQ1 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q1").ToList();
                        var mdfQ2 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q2").ToList();
                        var mdfQ3 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q3").ToList();
                        var mdfQ4 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q4").ToList();

                        var totalMdfAllocated = (mdfQ1 != null
                            ? (mdfQ1.FirstOrDefault() != null
                                ? ConvertSFMoneyToDouble(mdfQ1.FirstOrDefault().MdfAwardDollar)
                                : 0)
                            : 0) +
                                                (mdfQ2 != null
                                                    ? (mdfQ2.FirstOrDefault() != null
                                                        ? ConvertSFMoneyToDouble(mdfQ2.FirstOrDefault().MdfAwardDollar)
                                                        : 0)
                                                    : 0) +
                                                (mdfQ3 != null
                                                    ? (mdfQ3.FirstOrDefault() != null
                                                        ? ConvertSFMoneyToDouble(mdfQ3.FirstOrDefault().MdfAwardDollar)
                                                        : 0)
                                                    : 0) +
                                                (mdfQ4 != null
                                                    ? (mdfQ4.FirstOrDefault() != null
                                                        ? ConvertSFMoneyToDouble(mdfQ4.FirstOrDefault().MdfAwardDollar)
                                                        : 0)
                                                    : 0);

                        var totalMdfAvailable = (mdfQ1 != null
                            ? (mdfQ1.FirstOrDefault() != null
                                ? ConvertSFMoneyToDouble(mdfQ1.FirstOrDefault().MdfAwardBalance)
                                : 0)
                            : 0) +
                                                (mdfQ2 != null
                                                    ? (mdfQ2.FirstOrDefault() != null
                                                        ? ConvertSFMoneyToDouble(mdfQ2.FirstOrDefault().MdfAwardBalance)
                                                        : 0)
                                                    : 0) +
                                                (mdfQ3 != null
                                                    ? (mdfQ3.FirstOrDefault() != null
                                                        ? ConvertSFMoneyToDouble(mdfQ3.FirstOrDefault().MdfAwardBalance)
                                                        : 0)
                                                    : 0) +
                                                (mdfQ4 != null
                                                    ? (mdfQ4.FirstOrDefault() != null
                                                        ? ConvertSFMoneyToDouble(mdfQ4.FirstOrDefault().MdfAwardBalance)
                                                        : 0)
                                                    : 0);

                        var totalMdfUsed = (mdfQ1 != null
                            ? (mdfQ1.FirstOrDefault() != null
                                ? ConvertSFMoneyToDouble(mdfQ1.FirstOrDefault().MdfAwardTotalClaimed)
                                : 0)
                            : 0) +
                                           (mdfQ2 != null
                                               ? (mdfQ2.FirstOrDefault() != null
                                                   ? ConvertSFMoneyToDouble(mdfQ2.FirstOrDefault().MdfAwardTotalClaimed)
                                                   : 0)
                                               : 0) +
                                           (mdfQ3 != null
                                               ? (mdfQ3.FirstOrDefault() != null
                                                   ? ConvertSFMoneyToDouble(mdfQ3.FirstOrDefault().MdfAwardTotalClaimed)
                                                   : 0)
                                               : 0) +
                                           (mdfQ4 != null
                                               ? (mdfQ4.FirstOrDefault() != null
                                                   ? ConvertSFMoneyToDouble(mdfQ4.FirstOrDefault().MdfAwardTotalClaimed)
                                                   : 0)
                                               : 0);

                        var totalMdfPending = (mdfQ1 != null ? mdfQ1.Sum(x => ConvertSFMoneyToDouble(x.MdfTotalAmount)) : 0) +
                                              (mdfQ2 != null ? mdfQ2.Sum(x => ConvertSFMoneyToDouble(x.MdfTotalAmount)) : 0) +
                                              (mdfQ3 != null ? mdfQ3.Sum(x => ConvertSFMoneyToDouble(x.MdfTotalAmount)) : 0) +
                                              (mdfQ4 != null ? mdfQ4.Sum(x => ConvertSFMoneyToDouble(x.MdfTotalAmount)) : 0);
                        return new
                        {
                            MdfPerQuarter = totalMdfAllocated.ToString("C0") + " USD",
                            TotalMdfAvailable = totalMdfAvailable.ToString("C0") + " USD",
                            TotalMdfPending = (totalMdfAllocated - totalMdfPending).ToString("C0") + " USD",
                            TotalMdfUsed = totalMdfUsed.ToString("C0") + " USD",
                            MdfNo = obj.MdfName,
                            MdfId = obj.MdfId,
                            MdfContact = obj.MdfContact,
                            MdfContactEmail = obj.MdfContactEmail,
                            PercentageUsed = GetMdfUsedPercentage(totalMdfAllocated.ToString(), totalMdfUsed.ToString()),
                            MdfLatestQuarter = obj.MdfAwardQuarter
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new SFTempSessionMdfData();
            }
            return new SFTempSessionMdfData();
        }

        private Int32 GetMdfUsedPercentage(string totalMdf, string totalUsedMdf)
        {
            double totalMdfAlloted = 0;
            double totalMdfUsed = 0;
            if (!string.IsNullOrEmpty(totalMdf))
            {
                var t = totalMdf.Replace("$", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out totalMdfAlloted);
            }
            if (!string.IsNullOrEmpty(totalUsedMdf))
            {
                var t = totalUsedMdf.Replace("$", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out totalMdfUsed);
            }
            double result = 0;
            if (totalMdfUsed < totalMdfAlloted)
            {
                if (totalMdfAlloted == 0)
                {
                    result = 0;
                }
                else
                {
                    result = ((totalMdfUsed / totalMdfAlloted) * 100);
                }
            }
            if ((totalMdfUsed == 0) && (totalMdfAlloted == 0))
            {
                result = 0;
            }
            if (totalMdfUsed == totalMdfAlloted)
            {
                result = 100;
            }
            return result == 0 ? 0 : Convert.ToInt32(Math.Floor(result));
        }

        [Route("GetMdfRequestFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetMdfRequestFromSessionData()
        {
            var mdf = _sessionMdfRepository.GetAll().Where(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber).ToList();
            var mdfReq = new List<SFTempSessionMdfData>();
            if (mdf != null)
            {
                var obj = mdf.OrderByDescending(x => ConvertStringToInteger(x.MdfAwardYear)).FirstOrDefault();
                if (obj != null)
                {
                    var mdfYear = obj != null ? obj.MdfAwardYear : string.Empty;
                    var mdfQ1 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q1").ToList();
                    var mdfQ2 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q2").ToList();
                    var mdfQ3 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q3").ToList();
                    var mdfQ4 = mdf.Where(x => x.MdfAwardYear == mdfYear && x.MdfAwardQuarter == "Q4").ToList();

                    var totalMdfAvailable = (mdfQ1 != null
                       ? (mdfQ1.FirstOrDefault() != null
                           ? ConvertSFMoneyToDouble(mdfQ1.FirstOrDefault().MdfAwardBalance)
                           : 0)
                       : 0) +
                                           (mdfQ2 != null
                                               ? (mdfQ2.FirstOrDefault() != null
                                                   ? ConvertSFMoneyToDouble(mdfQ2.FirstOrDefault().MdfAwardBalance)
                                                   : 0)
                                               : 0) +
                                           (mdfQ3 != null
                                               ? (mdfQ3.FirstOrDefault() != null
                                                   ? ConvertSFMoneyToDouble(mdfQ3.FirstOrDefault().MdfAwardBalance)
                                                   : 0)
                                               : 0) +
                                           (mdfQ4 != null
                                               ? (mdfQ4.FirstOrDefault() != null
                                                   ? ConvertSFMoneyToDouble(mdfQ4.FirstOrDefault().MdfAwardBalance)
                                                   : 0)
                                               : 0);
                    mdfReq.AddRange(mdfQ1); mdfReq.AddRange(mdfQ2); mdfReq.AddRange(mdfQ3); mdfReq.AddRange(mdfQ4);
                    var mdfRes = new
                    {
                        MdfYear = mdfYear,
                        MdfContact = obj.MdfContact,
                        MdfContactEmail = obj.MdfContactEmail,
                        TotalMdfAvailable = totalMdfAvailable.ToString("C0"),
                        MDFQuarterOne = mdfQ1.FirstOrDefault() != null ? Math.Round(ConvertSFMoneyToDouble(mdfQ1.FirstOrDefault().MdfAwardDollar)).ToString("C0") : string.Empty,
                        MDFQuarterTwo = mdfQ2.FirstOrDefault() != null ? Math.Round(ConvertSFMoneyToDouble(mdfQ2.FirstOrDefault().MdfAwardDollar)).ToString("C0") : string.Empty,
                        MDFQuarterThree = mdfQ3.FirstOrDefault() != null ? Math.Round(ConvertSFMoneyToDouble(mdfQ3.FirstOrDefault().MdfAwardDollar)).ToString("C0") : string.Empty,
                        MDFQuarterFour = mdfQ4.FirstOrDefault() != null ? Math.Round(ConvertSFMoneyToDouble(mdfQ4.FirstOrDefault().MdfAwardDollar)).ToString("C0") : string.Empty,
                    };
                    return new
                    {
                        Mdf = mdfRes,
                        MdfRequests = mdfReq.Select(x => new
                        {
                            Amount = ConvertSFMoneyToDouble(x.MdfTotalAmount).ToString("C0"),
                            ActivityDescription = x.MdfName,
                            ProjectStatus = string.Empty,
                            PaymentStatus = x.MdfPaymentStatus,
                            ShowDescription = false
                        }),
                        TotalMdfRequestAmount =
                            mdfReq.Select(x => ConvertSFMoneyToDouble(x.MdfTotalAmount)).Sum().ToString("C0")
                    };
                }
            }
            return new
            {
                Mdf = new SFTempSessionMdfData(),
                MdfRequests = new List<SFTempSessionMdfData>(),
                TotalMdfRequestAmount = 0
            }; ;
        }

        private double ConvertSFMoneyToDouble(string value)
        {
            double resValue = 0;
            if (!string.IsNullOrEmpty(value))
            {
                var t = value.Replace("$", "").Replace(",", "").Replace(" ", "");
                Double.TryParse(t, out resValue);
            }

            return resValue == 0 ? 0 : Math.Round(resValue, 2);
        }

        private double ConvertStringToInteger(string value)
        {
            int resValue = 0;
            if (!string.IsNullOrEmpty(value))
            {
                Int32.TryParse(value, out resValue);
            }

            return resValue == 0 ? 0 : resValue;
        }

        [Route("AddMdfRequest")]
        [System.Web.Http.HttpPost]
        public void AddMdfRequest(dynamic opp)
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
            var mdf = _sessionMdfRepository.GetAll().FirstOrDefault(
                    x => x.SessionKey == _identity.SessionKey && x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber);
            //var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProducts,
            //    _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
            //    _sessionMdfRepository, 
            //    _identity.SessionKey);

            //opp.Account_Name__c = _identity.AccountId;
            //opp.Payment_Status__c = "Requested";
            //dynamic res = sfClient.AddMdfRequest(opp);

            //Send Email
            var contactEmail = (string)opp.ContactEmail;
            if (!string.IsNullOrEmpty(contactEmail))
            {
                new Thread(() =>
                {
                    try
                    {
                        Thread.CurrentThread.IsBackground = true;
                        Thread.Sleep(0);

                        var subject = "MDF request from " + account.AccountName;

                        var sb = new StringBuilder();
                        sb.Append(string.Format("<b>Dear {0},</b><br><br>", opp.Contact));
                        sb.Append(string.Format("Following MDF request has been added by {0}. <br><br>", account.PartnerNumber));
                        sb.Append("MDF request Detail. <br><br>");
                        sb.Append(string.Format("Description : {0}. <br><br>", opp.Market_Activity_Description__c));
                        sb.Append(string.Format("Amount : {0}. <br><br>", opp.Total_Amount__c));

                        sb.Append("<b>Thanks</b><br>");
                        _emailService.SendEmail(sb.ToString(), contactEmail, subject);

                        var tsd = new MdfAdded()
                        {
                            SessionKey = _identity.SessionKey,
                            AccountId = _identity.AccountId,
                            AccountName = account != null ? account.AccountName : "",
                            PartnerNumber = _identity.PartnerNumber,
                            MdfContact = mdf != null ? mdf.MdfContact : string.Empty,
                            MdfContactEmail = mdf != null ? mdf.MdfContactEmail : string.Empty,
                            MdfNo = mdf != null ? mdf.MdfName : string.Empty,
                            MdfPerQuarter = opp.Quarter__c,
                            //TotalMdfAvailable = mdf != null ? mdf.TotalMdfAvailable : string.Empty,
                            //TotalMdfPending = mdf != null ? mdf.TotalMdfPending : string.Empty,
                            //TotalMdfUsed = mdf != null ? mdf.TotalMdfUsed : string.Empty,
                            CreatedDate = DateTime.Now.ToString(),
                            MdfId = opp.MDF_Award__c,
                            Amount = opp.Total_Amount__c,
                            ActivityDescription = opp.Market_Activity_Description__c,
                            ProjectStatus = "Not Started",
                            PaymentStatus = "Not Paid"
                        };

                        _mdfAdded.Add(tsd);
                    }
                    catch (Exception ex)
                    {

                    }
                }).Start();
            }
            //--------------------------------------------------------
        }

        [Route("GetAccountByPartnerNumber")]
        [System.Web.Http.HttpGet]
        public object GetAccountByPartnerNumber(string id)
        {
            return _sfAccounts.GetAll().Where(x => x.PartnerNumber == id).FirstOrDefault();
        }

        [Route("GetMenuList")]
        [System.Web.Http.HttpGet]
        public dynamic GetMenuListForPermission()
        {
            return
                _menu.GetAll().Where(m => m.IsActive).OrderBy(m => m.SortOrder)
                    .ToList()
                    .Select(
                        x =>
                            new
                            {
                                Id = x.Id,
                                Title = x.Title,
                                DisplayTitle = x.ParentId.HasValue ? GetMenuTitle(x.Id) : x.Title,
                                AllowedPartnerTypes = x.AllowedPartnerTypes,
                                PartnerTypes = GetPartnerTypesForMenuPermission(x.Id),
                                Expanded = false
                            });
        }

        [Route("SaveMenuPermission")]
        [System.Web.Http.HttpPost]
        public void SaveMenuPermission(dynamic menu)
        {
            if (menu.Id != null && menu.Id > 0)
            {
                var uMenu = _menu.GetById((int)menu.Id);
                uMenu.AllowedPartnerTypes = menu.AllowedPartnerTypes;
                _menu.Update(uMenu);
            }
        }

        private string GetMenuTitle(int id)
        {
            var title = string.Empty;
            var menu = _menu.GetById(id);
            title = menu.ParentId.HasValue ? GetMenuTitle(menu.ParentId.Value) : menu.Title + title;
            return title;
        }
        private dynamic GetPartnerTypesForMenuPermission(int id)
        {
            var menu = _menu.GetById(id);
            return
                _sfAccounts.GetAll().Where(m => !string.IsNullOrEmpty(m.PartnerType)).Select(v => v.PartnerType).ToList().Distinct().
                Select(x => new { PartnerType = x, IsAssigned = string.IsNullOrEmpty(menu.AllowedPartnerTypes) ? false : menu.AllowedPartnerTypes.IndexOf(x) >= 0 }).ToList();
        }

        [Route("GetMenuList")]
        [System.Web.Http.HttpGet]
        public dynamic GetPageSectionListForPermission()
        {
            return
                _pages.GetAll().Where(m => (m.Status.HasValue ? m.Status.Value : true) && m.PageType == (int)PageTypes.PageSections).OrderBy(m => m.PageId)
                    .ToList()
                    .Select(
                        x =>
                            new
                            {
                                Id = x.Id,
                                Title = x.Title,
                                DisplayTitle = x.PageId.HasValue ? (_pages.GetById(x.PageId.Value).Title + "-" + x.Title) : x.Title,
                                AllowedPartnerTypes = x.AllowedPartnerTypes,
                                PartnerTypes = GetPartnerTypesForPageSectionPermission(x.Id),
                                Expanded = false
                            });
        }

        private dynamic GetPartnerTypesForPageSectionPermission(int id)
        {
            var page = _pages.GetById(id);
            return
                _sfAccounts.GetAll().Where(m => !string.IsNullOrEmpty(m.PartnerType)).Select(v => v.PartnerType).ToList().Distinct().
                Select(x => new { PartnerType = x, IsAssigned = string.IsNullOrEmpty(page.AllowedPartnerTypes) ? false : page.AllowedPartnerTypes.IndexOf(x) >= 0 }).ToList();
        }

        [Route("SavePageSectionPermission")]
        [System.Web.Http.HttpPost]
        public void SavePageSectionPermission(dynamic page)
        {
            if (page.Id != null && page.Id > 0)
            {
                var uPage = _pages.GetById((int)page.Id);
                uPage.AllowedPartnerTypes = page.AllowedPartnerTypes;
                _pages.Update(uPage);
            }
        }

        #region Rebate Request


        [Route("GetDistributorsForAccount")]
        [System.Web.Http.HttpGet]
        public object GetDistributorsForAccount()
        {
            var acc = _sfAccounts.GetAll()
               .FirstOrDefault(
                   x => x.AccountId == _identity.AccountId &&
                           x.PartnerNumber == _identity.PartnerNumber);
            if (acc != null)
            {
                return _sfAccounts.GetAll().Where(x => x.Region == acc.Region && x.PartnerType == "Distributor").ToList();
            }
            return null;
        }

        [Route("AddMdfRequest")]
        [System.Web.Http.HttpPost]
        public long AddRebateRequest(dynamic data)
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);

            var rebateRequest = new RebateRequest
            {
                DPN = data.DPN,
                RequestDate = data.RequestDate != null ? data.RequestDate : DateTime.Now,
                EmpowerPartnerNumber = data.EmpowerPartnerNumber,
                PurchasedOnBehalfOf = data.PurchasedOnBehalfOf,
                StarVendorNumber = data.StarVendorNumber,
                TotalAmount = data.TotalAmount != null ? data.TotalAmount : 0,
                Notes = data.Notes,
                AccountId = account.AccountId,
                PartnerNumber = account.PartnerNumber,
                AccountName = account.AccountName
            };
            _rebateRequest.Add(rebateRequest);
            try
            {
                foreach (var item in data.RebateRequestDetail)
                {
                    if (item.PartNumber != null && item.PartNumber != "" && item.Quantity != null && item.Quantity != "")
                    {
                        var rebateRequestDetail = new RebateRequestDetail
                        {
                            RequestId = rebateRequest.Id,
                            PartNumber = item.PartNumber,
                            ItemName = item.ItemName,
                            DelegateRebate = item.DelegateRebate,
                            Quantity = (double)item.Quantity,
                            Distributor = item.Distributor
                        };
                        _rebateRequestDetail.Add(rebateRequestDetail);
                    }
                }

                var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/RebateRequestFiles/");
                sPath = sPath + account.PartnerNumber + "/";
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
                sPath = sPath + rebateRequest.Id.ToString() + "/";
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }

                foreach (var item in data.RebateRequestFiles)
                {
                    var rebateRequestFiles = new RebateRequestFiles
                    {
                        RequestId = rebateRequest.Id,
                        FileName = item,
                        FilePath = sPath + item
                    };
                    _rebateRequestFiles.Add(rebateRequestFiles);
                }

                return rebateRequest.Id;
            }
            catch (Exception ex)
            {
                if (rebateRequest.Id > 0)
                {
                    _rebateRequestFiles.Delete(x => x.RequestId == rebateRequest.Id);
                    _rebateRequestDetail.Delete(x => x.RequestId == rebateRequest.Id);
                    _rebateRequest.Delete(x => x.Id == rebateRequest.Id);
                }
                return 0;
            }
            //--------------------------------------------------------
        }

        /// <summary>
        ///     Upload Files
        /// </summary>
        [Route("UploadRebateRequestFiles")]
        [System.Web.Http.HttpPost]
        public string UploadRebateRequestFiles()
        {
            var rid = System.Web.HttpContext.Current.Request.Form["RequestId"];
            rid = rid.Contains(",") ? rid.Substring(rid.LastIndexOf(",") + 1, rid.Length - (rid.LastIndexOf(",") + 1)) : rid;
            var requestId = Int64.Parse(rid);
            try
            {
                var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";

                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/RebateRequestFiles/" + account.PartnerNumber + "/" + requestId.ToString() + "/");

                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                var rebateRequest = _rebateRequest.GetById(requestId);
                var filesNotSaved = new List<string>();

                // CHECK THE FILE COUNT.
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        var extension = hpf.FileName.Substring(hpf.FileName.LastIndexOf('.'), hpf.FileName.Length - hpf.FileName.LastIndexOf('.'));
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        }
                        else
                        {
                            File.Delete(sPath + Path.GetFileName(hpf.FileName));
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        }
                    }
                }


                //Send email
                var contactEmail = ConfigurationManager.AppSettings["RebateRequestEmailRecipient"].ToString();
                if (!string.IsNullOrEmpty(contactEmail))
                {
                    new Thread(() =>
                    {
                        try
                        {
                            Thread.CurrentThread.IsBackground = true;
                            Thread.Sleep(0);

                            var subject = "Rebate Request from " + account.AccountName + "-" + rebateRequest.DPN;

                            var sb = new StringBuilder();
                            sb.Append(string.Format("<b>Hi ,</b><br><br>"));
                            sb.Append(string.Format("Following Rebate request has been added by {0} for DPN {1}. <br><br>", account.PartnerNumber, rebateRequest.DPN));
                            sb.Append("Rebate request Detail. <br><br>");
                            sb.Append(string.Format("DPN : {0}. <br><br>", rebateRequest.DPN));
                            sb.Append(string.Format("Request Date : {0}. <br><br>", rebateRequest.RequestDate.HasValue ? rebateRequest.RequestDate.Value.ToShortDateString() : ""));
                            sb.Append(string.Format("Empower Partner Number : {0}. <br><br>", rebateRequest.EmpowerPartnerNumber));
                            sb.Append(string.Format("Purchased on behalf of : {0}. <br><br>", rebateRequest.PurchasedOnBehalfOf));
                            sb.Append(string.Format("Star vendor Number : {0}. <br><br>", rebateRequest.StarVendorNumber));
                            sb.Append(string.Format("Total Amount : {0}. <br><br>", rebateRequest.TotalAmount.ToString()));
                            sb.Append(string.Format("Notes : {0}. <br><br>", rebateRequest.Notes));

                            sb.Append(string.Format("<table style='width:700px;'>"));
                            sb.Append(string.Format("<tr style='height:40px;'>"));
                            sb.Append(string.Format("<td style='width:140px;'>Part Number"));
                            sb.Append(string.Format("</td>"));
                            sb.Append(string.Format("<td style='width:140px;'>Item Name"));
                            sb.Append(string.Format("</td>"));
                            sb.Append(string.Format("<td style='width:140px;'>Delegate Rebate"));
                            sb.Append(string.Format("</td>"));
                            sb.Append(string.Format("<td style='width:140px;'>Quantity"));
                            sb.Append(string.Format("</td>"));
                            sb.Append(string.Format("<td style='width:140px;'>Distributor"));
                            sb.Append(string.Format("</td>"));
                            sb.Append(string.Format("</tr>"));

                            var rd = _rebateRequestDetail.GetAll().Where(x => x.RequestId == rebateRequest.Id).ToList();

                            foreach (var item in rd)
                            {
                                sb.Append(string.Format("<tr style='height:40px;'>"));
                                sb.Append(string.Format("<td style='width:140px;'>{0}", item.PartNumber));
                                sb.Append(string.Format("</td>"));
                                sb.Append(string.Format("<td style='width:140px;'>{0}", item.ItemName));
                                sb.Append(string.Format("</td>"));
                                sb.Append(string.Format("<td style='width:140px;'>{0}", item.DelegateRebate));
                                sb.Append(string.Format("</td>"));
                                sb.Append(string.Format("<td style='width:140px;'>{0}", item.Quantity));
                                sb.Append(string.Format("</td>"));
                                sb.Append(string.Format("<td style='width:140px;'>{0}", item.Distributor));
                                sb.Append(string.Format("</td>"));
                                sb.Append(string.Format("</tr>"));
                            }


                            sb.Append(string.Format("</table>"));

                            var rf = _rebateRequestFiles.GetAll().Where(x => x.RequestId == rebateRequest.Id).ToList();

                            var attachments = new List<string>();
                            foreach (var item in rf)
                            {
                                attachments.Add(item.FilePath);

                            }
                            //sb.Append(string.Format("Amount : {0}. <br><br>", opp.Total_Amount__c));

                            sb.Append("<b>Thanks</b><br>");
                            _emailService.SendEmail(sb.ToString(), contactEmail, subject, attachments);


                        }
                        catch (Exception ex)
                        {

                        }
                    }).Start();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                _rebateRequestFiles.Delete(x => x.RequestId == requestId);
                _rebateRequestDetail.Delete(x => x.RequestId == requestId);
                _rebateRequest.Delete(x => x.Id == requestId);
                return ex.ToString();
            }
        }

        [Route("GetRecentRequestsFromSessionData")]
        [System.Web.Http.HttpGet]
        public object GetRecentRequestsFromSessionData()
        {
            var data = _rebateRequest.GetAll()
                .Where(x =>
                    x.AccountId == _identity.AccountId &&
                            x.PartnerNumber == _identity.PartnerNumber && filterRecentRequest(x.RequestDate.Value.ToShortDateString())).OrderByDescending(m => m.RequestDate).ToList();
            if (data != null)
            {
                dynamic retData = new List<dynamic>();
                //retData.RebateRequests = data;

                foreach (var item in data)
                {
                    dynamic rd = new ExpandoObject();
                    rd.Id = item.Id;
                    rd.DPN = item.DPN;
                    rd.RequestDate = item.RequestDate;
                    rd.EmpowerPartnerNumber = item.EmpowerPartnerNumber;
                    rd.PurchasedOnBehalfOf = item.PurchasedOnBehalfOf;
                    rd.StarVendorNumber = item.StarVendorNumber;
                    rd.TotalAmount = item.TotalAmount;
                    rd.Notes = item.Notes;
                    rd.AccountId = item.AccountId;
                    rd.PartnerNumber = item.PartnerNumber;
                    rd.AccountName = item.AccountName;

                    rd.RebateRequestDetail = _rebateRequestDetail.GetAll().Where(x => x.RequestId == item.Id).ToList();
                    rd.RebateRequestFiles = _rebateRequestFiles.GetAll().Where(x => x.RequestId == item.Id).ToList();

                    retData.Add(rd);
                }

                return retData;
            };
            return new List<RebateRequest>();
        }

        private bool filterRecentRequest(string requestDate)
        {

            if (string.IsNullOrEmpty(requestDate))
            {
                return true;
            }

            if (string.IsNullOrEmpty(requestDate))
            {
                return true;
            }
            else
            {
                try
                {
                    var dt = Convert.ToDateTime(requestDate);
                    if ((DateTime.Now.Month - dt.Month) > 4)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
            return true;
        }
        #endregion

        #region File Type
        [System.Web.Http.HttpGet]
        public object GetFileTypes()
        {
            return _fileTypeConfig.GetAll().ToList();
        }

        [System.Web.Http.HttpPost]
        public object SaveFileType(dynamic fileType)
        {
            if (fileType.Id != null && fileType.Id > 0)
            {
                var newFileType = _fileTypeConfig.GetById((int)fileType.Id);
                newFileType.FileType = fileType.FileType;
                newFileType.Description = fileType.Description;
                newFileType.Extension = fileType.Extension;
                newFileType.Thumbnail = fileType.Thumbnail;

                _fileTypeConfig.Update(newFileType);
            }
            else
            {
                var newFileType = new FileTypeConfig();
                newFileType.FileType = fileType.FileType;
                newFileType.Description = fileType.Description;
                newFileType.Extension = fileType.Extension;
                newFileType.Thumbnail = fileType.Thumbnail;


                _fileTypeConfig.Add(newFileType);
                fileType.Id = newFileType.Id;

            }
            return fileType;
        }

        [Route("DeleteFileType")]
        [System.Web.Http.HttpPost]
        public void DeleteFileType(dynamic fileType)
        {
            var id = (int)fileType.Id;

            _fileTypeConfig.Delete(x => x.Id == id);
        }
        #endregion

        #region Library Category
        [System.Web.Http.HttpGet]
        public object GetLibraryCategories()
        {
            return
              _libraryCategory.GetAll().Where(m => !m.IsDeleted).OrderBy(x => x.LibraryType).ThenBy(m => m.SortOrder)
                  .ToList()
                  .Select(
                      x =>
                          new
                          {
                              Id = x.Id,
                              LibraryType = x.LibraryType,
                              CategoryName = x.CategoryName,
                              Description = x.Description,
                              CategoryImage = x.CategoryImage,
                              SortOrder = x.SortOrder,
                              AllowedPartnerTypes = x.AllowedPartnerTypes,
                              PartnerTypes = GetPartnerTypesForLibraryCategoryPermission(x.Id),
                              Expanded = false,
                          });

        }

        private dynamic GetPartnerTypesForLibraryCategoryPermission(int id)
        {
            var cat = _libraryCategory.GetById(id);
            return
                _sfAccounts.GetAll().Where(m => !string.IsNullOrEmpty(m.PartnerType)).Select(v => v.PartnerType).ToList().Distinct().
                Select(x => new { PartnerType = x, IsAssigned = string.IsNullOrEmpty(cat.AllowedPartnerTypes) ? false : cat.AllowedPartnerTypes.IndexOf(x) >= 0 }).ToList();
        }

        [System.Web.Http.HttpGet]
        public dynamic GetPartnerTypesForPermission()
        {
            return
                _sfAccounts.GetAll().Where(m => !string.IsNullOrEmpty(m.PartnerType)).Select(v => v.PartnerType).ToList().Distinct().
                Select(x => new { PartnerType = x, IsAssigned = false }).ToList();
        }

        [System.Web.Http.HttpPost]
        public object SaveLibraryCategory(dynamic cat)
        {
            if (cat.Id != null && cat.Id > 0)
            {
                var newCat = _libraryCategory.GetById((int)cat.Id);
                newCat.LibraryType = cat.LibraryType;
                newCat.CategoryName = cat.CategoryName;
                newCat.CategoryImage = cat.CategoryImage;
                newCat.Description = cat.Description;
                newCat.AllowedPartnerTypes = cat.AllowedPartnerTypes;
                newCat.SortOrder = cat.SortOrder;
                newCat.IsDeleted = false;

                _libraryCategory.Update(newCat);
            }
            else
            {
                var newCat = new LibraryCategory();
                newCat.LibraryType = cat.LibraryType;
                newCat.CategoryName = cat.CategoryName;
                newCat.CategoryImage = cat.CategoryImage;
                newCat.Description = cat.Description;
                newCat.AllowedPartnerTypes = cat.AllowedPartnerTypes;
                newCat.SortOrder = cat.SortOrder;
                newCat.IsDeleted = false;

                _libraryCategory.Add(newCat);
                cat.Id = newCat.Id;

            }
            return cat;
        }

        [Route("DeleteFileType")]
        [System.Web.Http.HttpPost]
        public void DeleteLibraryCategory(dynamic cat)
        {
            var category = _libraryCategory.GetById((int)cat.Id);
            category.IsDeleted = true;

            _libraryCategory.Update(category);
        }
        #endregion

        #region Library Files
        [System.Web.Http.HttpGet]
        public object GetLibraryFiles()
        {
            return
              _libraryFiles.GetAll().Where(m => !m.IsDeleted).OrderBy(x => x.LibraryType).ThenBy(y => y.LibraryCategoryId).ThenBy(m => m.SortOrder)
                  .ToList()
                  .Select(
                      x =>
                          new
                          {
                              Id = x.Id,
                              LibraryType = x.LibraryType,
                              LibraryCategoryId = x.LibraryCategoryId.ToString(),
                              CategoryName = _libraryCategory.GetById(x.LibraryCategoryId).CategoryName,
                              FileTypeId = x.FileTypeId.HasValue ? x.FileTypeId.Value.ToString() : "",
                              FileType = (x.FileTypeId.HasValue && x.FileTypeId.Value > 0) ? _fileTypeConfig.GetById(x.FileTypeId.Value).FileType : string.Empty,
                              Title = x.Title,
                              TitleSpanish = x.TitleSpanish,
                              Description = x.Description,
                              DescriptionSpanish = x.DescriptionSpanish,
                              FilePath = x.FilePath,
                              FileThumbnailPath = x.FileThumbnailPath,
                              SortOrder = x.SortOrder.ToString(),
                              AllowedPartnerTypes = x.AllowedPartnerTypes,
                              PartnerTypes = GetPartnerTypesForLibraryFilesPermission(x.Id),
                              Expanded = false,
                          });

        }

        private dynamic GetPartnerTypesForLibraryFilesPermission(int id)
        {
            var cat = _libraryFiles.GetById(id);
            return
                _sfAccounts.GetAll().Where(m => !string.IsNullOrEmpty(m.PartnerType)).Select(v => v.PartnerType).ToList().Distinct().
                Select(x => new { PartnerType = x, IsAssigned = string.IsNullOrEmpty(cat.AllowedPartnerTypes) ? false : cat.AllowedPartnerTypes.IndexOf(x) >= 0 }).ToList();
        }

        [System.Web.Http.HttpPost]
        public object SaveLibraryFiles(dynamic cat)
        {
            if (cat.Id != null && cat.Id > 0)
            {
                var newCat = _libraryFiles.GetById((int)cat.Id);
                newCat.LibraryType = cat.LibraryType;
                newCat.LibraryCategoryId = cat.LibraryCategoryId;
                newCat.FileTypeId = cat.FileTypeId;
                newCat.Title = cat.Title;
                newCat.TitleSpanish = cat.TitleSpanish;
                newCat.Description = cat.Description;
                newCat.DescriptionSpanish = cat.DescriptionSpanish;
                newCat.FilePath = cat.FilePath;
                newCat.FileThumbnailPath = cat.FileThumbnailPath;
                newCat.AllowedPartnerTypes = cat.AllowedPartnerTypes;
                newCat.SortOrder = cat.SortOrder;
                newCat.IsDeleted = false;

                _libraryFiles.Update(newCat);
            }
            else
            {
                var newCat = new LibraryFiles();
                newCat.LibraryType = cat.LibraryType;
                newCat.LibraryCategoryId = cat.LibraryCategoryId;
                newCat.FileTypeId = cat.FileTypeId;
                newCat.Title = cat.Title;
                newCat.TitleSpanish = cat.TitleSpanish;
                newCat.Description = cat.Description;
                newCat.DescriptionSpanish = cat.DescriptionSpanish;
                newCat.FilePath = cat.FilePath;
                newCat.FileThumbnailPath = cat.FileThumbnailPath;
                newCat.AllowedPartnerTypes = cat.AllowedPartnerTypes;
                newCat.SortOrder = cat.SortOrder;
                newCat.IsDeleted = false;

                _libraryFiles.Add(newCat);
                cat.Id = newCat.Id;

            }
            return cat;
        }

        [Route("DeleteLibraryiles")]
        [System.Web.Http.HttpPost]
        public void DeleteLibraryiles(dynamic cat)
        {
            var file = _libraryFiles.GetById((int)cat.Id);
            file.IsDeleted = true;

            _libraryFiles.Update(file);
        }

        [System.Web.Http.HttpGet]
        public object GetLibraryFilesForSupport()
        {
            return
                _libraryCategory.GetAll()
                    .Where(x => x.LibraryType.ToLower() == "support" && !x.IsDeleted && (!string.IsNullOrEmpty(x.AllowedPartnerTypes) && x.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0))
                    .OrderBy(m => m.SortOrder)
                    .Select(k => new
                    {
                        CategoryId = k.Id,
                        CategoryName = k.CategoryName,
                        Description = k.Description,
                        LibraryFiles = _libraryFiles.GetAll().Where(a => !a.IsDeleted && a.LibraryType == "support" && a.LibraryCategoryId == k.Id && (!string.IsNullOrEmpty(a.AllowedPartnerTypes) && a.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0))
                              .OrderBy(n => n.SortOrder)
                              .ToList()
                              .Select(
                                  p =>
                                      new
                                      {
                                          Id = p.Id,
                                          FileTypeId = p.FileTypeId.HasValue ? p.FileTypeId.Value.ToString() : "",
                                          FileType = (p.FileTypeId.HasValue && p.FileTypeId.Value > 0) ? _fileTypeConfig.GetById(p.FileTypeId.Value).FileType : string.Empty,
                                          Title = p.Title,
                                          TitleSpanish = p.TitleSpanish,
                                          Description = p.Description,
                                          DescriptionSpanish = p.DescriptionSpanish,
                                          FilePath = string.IsNullOrEmpty(p.FilePath) ? string.Empty : p.FilePath,
                                          FileThumbnailPath = GetLibraryFileThumbnail(p),
                                          SortOrder = p.SortOrder.ToString(),
                                      }).ToList()

                    });

        }

        [System.Web.Http.HttpGet]
        public object GetLibraryFilesForMarketing()
        {
            //return
            //    _libraryCategory.GetAll()
            //        .Where(x => x.LibraryType.ToLower() == "marketing" && !x.IsDeleted && (!string.IsNullOrEmpty(x.AllowedPartnerTypes) && x.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0))
            //        .OrderBy(m => m.SortOrder)
            //        .Select(k => new
            //        {
            //            CategoryId = k.Id,
            //            CategoryName = k.CategoryName,
            //            Description = k.Description,
            //            ShowMore = false,
            //            LibraryFiles = _libraryFiles.GetAll().Where(a => !a.IsDeleted && a.LibraryType == "marketing" && a.LibraryCategoryId == k.Id && (!string.IsNullOrEmpty(a.AllowedPartnerTypes) && a.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0))
            //                  .OrderBy(n => n.SortOrder)
            //                  .ToList()
            //                  .Select(
            //                      p =>
            //                          new
            //                          {
            //                              Id = p.Id,
            //                              FileTypeId = p.FileTypeId.HasValue ? p.FileTypeId.Value.ToString() : "",
            //                              FileType = (p.FileTypeId.HasValue && p.FileTypeId.Value > 0) ? _fileTypeConfig.GetById(p.FileTypeId.Value).FileType : string.Empty,
            //                              Title = p.Title,
            //                              TitleSpanish = p.TitleSpanish,
            //                              Description = p.Description,
            //                              DescriptionSpanish = p.DescriptionSpanish,
            //                              FilePath = string.IsNullOrEmpty(p.FilePath) ? string.Empty : p.FilePath,
            //                              FileThumbnailPath = GetLibraryFileThumbnail(p),
            //                              SortOrder = p.SortOrder.ToString(),
            //                          }).ToList()

            //        });
            //Battle cards
            return _libraryFiles.GetAll().Where(a => !a.IsDeleted && a.LibraryType == "marketing" && a.LibraryCategoryId == 13 && (!string.IsNullOrEmpty(a.AllowedPartnerTypes)
            && a.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0))
                              .OrderBy(n => n.SortOrder)
                              .ToList()
                              .Select(
                                  p =>
                                      new
                                      {
                                          Id = p.Id,
                                          FileTypeId = p.FileTypeId.HasValue ? p.FileTypeId.Value.ToString() : "",
                                          FileType = (p.FileTypeId.HasValue && p.FileTypeId.Value > 0) ? _fileTypeConfig.GetById(p.FileTypeId.Value).FileType : string.Empty,
                                          Title = p.Title,
                                          TitleSpanish = p.TitleSpanish,
                                          Description = p.Description,
                                          DescriptionSpanish = p.DescriptionSpanish,
                                          FilePath = string.IsNullOrEmpty(p.FilePath) ? string.Empty : p.FilePath,
                                          FileThumbnailPath = GetLibraryFileThumbnail(p),
                                          SortOrder = p.SortOrder.ToString(),
                                      }).ToList();
        }


        [System.Web.Http.HttpGet]
        public object GetImportedMarketingMaterial()
        {
            var marketingMaterials = _importedMarketingMaterial.GetAll().Select(
                x => new
                {
                    ID = x.Id,
                    Name = x.Name,
                    ProductTagline = x.ProductTagline,
                    PermALink = string.IsNullOrEmpty(x.PermALink) ? "#" : x.PermALink,
                    Description = x.Description,
                    CatalogVisibility = x.CatalogVisibility,
                    Price = x.Price,
                    DataSheetUrl = string.IsNullOrEmpty(x.DataSheetUrl) ? "#" : x.DataSheetUrl,
                    ActualDataSheetUrl = string.IsNullOrEmpty(x.ActualDataSheetUrl) ? "#" : x.ActualDataSheetUrl,
                    DataSheetDownloadLink = string.IsNullOrEmpty(x.DataSheetDownloadLink) ? "#" : x.DataSheetDownloadLink,
                    VideoUrl = string.IsNullOrEmpty(x.VideoUrl) ? "#" : x.VideoUrl,
                    ImageDownloadLink = string.IsNullOrEmpty(x.ImageDownloadLink) ? "#" : x.ImageDownloadLink,
                    DownloadAllLink = string.IsNullOrEmpty(x.DownloadAllLink) ? "#" : x.DownloadAllLink,
                    Images = _importedMarketingMaterialImages.GetAll().Where(m => m.MarketingMaterialID == x.Id).OrderByDescending(l => l.ModifiedDate).ToList(),
                    Categories = _importedMarketingMaterialCategories.GetAll().Where(n => n.MarketingMaterialID == x.Id).ToList()
                }).ToList();
            return marketingMaterials;
        }

        [System.Web.Http.HttpGet]
        public object GetImportedResources()
        {
            var resources = _importedResource.GetAll().Select(
                x => new
                {
                    ID = x.Id,
                    ImportID = x.ImportID,
                    Title = HttpUtility.HtmlDecode(x.Title),
                    Description = HttpUtility.HtmlDecode(x.Description.Replace("<p>", "").Replace("</p>", "")),
                    Link = x.Link,
                    ResourceType = x.ResourceType,
                    PhotoId = x.PhotoId,
                    PhotoLargeSrc = x.PhotoLargeSrc,
                    PhotoMediumSrc = x.PhotoMediumSrc,
                    Status = x.Status,
                    PhotoThumbnailSrc = x.PhotoThumbnailSrc,
                    ParentID = x.ParentID,
                });
            return resources;
        }

        private string GetLibraryFileThumbnail(LibraryFiles lb)
        {
            if (!string.IsNullOrEmpty(lb.FileThumbnailPath))
            {
                return lb.FileThumbnailPath;
            }
            if (lb.FileTypeId.HasValue && lb.FileTypeId.Value > 0)
            {
                return _fileTypeConfig.GetById(lb.FileTypeId.Value).Thumbnail;
            }
            if (lb.FilePath.Contains("youtube.com"))
            {
                var sindx = lb.FilePath.IndexOf('?');
                sindx = sindx + 3;
                var code = lb.FilePath.Substring(sindx, lb.FilePath.Length - sindx);
                return string.Format("https://img.youtube.com/vi/{0}/hqdefault.jpg", code);
            }

            var indx = lb.FilePath.LastIndexOf('.');
            var ext = lb.FilePath.Substring(indx, lb.FilePath.Length - indx);
            if (!string.IsNullOrEmpty(ext))
            {
                var ft =
                    _fileTypeConfig.GetAll()
                        .FirstOrDefault(
                            x =>
                                (x.Extension.Contains('.') && (x.Extension == ext)) ||
                                (!x.Extension.Contains('.') && (x.Extension == ext.Replace(".", ""))));
                if (ft != null)
                {
                    return ft.Thumbnail;
                }
            }

            return string.Empty;
        }

        #endregion

        #region Admin Dashboard
        [Route("GetAccountsForAdminDashboard")]
        [System.Web.Http.HttpGet]
        public object GetAccountsForAdminDashboard()
        {
            var accounts = new List<dynamic>();
            var result = _sfAccounts.GetAll().Where(x => !string.IsNullOrEmpty(x.AccountPassword) && !string.IsNullOrEmpty(x.PartnerNumber) && !string.IsNullOrEmpty(x.AccountEmail)).DistinctBy(m => m.PartnerNumber).ToList();
            foreach (var u in result)
            {
                dynamic output = new ExpandoObject();
                output.Id = u.Id;
                output.AccountId = u.AccountId;
                output.PartnerNumber = u.PartnerNumber;
                output.AccountEmail = u.AccountEmail;
                output.AccountName = u.AccountName;
                output.Status = u.IsActive;
                output.StatusDisplay = u.IsActive ? "Active" : "Deactive";
                output.DateCreated = _passwordGeneraionLog.GetAll().LastOrDefault(x => x.PartnerNumber == u.PartnerNumber) != null
                    ? (_passwordGeneraionLog.GetAll().LastOrDefault(x => x.PartnerNumber == u.PartnerNumber).PasswordGenerationDate.HasValue ?
                    _passwordGeneraionLog.GetAll().LastOrDefault(x => x.PartnerNumber == u.PartnerNumber).PasswordGenerationDate.Value.ToShortDateString() : string.Empty)
                    : string.Empty;
                output.LastLogin = _sfUserSession.GetAll().LastOrDefault(x => x.PartnerNumber == u.PartnerNumber) != null
                    ? (_sfUserSession.GetAll().LastOrDefault(x => x.PartnerNumber == u.PartnerNumber).LoginTime.HasValue ?
                        _sfUserSession.GetAll().LastOrDefault(x => x.PartnerNumber == u.PartnerNumber).LoginTime.Value.ToString("MM/dd/yyyy hh:mm tt") : string.Empty)
                    : string.Empty;
                output.HasLoyaltyData = _sfTempSessionLoyaltRegistration.Get(x => x.PartnerNumber == u.PartnerNumber) != null ? true : false;
                accounts.Add(output);
            }
            return accounts;
        }

        [Route("ChangeAccountStatus")]
        [System.Web.Http.HttpPost]
        public void ChangeAccountStatus(dynamic account)
        {
            if (account.Id != null && account.Id > 0)
            {
                var acc = _sfAccounts.GetById((int)account.Id);
                acc.IsActive = !acc.IsActive;
                _sfAccounts.Update(acc);
            }
        }

        [Route("ClearLoyaltyData")]
        [System.Web.Http.HttpPost]
        public void ClearLoyaltyData(dynamic account)
        {
            if (account.Id != null && account.Id > 0)
            {
                string partnerNumber = Convert.ToString(account.PartnerNumber);
                var d = _sfTempSessionLoyaltRegistration.GetAll().Where(x => x.PartnerNumber == partnerNumber);
                if (d != null)
                {
                    foreach (var item in d)
                    {
                        _sfTempSessionLoyaltRegistration.Delete(item);
                    }
                }
            }
        }

        [Route("GetVisitsAndPageViewsSummary")]
        [System.Web.Http.HttpGet]
        public object GetVisitsAndPageViewsSummary(int id)
        {
            var pViews = new List<int>();
            var visits = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                visits.Add(_sfUserSession.GetAll()
                    .Count(
                        x =>
                            x.LoginTime.HasValue && x.LoginTime.Value.Year == id &&
                            x.LoginTime.Value.Month == i));
                pViews.Add(_pageViews.GetAll().Count(x => x.VisitDate.Year == id && x.VisitDate.Month == i));

            }
            return new { Visits = visits, PageViews = pViews };
        }

        #endregion

        #region Notifications
        [System.Web.Http.HttpGet]
        public object GetNotifications()
        {
            return
              _notifications.GetAll().OrderBy(x => x.CreateDate).ToList();

        }
        public object GetNotificationsForUser()
        {
            var notificationsForUser = new List<NotificationSearchResult>();

            var sql = string.Format("EXEC GetNotifications @PartnerNumber = '{0}'", _identity.PartnerNumber);

            using (var context = new DataContext())
            {
                notificationsForUser = context.Database.SqlQuery<NotificationSearchResult>(sql).ToList();
            }

            return notificationsForUser;
        }
        [System.Web.Http.HttpGet]
        public object GetNotification(int id)
        {
            var ser = new JavaScriptSerializer();
            var n = _notifications.GetById(id);
            dynamic selectedRecipient = new List<dynamic>();

            var nr = _notificationRecipients.GetAll().Where(x => x.NotificationId == id);
            if (n.Type == 2)
            {
                foreach (var item in nr)
                {
                    dynamic obj = new ExpandoObject();
                    obj.id = item.Recipient;
                    obj.label = item.Recipient;
                    selectedRecipient.Add(obj);
                }
            }
            if (n.Type == 3)
            {
                foreach (var item in nr)
                {
                    var t = _sfAccounts.GetAll().Where(x => x.PartnerNumber == item.Recipient).FirstOrDefault();
                    if (t != null)
                    {
                        dynamic obj = new ExpandoObject();
                        obj.id = t.PartnerNumber;
                        obj.label = string.Format("{0}({1})", t.AccountName, t.PartnerNumber);
                        selectedRecipient.Add(obj);
                    }
                }
            }
            var nrb = _notificationReadBy.GetAll().Where(x => x.NotificationId == id);
            return new
            {
                Notification = new
                {
                    Id = n.Id,
                    Header = n.Header,
                    Type = n.Type.ToString(),
                    Detail = ser.Deserialize<dynamic>(n.Detail),
                    IsActive = n.IsActive
                },
                NotificationRecipients = nr,
                NotificationReadBy = nrb,
                SelectedRecipient = selectedRecipient
            };
        }

        [System.Web.Http.HttpGet]
        public object GetPortalUsersList()
        {
            return
              _sfAccounts.GetAll().Where(x => !string.IsNullOrEmpty(x.AccountPassword) && x.IsActive).OrderBy(m => m.AccountName).ToList();

        }
        [System.Web.Http.HttpGet]
        public object GetPortalPartnerTypes()
        {
            return
              _sfAccounts.GetAll().Where(x => !string.IsNullOrEmpty(x.AccountPassword) && x.IsActive).Select(m => m.PartnerType).Distinct().ToList();

        }

        [Route("SaveNotification")]
        [HttpPost]
        public string SaveNotification(dynamic data)
        {
            var notfn = new Notifications();
            if (data.Id > 0)
            {
                notfn = _notifications.GetById((int)data.Id);
            }
            else
            {
                notfn.CreateDate = DateTime.Now;
                notfn.IsActive = true;
            }
            notfn.Id = data.Id;
            notfn.Type = (int)data.Type;
            notfn.Header = data.Header;

            if (data.Detail != null)
            {
                notfn.Detail = data.Detail.ToString();
            }
            if (data.Id > 0)
            {
                var p = _notifications.GetMany(x => x.Header == notfn.Header && x.Id != notfn.Id && x.IsActive).ToList();
                if (p.Count >= 1)
                {
                    return "Notification already exists";
                }
                _notifications.Update(notfn);
            }
            else
            {
                var p = _notifications.GetMany(x => x.Header == notfn.Header && x.IsActive).ToList();
                if (p.Count >= 1)
                {
                    return "Notification already exists";
                }
                _notifications.Add(notfn);
            }

            //            if(notfn.Type ==2)
            //          {
            if (data.Recipients != null)
            {
                _notificationRecipients.Delete(x => x.NotificationId == notfn.Id);
                var sendEmail = (bool)data.SendEmail;
                foreach (var item in data.Recipients)
                {
                    var notfnR = new NotificationRecipients();
                    notfnR.NotificationId = notfn.Id;
                    notfnR.Recipient = item;
                    _notificationRecipients.Add(notfnR);

                    if (sendEmail)
                    {
                        SendNotificationEmail(notfn);
                    }
                }
            }
            //         }           

            // RETURN A MESSAGE.
            return "Notification saved successfully";
        }

        private void SendNotificationEmail(Notifications notfn)
        {
            var obj = JValue.Parse(notfn.Detail);
            var emailBody = new JsonToHtmlParser(_cmsElementProperty, _productDetails, _uploadInformationRepository).Parse(obj);
            var notfnR = _notificationRecipients.GetAll().Where(x => x.NotificationId == notfn.Id).ToList();
            //_emailService.SendEmail(emailBody, contactEmail, notfn.Header);
            var users = new List<SFAccounts>();
            if (notfn.Type == 1)
            {
                users = _sfAccounts.GetAll().Where(x => !string.IsNullOrEmpty(x.AccountPassword) && !string.IsNullOrEmpty(x.AccountEmail) && x.IsActive).ToList();
            }
            if (notfn.Type == 2 && notfnR != null && notfnR.Count > 0)
            {
                var r = notfnR.FirstOrDefault();
                users = _sfAccounts.GetAll().Where(x => !string.IsNullOrEmpty(x.AccountPassword) && !string.IsNullOrEmpty(x.AccountEmail) && x.IsActive && x.PartnerType.ToLower() == r.Recipient.ToLower()).ToList();
            }
            if (notfn.Type == 3 && notfnR != null && notfnR.Count > 0)
            {
                foreach (var p in notfnR)
                {
                    var s = _sfAccounts.GetAll().Where(x => !string.IsNullOrEmpty(x.AccountPassword) && !string.IsNullOrEmpty(x.AccountEmail) && x.IsActive &&
                    x.PartnerNumber.ToLower() == p.Recipient.ToLower()).FirstOrDefault();
                    if (s != null)
                    {
                        users.Add(s);
                    }
                }
            }

            new Thread(() =>
            {
                try
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(0);
                    foreach (var user in users)
                    {
                        _emailService.SendEmail(emailBody, user.AccountEmail, notfn.Header);
                    }
                }
                catch (Exception ex)
                { }
            }).Start();
        }

        [HttpPost]
        public string ChangeNotificationStatus(dynamic notification)
        {
            var notfn = _notifications.GetById((int)notification.Id);
            var st = (bool)notfn.IsActive;
            notfn.IsActive = !st;

            _notifications.Update(notfn);
            return "Status updated successfully.";
        }

        [HttpPost]
        public string DeleteNotification(dynamic notification)
        {
            var notfn = _notifications.GetById((int)notification.Id);
            if (notfn != null)
            {
                _notificationReadBy.Delete(x => x.NotificationId == notfn.Id);
                _notificationRecipients.Delete(x => x.NotificationId == notfn.Id);
                _notifications.Delete(notfn);
            }
            return "Record deleted.";
        }

        [HttpPost]
        public void SetNotificationAsReadForUser()
        {
            var nrb = _notificationReadBy.GetAll().Where(x => x.ReadBy == _identity.PartnerNumber).Select(m => m.NotificationId).Distinct().ToList();
            var nr1 = _notificationRecipients.GetAll().Where(x => x.Recipient == _identity.PartnerNumber || x.Recipient == _identity.PartnerType).Select(m => m.NotificationId).Distinct().ToList();
            var nr2 = _notifications.GetAll().Where(x => x.Type == 1).Select(m => m.Id).ToList();
            var AllNoticeId = new List<long>();
            if (nr1 != null && nr1.Count > 0)
            {
                foreach (var n in nr1)
                {
                    AllNoticeId.Add(n);
                }
            }
            if (nr2 != null && nr2.Count > 0)
            {
                foreach (var n in nr2)
                {
                    AllNoticeId.Add(n);
                }
            }

            foreach (var item in AllNoticeId)
            {
                if (!nrb.Contains(item))
                {
                    var d = new NotificationReadBy();
                    d.NotificationId = item;
                    d.ReadBy = _identity.PartnerNumber;
                    d.ReadDate = DateTime.Now;
                    _notificationReadBy.Add(d);
                }
            }
        }
        #endregion

        #region Loyalty Registration

        [Route("SaveLoyaltyLeadLocally")]
        [System.Web.Http.HttpPost]
        public void SaveLoyaltyLeadLocally(dynamic opp)
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
            var data = _sfTempSessionLoyaltRegistration.Get(x => x.AccountId == _identity.AccountId && x.PartnerNumber == _identity.PartnerNumber);

            var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProducts,
               _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
               _sessionMdfRepository, _sfAccounts, _delegateReport, _delegateRebateItemReport,
               _identity.SessionKey);
            dynamic dataToPost = opp.DataToPost;

            if (account != null)
            {
                dynamic res = sfClient.AddLoyaltyLead(dataToPost, account.AccountId);
                try
                {
                    var tsd = new SFTempSessionLoyaltRegistration
                    {
                        SessionKey = _identity.SessionKey,
                        AccountId = account.AccountId,
                        AccountName = account.AccountName,
                        PartnerNumber = _identity.PartnerNumber,
                        FirstName = opp.FirstName,
                        LastName = opp.LastName,
                        Email = opp.EmailAddress,
                        HowToSupportStar = opp.HowToSupportStar,
                        HowToSupportStarOther = opp.HowToSupportStarOtherExplanation,
                        ProductsThatSupport = opp.AllStarProducts,
                        ProductsThatSell = opp.AllStarProductsSelling,
                        DoesSellHardware = opp.DoYouSellHardware,
                        HardwareSource = opp.CustomerSourceForHardware,
                        AgreedToNewsLetter = opp.AgreedToPrivacyTerms == null ? false : opp.AgreedToPrivacyTerms,
                        TermsAgreed = opp.AcceptTerms == null ? false : opp.AcceptTerms

                    };
                    if (data != null)
                    {
                        tsd.Id = data.Id;
                        _sfTempSessionLoyaltRegistration.Update(tsd);
                    }
                    else
                    {
                        _sfTempSessionLoyaltRegistration.Add(tsd);
                    }
                }
                catch (Exception ex)
                {


                }
            }
        }

        [Route("GetLoyaltyLeadLocally")]
        [System.Web.Http.HttpGet]
        public dynamic GetLoyaltyLeadLocally(dynamic opp)
        {
            var data = _sfTempSessionLoyaltRegistration.Get(x => x.AccountId == _identity.AccountId && x.PartnerNumber == _identity.PartnerNumber);
            return data;
        }

        [Route("ShowLoyaltyRegnNotification")]
        [System.Web.Http.HttpGet]
        public bool ShowLoyaltyRegnNotification()
        {
            var account = _sfAccounts.Get(x => x.AccountId == _identity.AccountId && x.PartnerNumber == _identity.PartnerNumber && x.IsActive);
            var result = (account != null && (account.PartnerType.ToLower().Contains("services") ||
                account.PartnerType.ToLower().Contains("technology") ||
                account.PartnerType.ToLower().Contains("solution")));

            if (result)
            {
                string partnerNumber = Convert.ToString(account.PartnerNumber);
                var obj = _sfTempSessionLoyaltRegistration.GetAll().FirstOrDefault(x => x.PartnerNumber == _identity.PartnerNumber);
                result = obj == null;
            }
            return result;
        }

        #endregion

        #region Demo Unit Requested

        [Route("SaveDemoUnitRequested")]
        [System.Web.Http.HttpPost]
        public void SaveDemoUnitRequested(dynamic opp)
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
            var data = _sfTempSessionDemoUnitRequested.Get(x => x.AccountId == _identity.AccountId && x.PartnerNumber == _identity.PartnerNumber);

            var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProducts,
               _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
               _sessionMdfRepository, _sfAccounts, _delegateReport, _delegateRebateItemReport,
               _identity.SessionKey);
            dynamic dataToPost = opp.DataToPost;

            if (account != null)
            {
                dynamic res = sfClient.AddDemoUnitRequest(opp, account.AccountId);
                try
                {
                    var tsd = new SFTempSessionDemoUnitRequested
                    {
                        SessionKey = _identity.SessionKey,
                        AccountId = account.AccountId,
                        AccountName = account.AccountName,
                        PartnerNumber = _identity.PartnerNumber,
                        CaseType = "Desk Order Case Record Type",
                        Subject = opp.Subject,
                        Description = opp.Description,
                        ProductName = opp.Product_Name__c,
                        ProductColor = opp.Product_Color__c,
                        ProductInterface = opp.Product_Interface__c,
                        Quantity = opp.Quantity_of_Printers__c
                    };
                    if (data != null)
                    {
                        tsd.Id = data.Id;
                        _sfTempSessionDemoUnitRequested.Update(tsd);
                    }
                    else
                    {
                        _sfTempSessionDemoUnitRequested.Add(tsd);
                    }
                }
                catch (Exception ex)
                {


                }
            }
        }

        #endregion

        #region Training Requested

        [Route("SaveDemoUnitRequested")]
        [System.Web.Http.HttpPost]
        public void SaveTrainingRequested(dynamic opp)
        {
            var account = _sfAccounts.Get(x => x.PartnerNumber == _identity.PartnerNumber);
            var data = _sfTempSessionTrainingRequested.Get(x => x.AccountId == _identity.AccountId && x.PartnerNumber == _identity.PartnerNumber);

            var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProducts,
               _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
               _sessionMdfRepository, _sfAccounts, _delegateReport, _delegateRebateItemReport,
               _identity.SessionKey);

            if (account != null)
            {
                dynamic res = sfClient.AddTrainingRequest(opp, account.AccountId);
                try
                {
                    var tsd = new SFTempSessionTrainingRequested
                    {
                        SessionKey = _identity.SessionKey,
                        AccountId = account.AccountId,
                        AccountName = account.AccountName,
                        PartnerNumber = _identity.PartnerNumber,
                        RequestorName = opp.Training_Requestor_s_Name__c,
                        RequestorMail = opp.Training_Requestor_s_Email__c,
                        Description = opp.Products_or_Markets_you_want_training_on__c,
                        TypeOfTraining = opp.Type_of_training__c
                    };
                    if (data != null)
                    {
                        tsd.Id = data.Id;
                        _sfTempSessionTrainingRequested.Update(tsd);
                    }
                    else
                    {
                        _sfTempSessionTrainingRequested.Add(tsd);
                    }
                }
                catch (Exception ex)
                {


                }
            }
        }

        #endregion
    }
}
