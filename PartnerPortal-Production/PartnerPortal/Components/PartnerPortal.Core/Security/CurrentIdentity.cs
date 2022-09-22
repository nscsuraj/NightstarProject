//using System;
//using System.Web;

//namespace PartnerPortal.Core.Security
//{
//    /// <summary>
//    /// ITrackerIdentity stores the Identity information for the current login
//    /// </summary>
//    /// <remarks>
//    ///    Date            Developer   Description
//    ///    08/04/2013      Dwarika     Created   
//    /// </remarks>
//    public class CurrentIdentity
//    {

//        /// <summary>
//        /// User ID of the Identity
//        /// </summary>
//        public string UserId 
//        {
//            get
//            {
//                if (HttpContext.Current.Request.Cookies["PPCookie"] != null)
//                {
//                    HttpCookie cookie = HttpContext.Current.Request.Cookies["PPCookie"];
//                    return cookie["Id"];
//                }
//                return string.Empty;
//            }
//        }

//        /// <summary>
//        /// User Name of the Identity
//        /// </summary>
//        public string AccountId
//        {
//            get
//            {
//                if (HttpContext.Current.Request.Cookies["PPCookie"] != null)
//                {
//                    HttpCookie cookie = HttpContext.Current.Request.Cookies["PPCookie"];
//                    return cookie["AccountId"];
//                }
//                return string.Empty;
//            }
//        }

//        /// <summary>
//        /// User Account Id of the Identity
//        /// </summary>
//        public string SessionKey
//        {
//            get
//            {
//                if (HttpContext.Current.Request.Cookies["PPCookie"] != null)
//                {
//                    HttpCookie cookie = HttpContext.Current.Request.Cookies["PPCookie"];
//                    return cookie["SessionKey"];
//                }
//                return string.Empty;
//            }
//        }

//        /// <summary>
//        /// User Account Id of the Identity
//        /// </summary>
//        public string PartnerNumber
//        {
//            get
//            {
//                if (HttpContext.Current.Request.Cookies["PPCookie"] != null)
//                {
//                    HttpCookie cookie = HttpContext.Current.Request.Cookies["PPCookie"];
//                    return cookie["PartnerNumber"];
//                }
//                return string.Empty;
//            }
//        }
//    }
//}
