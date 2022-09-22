using System;
using System.Configuration;
using System.Web;
using System.Web.Script.Serialization;
using PartnerPortal.Business.Security;
using PartnerPortal.Domain.Accounts;

namespace PartnerPortal.Utility
{
    /// <summary>
    /// ITrackerIdentity stores the Identity information for the current login
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    08/04/2013      Dwarika     Created   
    /// </remarks>
    public class CurrentIdentity
    {

        private PPUsers _currentUser;

        public CurrentIdentity()
        {
            _currentUser = new PPUsers();

            if (HttpContext.Current.Request.Cookies["PPCookie"] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["PPCookie"];
                if (cookie != null)
                {
                    try
                    {
                        var _encryptionService = new EncryptionService();
                        _currentUser = new JavaScriptSerializer().Deserialize<PartnerPortal.Domain.Accounts.PPUsers>(_encryptionService.Decrypt(cookie.Value));
                    }
                    catch (Exception)
                    {
                        
                    }


                }
            }
            
            
        }

        /// <summary>
        /// User ID of the Identity
        /// </summary>
        public int UserId 
        {
            get
            {
                return _currentUser.Id;
            }
        }

        /// <summary>
        /// User Name of the Identity
        /// </summary>
        public string AccountId
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentUser.AccountId))
                {
                    return _currentUser.AccountId;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// User Account Id of the Identity
        /// </summary>
        public string SessionKey
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentUser.SessionKey))
                {
                    return _currentUser.SessionKey;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// User Account Id of the Identity
        /// </summary>
        public string PartnerNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentUser.PartnerNumber))
                {
                    return _currentUser.PartnerNumber;
                }
                return string.Empty;
            }
        }

        public string PartnerType
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentUser.PartnerType))
                {
                    return _currentUser.PartnerType;
                }
                return string.Empty;
            }
        }
    }
}
