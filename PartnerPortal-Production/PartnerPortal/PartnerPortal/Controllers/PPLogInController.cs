using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PartnerPortal.Business.Security;
using PartnerPortal.Business.Users;
using PartnerPortal.Domain.Admin;
using PartnerPortal.Models;
using PartnerPortal.Models.Admin;
using PartnerPortal.Repository;
using System.Configuration;
using System.Runtime.ExceptionServices;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Utility;

namespace PartnerPortal.Controllers
{
    public class PPLogInController : Controller
    {
        //
        // GET: /LogIn/

        private readonly IAuthenticationService _authenticationService;
        private readonly IEFRepository<SFUserSession> _userSession;
        private readonly IEFRepository<SFAccounts> _accounts;
        private readonly IEFRepository<SFTempSessionData> _sessionData;
        private readonly IEFRepository<SFTempSessionOpportunityData> _sessionOpportunityData;
        private readonly IEFRepository<SFTempSessionOpportunityProducts> _sessionOpportunityProduct;
        private readonly IEncryptionService _encryptionService;
        private readonly IEFRepository<SalesforceAuthentication> _authenticationToken;
        private readonly IEFRepository<SFTempSessionPurchaseByDistributors> _sessionPurchaseByDistributors;
        private readonly IEFRepository<SFTempSessionPurchaseByProductClasses> _sessionPurchaseByProductClasses;
        private readonly IEFRepository<SFTempSessionMdfData> _sessionMdfRepository;
        // private readonly IEFRepository<SFTempSessionMdfRequestData> _sessionMdfRequestRepository;
        private readonly IEFRepository<SFTempSessionDelegateData> _delegateReport;
        private readonly IEFRepository<SFTempSessionDelegateRebateItemData> _delegateRebateItemReport;

