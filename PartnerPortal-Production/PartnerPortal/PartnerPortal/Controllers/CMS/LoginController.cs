using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using PartnerPortal.Business.Security;
using PartnerPortal.Business.Users;
using PartnerPortal.Domain.Admin;
using PartnerPortal.Models.Admin;
using PartnerPortal.Repository;

namespace PartnerPortal.Controllers.CMS
{
    public class LoginController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly IEFRepository<UserSession> _userSession;
        private readonly IEFRepository<UserProfile> _user;
        private readonly IEncryptionService _encryptionService;

        public LoginController(IAuthenticationService authenticationService,
            IEFRepository<UserSession> userSession,
            IEFRepository<UserProfile> user,
            IEncryptionService encryptionService)
        {
            _authenticationService = authenticationService;
            _userSession = userSession;
            _user = user;
            _encryptionService = encryptionService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View(new UserVM());
        }

        //star
        //star@1234
        [HttpPost]
        public ActionResult DoLogin(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var ppp = _encryptionService.Encrypt(model.LoginPassword);
                if (_authenticationService.Validate(model.LoginId, model.LoginPassword))
                {
                    var user = _user.Get(x => x.LoginId == model.LoginId);
                    var userJson = new JavaScriptSerializer().Serialize(new { LoginId = model.LoginId, UserId = user.Id, UserLevel = user.UserLevel });

                    var encryptedTicket = _encryptionService.Encrypt(userJson);

                    var userSession = new UserSession
                    {
                        SessionKey = userJson,
                        LastAccessed = DateTime.Now,
                        UserId = user.Id
                    };

                    _userSession.Add(userSession);

                    // Create a cookie and add the encrypted ticket to the cookie as data.
                    Response.Cookies.Add(new HttpCookie("PPCMSUserSession", encryptedTicket));
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "Either user name or password for user name " + model.LoginId + " is incorrect.");
                    return View("Index", model);
                }

            }
            else
            {
                return View("Index",model);
            }
        }

        //star
        //star@1234
        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToAction("Index","Login");
        }
    }
}
