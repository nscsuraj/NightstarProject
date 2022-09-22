using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using PartnerPortal.Business.Email;
using PartnerPortal.Business.Security;
using PartnerPortal.Core.Attributes;
using PartnerPortal.Core.Enumerations;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Gateway;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.Pages;
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
    public class GatewayController : BaseController
    {
        private readonly IEFRepository<CMS_ElementProperty> _cmsElementProperty;
        private readonly IEFRepository<SystemConfig> _systemConfig;
        private readonly IEFRepository<MetaTags> _metaTags;
        private readonly IEFRepository<UploadInformation> _uploadInformationRepository;
        private readonly IEFRepository<SFAccounts> _sfAccounts;
        private readonly ISMNEmailService _emailService;
        private readonly IEFRepository<PageInfo> _pageInfo;
        
        /// <summary>
        /// Gateway Controller
        /// </summary>
        /// <returns></returns>
        public GatewayController(
            IEFRepository<CMS_ElementProperty>  cmsElementProperty,
            IEFRepository<SystemConfig> systemConfig,
            IEFRepository<MetaTags> metaTags,
            IEFRepository<UploadInformation> uploadInformationRepository,
            IEFRepository<SFAccounts> sfAccounts,
            ISMNEmailService emailService,
             IEFRepository<PageInfo> pageInfo
            )
        {
            _cmsElementProperty = cmsElementProperty;
            _systemConfig = systemConfig;
            _metaTags = metaTags;
            _uploadInformationRepository = uploadInformationRepository;
            _sfAccounts = sfAccounts;
            _emailService = emailService;
            _pageInfo = pageInfo;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
       // [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {


            var ppCookie = Request.Cookies["PPCookie"];
            if (ppCookie != null)
            {
                try
                {
                    var _encryptionService = new EncryptionService();
                    var v = new JavaScriptSerializer().Deserialize<PartnerPortal.Domain.Accounts.PPUsers>(_encryptionService.Decrypt(ppCookie.Value));


                    if (v.RememberMe)
                    {
                        var expTime = Convert.ToInt32(ConfigurationManager.AppSettings["cookieExpireTime"].ToString());

                        var userJsonTemp = new JavaScriptSerializer().Serialize(new { Id = v.Id, AccountId = v.AccountId, SessionKey = v.SessionKey, PartnerNumber = v.PartnerNumber, PartnerType = v.PartnerType, RememberMe = v.RememberMe });
                        var encryptedTicketTemp = _encryptionService.Encrypt(userJsonTemp);
                        var cookieName = new HttpCookie("PPCookie", encryptedTicketTemp);

                        cookieName.Expires = DateTime.Now.AddDays(expTime);
                        Response.Cookies.Add(cookieName);


                        return RedirectToAction("DoRememberLogin", "PPLogIn", new { sessionKey = v.SessionKey });

                    }
                }
                catch (Exception ex)
                {
                }
            }

            var model = new GatewayVM();
            var page = _pageInfo.GetAll().Where(X => X.Title == "Log In").FirstOrDefault();
            if (page != null)
            {
                model.MetaTags = _metaTags.GetMany(x => x.PageId == page.Id).ToList();
                model.PageTitle = page.TitleTag;
            }
            var cnf = _systemConfig.Get(x => x.ConfigKey == "RemoveCache");
            //cnf.ConfigValue = "1";
            if (cnf.ConfigValue == "1")
            {
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Cache)
                {
                    HttpContext.Cache.Remove((string)entry.Key);
                }
                cnf.ConfigValue = "0";
                _systemConfig.Update(cnf);
            }
            model.LoginId = "";
            model.LoginPassword = "";
            if (TempData["LoginFailed"] != null && TempData["LoginFailed"] == "Yes")
            {
                ModelState.AddModelError("LoginFailed", "Either user name or password is incorrect. Please use your EPN (Empower Partner Number) for your username. Contact empower@starmicronics.com if you can not find your EPN.");
                TempData["LoginFailed"] = null;
            }
           
            return View(model);
        }


        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        // [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Register()
        {
            var model = new GatewayVM();
            var page = _pageInfo.GetAll().Where(X => X.Title == "Register").FirstOrDefault();
            if (page != null)
            {
                model.MetaTags = _metaTags.GetMany(x => x.PageId == page.Id).ToList();
                model.PageTitle = page.TitleTag;
            }
            var cnf = _systemConfig.Get(x => x.ConfigKey == "RemoveCache");
            //cnf.ConfigValue = "1";
            if (cnf.ConfigValue == "1")
            {
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Cache)
                {
                    HttpContext.Cache.Remove((string)entry.Key);
                }
                cnf.ConfigValue = "0";
                _systemConfig.Update(cnf);
            }
            model.LoginId = "";
            model.LoginPassword = "";

            ViewBag.Title = "Partner Portal Registration";
            return View(model);
        }

        public ActionResult ForgotPassword()
        {
            var model = new GatewayVM();
            var page = _pageInfo.GetAll().Where(X => X.Title == "Forgot Password").FirstOrDefault();
            if (page != null)
            {
                model.MetaTags = _metaTags.GetMany(x => x.PageId == page.Id).ToList();
                model.PageTitle = page.TitleTag;
            }
            return View(new GatewayVM());
        }

        [HttpPost]
        public ActionResult ForgotPassword(GatewayVM model)
        {
            var token = Guid.NewGuid();
            var ac = _sfAccounts.GetAll().FirstOrDefault(x => x.AccountEmail == model.LoginId && x.PartnerProgramStatus == "Credentials Sent");
            if (ac == null)
            {
                ModelState.AddModelError("LoginFailed", "We could not find your email.");
                return View(model);
            }
            ac.ResetPasswordToken = token;
            _sfAccounts.Update(ac);
            var sb = new StringBuilder();
            sb.Append(string.Format("<b>Dear {0},</b><br><br>",ac.AccountName));
            sb.Append("Please click the below link to reset your password. <br><br>");

            var url = Url.Action("ResetPassword", "Gateway", new { m = token.ToString() }, Request.Url != null ? Request.Url.Scheme : "http");
            sb.Append(string.Format("{0}. <br><br>",url));

            sb.Append("<b>Thanks</b><br>");
            _emailService.SendResetPassword(sb.ToString(),ac.AccountEmail);

            return View("PasswordResetSent",model);
        }

        [HttpGet]
        public string SendResetPasswordLink(string email)
        {
            var token = Guid.NewGuid();
            var ac = _sfAccounts.GetAll().FirstOrDefault(x => x.AccountEmail == email);
            if (ac == null)
            {
                return "Email not found";
            }
            ac.ResetPasswordToken = token;
            _sfAccounts.Update(ac);
            var url = Url.Action("ResetPassword", "Gateway", new { m = token.ToString() }, Request.Url != null ? Request.Url.Scheme : "http");
            try
            {
                new Thread(() =>
                {
                    try
                    {
                        Thread.CurrentThread.IsBackground = true;
                        Thread.Sleep(0);
                        var sb = new StringBuilder();
                        sb.Append(string.Format("<b>Dear {0},</b><br><br>", ac.AccountName));
                        sb.Append("Please click the below link to reset your password. <br><br>");
                        
                        sb.Append(string.Format("{0}. <br><br>", url));
                        sb.Append("<b>Thanks</b><br>");
                        _emailService.SendResetPassword(sb.ToString(), ac.AccountEmail);
                    }
                    catch (Exception ex)
                    {
                    }
                }).Start();
            }
            catch (Exception)
            {

                return "Error in email send";
            }          
            //_emailService.SendResetPassword(sb.ToString(), ac.AccountEmail);

            return "Done";
        }

        public ActionResult ResetPassword(string m)
        {
            Guid token;
            var model = new GatewayVM();
            if (Guid.TryParse(m, out token))
            {
                var ac = _sfAccounts.GetAll().FirstOrDefault(x => x.ResetPasswordToken == token);
                if (ac != null)
                {
                    model.LoginId = ac.AccountEmail;
                    var page = _pageInfo.GetAll().Where(X => X.Title == "Reset Password").FirstOrDefault();
                    if (page != null)
                    {
                        model.MetaTags = _metaTags.GetMany(x => x.PageId == page.Id).ToList();
                        model.PageTitle = page.TitleTag;
                    }
                }
                else
                {
                    ModelState.AddModelError("Invalid Token", "Your reset link has expired. Please get a new link.");
                }
            }
            else
            {
                ModelState.AddModelError("Invalid Token", "Your reset link has expired. Please get a new link.");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(GatewayVM model)
        {
            var ac = _sfAccounts.GetAll().FirstOrDefault(x => x.AccountEmail == model.LoginId && x.PartnerProgramStatus == "Credentials Sent");
            if (ac != null)
            {
                ac.AccountPassword = model.LoginPassword;
                ac.ResetPasswordToken = null;
                _sfAccounts.Update(ac);
            }
            return View("ResetPasswordDone", model);
        }

        /// <summary>
        ///     Get Display Chat Value
        /// </summary>
        /// <returns>Result</returns>      
        public string DisplayChat(string configKey)
        {
            var configValue = string.Empty;
            using (var context = new ChatDataContext())
            {
                var sql = "Select SettingsValue From tblSettings Where SettingKey = '" + configKey + "';";
                configValue = context.Database.SqlQuery<string>(sql).FirstOrDefault();
            }
            return configValue;
        }
    }
}
