using System.Collections.Generic;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System;
using System.Web;
using PartnerPortal.Utility;
using PartnerPortal.Domain.Accounts;

namespace PartnerPortal.Controllers
{
    public class MenuController : Controller
    {
        private readonly IEFRepository<MegaMenu> _menuRepository;
        private readonly IEFRepository<SystemConfig> _systemConfig;
        private readonly IEFRepository<Notifications> _notifications;
        private readonly IEFRepository<NotificationRecipients> _notificationRecipients;
        private readonly IEFRepository<NotificationReadBy> _notificationReadBy;

        private CurrentIdentity _identity;
        private int itemsCountPerColumn;

        public MenuController(IEFRepository<MegaMenu> menuRepository,
            IEFRepository<SystemConfig> systemConfig,
             IEFRepository<Notifications> notifications,
             IEFRepository<NotificationRecipients> notificationRecipients,
             IEFRepository<NotificationReadBy> notificationReadBy)
        {
            _menuRepository = menuRepository;
            _systemConfig = systemConfig;
            _identity = new CurrentIdentity();
            _notifications = notifications;
            _notificationRecipients = notificationRecipients;
            _notificationReadBy = notificationReadBy;
        }

        public ActionResult Index()
        {
            return null;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetMegaMenu()
        {
            var cnf = _systemConfig.Get(x => x.ConfigKey == "RemoveCache");
            //cnf.ConfigValue = "1";
            //if (cnf.ConfigValue == "1")
            //{
            //    foreach (System.Collections.DictionaryEntry entry in HttpContext.Cache)
            //    {
            //        HttpContext.Cache.Remove((string)entry.Key);
            //    }
            //    cnf.ConfigValue = "0";
            //    _systemConfig.Update(cnf);
            //}

            var notificationsForUser = new List<NotificationSearchResult>();

            var sql = string.Format("EXEC GetNotifications @PartnerNumber = '{0}'", _identity.PartnerNumber);

            using (var context = new DataContext())
            {
                notificationsForUser = context.Database.SqlQuery<NotificationSearchResult>(sql).ToList();
            }

            var notificationCount = 0;
            if (notificationsForUser != null && notificationsForUser.Count > 0)
            {
                notificationCount = notificationsForUser[0].UnreadRecords;
            }

            var megaMenus = _menuRepository.GetAll().Where(x => x.IsActive && (string.IsNullOrEmpty(x.AllowedPartnerTypes) || x.AllowedPartnerTypes.IndexOf(_identity.PartnerType) >= 0)).OrderBy(y => y.SortOrder).ToList();
            var topMenus = megaMenus.Where(x => x.ParentId == null);
            var sb = new StringBuilder();
            var commonColWidth = string.Empty;
            sb.Append("<div class='topnav' id='myTopnav'>");
            sb.Append("<div class='topnavContainer'>");
            var cssClass = "activeMenu";
            foreach (var item in topMenus)
            {
                item.Title = item.Title.Split('-')[0];
                var menus =
                    _menuRepository.GetAll()
                        .Where(x => x.IsActive && x.ParentId == item.Id)
                        .OrderBy(y => y.SortOrder)
                        .ToList();
                if (menus.Count > 0)
                {
                    sb.Append("<div class='dropdown'>");
                    sb.Append("<button class='dropbtn'>" + item.Title);
                    sb.Append("<i class='fa fa-caret-down'></i>");
                    sb.Append("</button>");
                    sb.Append("<div class='dropdown-content" + cssClass + "'>");
                    GetMegaMenuSubItem(sb, menus);
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    if (item.PageId.HasValue)
                    {
                        sb.Append("<a href='" + GetBaseUrl() + "/pages/" + item.PageTitle + "' class='menuItem " + cssClass + " " + item.Title.Replace(" ", "") + "'>" + item.Title + "</a>");
                    }
                    else if (!string.IsNullOrEmpty(item.Url))
                    {
                        sb.Append("<a href='" + item.Url + "' class='menuItem " + cssClass + " " + item.Title.Replace(" ", "") + "'>" + item.Title + "</a>");
                    }
                    else
                    {
                        sb.Append("<a href='##menu-" + item.Title.Replace(".", "") + "' class='menuItem " + cssClass + " " + item.Title.Replace(" ", "") + "'>" + item.Title + "</a>");
                    }
                }
                cssClass = "";
            }
            //Create Notification list
            var notificationList = new StringBuilder();
            notificationList.Append("<div class='dropdown-content notificationList'  style=''>");
            notificationList.Append("<div style='margin-left:20px;margin-right:10px;margin-bottom:10px; margin-top:20px;overflow-y:scroll;' class='beautifulScrollbar'>");
            foreach (var item in notificationsForUser)
            {
                notificationList.Append("<div style='cursor:pointer;margin-bottom:20px;' onclick=viewNotificationDetail(" + item.Id.ToString() + ")><span style='font-size:14pt;font-weight:bold;'>Important notice:</span><br/>");
                notificationList.Append("<span style='font-size:12pt;font-weight:normal;'>" + item.Header + "</span>");
                notificationList.Append("</div>");
            }
            notificationList.Append("</div>");
            notificationList.Append("<span class='fade-effectNotificationList'></span>");
            notificationList.Append("<div class='btnViewAllNotification' onclick='viewAllNotifications()'>All Notifications >></div>");
            notificationList.Append("</div>");
            // end create notification list
            var stM = string.Format("<span onclick='notificationBellClickedM(event)'><span id='notificationBellIcon' class='fa-stack ' data-count='{0}'><i class='fa fa-bell fa-stack-1x fa-inverse'></i></span><span id='notificationBellIcon1M' style='display:none;background-color:blue;width:30px;padding-top:6px;height:23px;padding-left:10px;'><i class='fa fa-bell'></i></span>{1}</span></a>", notificationCount, notificationList);

            sb.Append("  <a href='javascript:void(0);' style='font-size:15px;' class='icon'><span  onclick='myMenuMobile()'><i class='fa fa-bars'></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>" + stM);
            //sb.Append("  <a href='javascript:void(0);' style='font-size:15px;' class='' onclick='notificationBellClicked()'><i class='fa fa-bell'></i></a>");


            sb.Append("<div class='hideBellOnMobile' style='margin-left:10px;margin-top:15px;color:#ffffff;	cursor :pointer;' onclick='notificationBellClicked(event)'>");
            if (notificationCount > 0)
            {
                sb.Append("<span id='notificationBellIcon' class='fa-stack' data-count='" + notificationCount + "'><i class='fa fa-bell fa-stack-1x fa-inverse'></i></span>");
                sb.Append("<span id='notificationBellIcon1d' style='display:none;background-color:blue;width:40px;padding-top:6px;height:30px;'><i class='fa fa-bell'></i></span>");
            }
            else
            {
                //sb.Append("<span id='notificationBellIcon' class='fa-stack' data-count='28'><i class='fa fa-bell fa-stack-1x fa-inverse'></i></span>");
                sb.Append("<span id='notificationBellIcon1d' style='display:block;background-color:blue;width:40px;padding-top:6px;height:30px;'><i class='fa fa-bell'></i></span>");
            }


            sb.Append(notificationList);
            sb.Append("</div>");

            sb.Append("</div>");
            sb.Append("</div>");
            //var cached = "StarResponsiveCachedMegaMenu";
            //var cachedMenu = HttpContext.Cache[cached] as string;
            //if (cachedMenu == null)
            //{
            //    cachedMenu = sb.ToString();
            //    HttpContext.Cache[cached] = cachedMenu;
            //}

            return PartialView("Partials/MegaMenuPartial", sb.ToString());
        }

        private int CountMenuItem(int currentCount, int parentId)
        {
            var items = _menuRepository.GetAll().Where(x => x.ParentId == parentId && x.IsActive);
            currentCount += items.Count();
            foreach (var item in items)
            {
                if (_menuRepository.GetAll().Any(x => x.ParentId == item.Id && x.IsActive))
                {
                    currentCount = CountMenuItem(currentCount, item.Id);
                }
            }
            return currentCount;
        }

        private void GetMegaMenuSubItem(StringBuilder sb, IList<MegaMenu> menus)
        {
            var cssClass = "";
            foreach (var item in menus)
            {
                item.Title = item.Title.Split('-')[0];
                var submenus =
                    _menuRepository.GetAll()
                        .Where(x => x.IsActive && x.ParentId == item.Id)
                        .OrderBy(y => y.SortOrder)
                        .ToList();
                if (submenus.Count > 0)
                {
                    sb.Append("<div class='dropdown'>");
                    sb.Append("<button class='dropbtn'>" + item.Title);
                    sb.Append("<i class='fa fa-caret-down'></i>");
                    sb.Append("</button>");
                    sb.Append("<div class='dropdown-content'>");
                    GetMegaMenuSubItem(sb, submenus);
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    if (item.PageId.HasValue)
                    {
                        sb.Append("<a href='" + GetBaseUrl() + "/pages/" + item.PageTitle + "' class='" + cssClass + "'>" + item.Title + "</a>");
                    }
                    else if (!string.IsNullOrEmpty(item.Url))
                    {
                        sb.Append("<a href='" + item.Url + "' class='" + cssClass + "'>" + item.Title + "</a>");
                    }
                    else
                    {
                        sb.Append("<a href='##menu-" + item.Title.Replace(".", "") + "' class='" + cssClass + "'>" + item.Title + "</a>");
                    }
                }
                cssClass = "";
            }
        }

        private void GetMegaMenuAdv(StringBuilder sb, int itemId, string itemTitle, bool addUl)
        {

        }

        public string GetBaseUrl()
        {
            var request = Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl == "/")
                appUrl = "";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }
}