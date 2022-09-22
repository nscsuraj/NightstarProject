using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.Pages;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Models;
using PartnerPortal.Repository;
using PartnerPortal.SiteMapUtility;
using PartnerPortal.Utility;

namespace PartnerPortal.Controllers
{
    /// <summary>
    ///     Supports Controller
    /// </summary>
    //[RedirectionChecker]
    public class SiteMapController : BaseController
    {
        /// <summary>
        ///   Member Variables
        /// </summary>
        private readonly IEFRepository<MegaMenu> _menuRepository;
        /// <summary>
        ///     Supports Constructor
        /// </summary>
        public SiteMapController(
            IEFRepository<PageInfo> pageInfoRepository,
            IEFRepository<MegaMenu> menuRepository,
            IEFRepository<UploadInformation> uploadInformationRepository)
        {
            _menuRepository = menuRepository;
        }

        [Route("sitemap.xml")]
        public ActionResult Index()
        {
            var sitemapItems = new List<SitemapItem>();
            sitemapItems = GetMenu(sitemapItems);
            return new SitemapResult(sitemapItems);
        }


        public List<SitemapItem> GetMenu(List<SitemapItem> sitemapItems)
        {
            var megaMenus = _menuRepository.GetAll().Where(x => x.IsActive).OrderBy(y => y.SortOrder).ToList();
            var topMenus = megaMenus.Where(x => x.ParentId == null);
            sitemapItems.Add(new SitemapItem(PathUtils.CombinePaths(Request.Url.GetLeftPart(UriPartial.Authority), "/"), lastModified: DateTime.Now));
            foreach (var item in topMenus)
            {
                var menus =
                    _menuRepository.GetAll()
                        .Where(x => x.IsActive && x.ParentId == item.Id)
                        .OrderBy(y => y.SortOrder)
                        .ToList();
                if (!string.IsNullOrEmpty(item.Title))
                {
                    if (item.PageId.HasValue)
                    {
                        sitemapItems.Add(new SitemapItem(PathUtils.CombinePaths(Request.Url.GetLeftPart(UriPartial.Authority), "/pages/" + item.PageTitle), lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
                    }
                    else if (!string.IsNullOrEmpty(item.Url))
                    {
                        sitemapItems.Add(new SitemapItem(item.Url, lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
                    }
                    sitemapItems = GetMenuSubItem(menus, sitemapItems);
                }
            }
            sitemapItems.Add(new SitemapItem(PathUtils.CombinePaths(Request.Url.GetLeftPart(UriPartial.Authority), "/pages/ContactUs"), lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
            sitemapItems.Add(new SitemapItem(PathUtils.CombinePaths(Request.Url.GetLeftPart(UriPartial.Authority), "/blog"), lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));

            return sitemapItems;
        }

        private List<SitemapItem> GetMenuSubItem(IList<MegaMenu> menus, List<SitemapItem> sitemapItems)
        {
            foreach (var item in menus)
            {
                var subMenus =
                    _menuRepository.GetAll()
                        .Where(x => x.IsActive && x.ParentId == item.Id)
                        .OrderBy(y => y.SortOrder)
                        .ToList();

                if (!string.IsNullOrEmpty(item.Title))
                {
                    if (item.PageId.HasValue)
                    {
                        sitemapItems.Add(new SitemapItem(PathUtils.CombinePaths(Request.Url.GetLeftPart(UriPartial.Authority), "/pages/" + item.PageTitle), lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
                    }
                    else if (!string.IsNullOrEmpty(item.Url))
                    {
                        sitemapItems.Add(new SitemapItem(item.Url, lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
                    }
                    sitemapItems = GetMenuSubItem(subMenus, sitemapItems);
                }
            }
            return sitemapItems;
        }


        public ActionResult GenerateSiteMap()
        {

            var sitemapItems = new List<SitemapItem>();

            sitemapItems = GetMenu(sitemapItems);

            SitemapGenerator sg = new SitemapGenerator();
            var doc = sg.GenerateSiteMap(sitemapItems);

            doc.Save(Server.MapPath("~/Sitemap.xml"));

            return RedirectToAction("Index", "Gateway");
        }
    }
}