        public PPLogInController(IAuthenticationService authenticationService,
            IEFRepository<SFUserSession> userSession,
            IEFRepository<SFAccounts> accounts,
            IEFRepository<SFTempSessionData> sessionData,
            IEFRepository<SalesforceAuthentication> authenticationToken,
            IEFRepository<SFTempSessionOpportunityData> sessionOpportunityData,
            IEFRepository<SFTempSessionOpportunityProducts> sessionOpportunityProduct,
            IEFRepository<SFTempSessionPurchaseByDistributors> sessionPurchaseByDistributors,
            IEFRepository<SFTempSessionPurchaseByProductClasses> sessionPurchaseByProductClasses,
            IEFRepository<SFTempSessionMdfData> sessionMdfRepository,
             IEFRepository<SFTempSessionDelegateData> delegateReport,
             IEFRepository<SFTempSessionDelegateRebateItemData> delegateRebateItemReport,
            // IEFRepository<SFTempSessionMdfRequestData> sessionMdfRequestRepository,
            IEncryptionService encryptionService)
        {
            _authenticationService = authenticationService;
            _userSession = userSession;
            _accounts = accounts;
            _sessionData = sessionData;
            _encryptionService = encryptionService;
            _authenticationToken = authenticationToken;
            _sessionOpportunityData = sessionOpportunityData;
            _sessionOpportunityProduct = sessionOpportunityProduct;
            _sessionPurchaseByDistributors = sessionPurchaseByDistributors;
            _sessionPurchaseByProductClasses = sessionPurchaseByProductClasses;
            _sessionMdfRepository = sessionMdfRepository;
            _delegateReport = delegateReport;
            _delegateRebateItemReport = delegateRebateItemReport;
            //    _sessionMdfRequestRepository = sessionMdfRequestRepository;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoRememberLogin(string sessionKey)
        {
            if(string.IsNullOrEmpty(sessionKey))
                return RedirectToAction("Index", "Gateway");
            
            var session = _userSession.GetAll().Where(x => x.SessionKey == sessionKey).FirstOrDefault();
            
            if(session == null || string.IsNullOrEmpty(session.PartnerNumber))
                return RedirectToAction("Index", "Gateway");

            var account = _accounts.GetAll().Where(x => x.PartnerNumber == session.PartnerNumber).FirstOrDefault();
            if(string.IsNullOrEmpty(account.PartnerNumber) || string.IsNullOrEmpty(account.AccountPassword))
                return RedirectToAction("Index", "Gateway");

            return DoLogin(new GatewayVM
            {
                LoginId = account.PartnerNumber,LoginPassword = account.AccountPassword,RememberMe = true
            });
        }

        //star
        //star@1234
        [HttpPost]
        public ActionResult DoLogin(GatewayVM model)
        {
            var account = _accounts.Get(x => x.PartnerNumber == model.LoginId && x.AccountPassword == model.LoginPassword && x.IsActive);
            if (account != null)
            {

                #region Create Session

                var sessionKey = GenerateSessionKey(account.PartnerNumber);

                //First kill existing open sessions
                var sessions =
                    _userSession.GetAll()
                        .Where(x => x.PartnerNumber == account.PartnerNumber && x.LogoutTime == null)
                        .ToList();
                foreach (var s in sessions)
                {
                    s.LogoutTime = DateTime.Now;
                    s.IsSessionExpired = true;
                    _userSession.Update(s);
                }

                var session = new SFUserSession
                {
                    PartnerNumber = account.PartnerNumber,
                    SessionKey = sessionKey,
                    LoginTime = DateTime.Now
                };
                _userSession.Add(session);
                #endregion
                #region Clear Old Temp data
                _sessionData.Delete(
                        x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _sessionOpportunityData.Delete(
                        x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _sessionOpportunityProduct.Delete(
                        x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _sessionPurchaseByDistributors.Delete(
                        x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _sessionPurchaseByProductClasses.Delete(
                        x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _sessionMdfRepository.Delete(
                    x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _delegateReport.Delete(
                    x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                _delegateRebateItemReport.Delete(
                    x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                //_sessionMdfRequestRepository.Delete(
                //    x => x.PartnerNumber == account.PartnerNumber && x.AccountId == account.AccountId);
                #endregion

                var sfClient = new SFRequestHandler(_authenticationToken, _sessionOpportunityData, _sessionOpportunityProduct, 
                    _sessionPurchaseByDistributors, _sessionPurchaseByProductClasses,
                    _sessionMdfRepository,_accounts,_delegateReport, _delegateRebateItemReport, sessionKey);
                sfClient.GetSFDataForMyAccount(account);
                // dynamic res = sfClient.GetAccountDetail(account.AccountId);
                //dynamic resOpportunity = sfClient.GetOpportunitiesForAccount(account.AccountId);
                #region Fillup Temp Session Data

                //if (res.records != null && Enumerable.Count(res.records)>0)
                //{
                //    var tsd = new SFTempSessionData
                //    {
                //        SessionKey = sessionKey,
                //        AccountId = account.AccountId,
                //        AccountName = res.records[0].Name,
                //        PartnerType = res.records[0].Partner_Type__c,
                //        AccountType = res.records[0].Account_Type__c,
                //        PartnerNumber = account.PartnerNumber,
                //        AccountExpert = res.records[0].Account_Expert__c,
                //        SupportExpert = res.records[0].Support_Expert__c,
                //        DiscountRate = res.records[0].Discount__c,
                //        LoyaltyLevel = res.records[0].Loyalty_Level__c
                //    };

                //    _sessionData.Add(tsd);

                //}
                //opportunities


                #endregion

                _accounts.Update(account);

                var userJsonTemp = new JavaScriptSerializer().Serialize(new { Id = account.Id, AccountId = account.AccountId, SessionKey = sessionKey, PartnerNumber = account.PartnerNumber, PartnerType = account.PartnerType, RememberMe = model.RememberMe });
                var encryptedTicketTemp = _encryptionService.Encrypt(userJsonTemp);
                var cookieName = new HttpCookie("PPCookie", encryptedTicketTemp);
                cookieName.Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookieExpireTime"].ToString()));
                Response.Cookies.Add(cookieName);

                return Redirect("~/Pages/Dashboard");
                
            }
            else
            {
                TempData["LoginFailed"] = "Yes";
                return RedirectToAction("Index", "Gateway");                
            }

        }

        private string GenerateSessionKey(string partnerNumber)
        {
            return string.Format("{0}{1}", partnerNumber, DateTime.Now.ToString("yyyyMMddHHmmssf"));
        }


        //star
        //star@1234
        [HttpGet]
        public ActionResult Logout()
        {
            var _identity = new CurrentIdentity();
            var sessions =
                  _userSession.GetAll()
                      .Where(x => x.PartnerNumber == _identity.PartnerNumber && x.SessionKey == _identity.SessionKey && x.LogoutTime == null)
                      .ToList();
            foreach (var s in sessions)
            {
                s.LogoutTime = DateTime.Now;
                _userSession.Update(s);
            }

            #region Clear Old Temp data
            _sessionData.Delete(
                    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _sessionOpportunityData.Delete(
                    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _sessionOpportunityProduct.Delete(
                    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _sessionPurchaseByDistributors.Delete(
                        x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _sessionPurchaseByProductClasses.Delete(
                        x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _sessionMdfRepository.Delete(
                    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _delegateReport.Delete(
                    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            _delegateRebateItemReport.Delete(
                    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            //_sessionMdfRequestRepository.Delete(
            //    x => x.PartnerNumber == _identity.PartnerNumber && x.AccountId == _identity.AccountId);
            #endregion

            Session.Abandon();
            Response.Cookies.Clear();
            if (Request.Cookies["PPCookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie("PPCookie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Index", "Gateway");
        }
    }
}
