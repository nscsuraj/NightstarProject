using System;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PartnerPortal.Domain.Pages;
using WebGrease.Css.Extensions;

namespace PartnerPortal.Controllers.CMS
{
    [RoutePrefix("api/megamenu")]
    public class MegaMenuController : BaseApiController
    {
        //
        // GET: /MenuApi/
        private readonly IEFRepository<MegaMenu> _menuRepository;
        private readonly IEFRepository<PageInfo> _pageRepository;

        public MegaMenuController(IEFRepository<MegaMenu> menuRepository,
            IEFRepository<PageInfo> pageRepository)
        {
            _menuRepository = menuRepository;
            _pageRepository = pageRepository;
        }

        [Route("GetAll")]
        [HttpGet]
        public object GetAll()
        {
            var data = new List<dynamic>();
            var menus = _menuRepository.GetAll();
            var rec = menus.Where(x => x.ParentId == null && x.IsActive == true).OrderBy(y => y.SortOrder);
            foreach (var q in rec)
            {
                dynamic item = new ExpandoObject();
                item.id = q.Id;
                item.title = q.Title;
                item.nodes = GetMenuListRecursive(q.Id, menus);
                data.Add(item);
            }
            return data;
        }

        [Route("DeletedMenuItems")]
        [HttpGet]
        public object DeletedMenuItems()
        {
            var data = new List<dynamic>();
            var menus = _menuRepository.GetAll().ToList();
            var rec = menus.Where(x => !x.IsActive).OrderBy(y => y.ParentId).ToList();

            var prevParentId = 0;
            var prevParent = new ExpandoObject();

            foreach (var item in rec)
            {
                dynamic d = new ExpandoObject();
                d.id = item.Id;
                d.value = item.Id;
                d.label = item.Title;
                d.lastNode = true;
                d.isSelected = false;
                d.parentId = item.ParentId;
                var parent = new ExpandoObject();
                if (item.ParentId == prevParentId)
                {
                    parent = PushDeletedMenuInParents(prevParent, d);
                }
                else
                {
                    parent = ConstructDeletedMenuParents(menus, item.ParentId.Value, d);
                    data.Add(parent);
                }
                prevParentId = item.ParentId.Value;
                prevParent = parent;
            }

            return data;
        }

        private dynamic PushDeletedMenuInParents(dynamic menu, dynamic child)
        {
            foreach (var item in menu.children)
            {
                if (item.id == child.parentId)
                {
                    item.children.Add(child);
                }
                else
                {
                    PushDeletedMenuInParents(menu, child);
                }
            }
            return menu;
        }

        //See any child node has deleted object or not
        private dynamic ConstructDeletedMenuParents(List<MegaMenu> menus, int menuId,dynamic child )
        {
            var currentMenu = menus.Where(x => x.Id == menuId).FirstOrDefault();
            if (currentMenu != null)
            {
                dynamic m = new ExpandoObject();
                m.id = currentMenu.Id;
                m.label = currentMenu.Title;
                m.value = currentMenu.Id;
                m.lastNode = false;
                m.isSelected = false;
                m.parentId = currentMenu.ParentId;
                m.children = new List<dynamic>();
                m.children.Add(child);
                //parent.Add(m);
                if (currentMenu.ParentId != null)
                {
                    m = ConstructDeletedMenuParents(menus,currentMenu.ParentId.Value,m);
                }
                return m;
            }
            return null;
        }

        [Route("updateDeletedStatus")]
        [HttpPost]
        public void updateDeletedStatus(dynamic menus)
        {
            foreach (dynamic item in menus)
            {
                var menu = _menuRepository.GetById((int)item.Id);
                menu.IsActive = true;
                    _menuRepository.Update(menu);
            }
            HttpContext.Current.Cache.Remove("StarResponsiveCachedMegaMenu");
        }


        [Route("update")]
        [HttpPost]
        public void Update(dynamic menus)
        {
            var oldMenus = _menuRepository.GetAll();
            oldMenus.ForEach(x =>
            {
                x.IsActive = false;
                _menuRepository.Update(x);
            });
            MenuUpdateRecursive(menus, null,1);
            HttpContext.Current.Cache.Remove("StarResponsiveCachedMegaMenu");
        }

        private int MenuUpdateRecursive(dynamic menus,int? parentId, int sortOrder)
        {
            if (menus == null) return sortOrder;
            foreach (dynamic item in menus)
            {
                var menu = new MegaMenu();
                if (item.id > -1)
                {
                    menu = _menuRepository.GetById((int)item.id);
                }
                menu.ParentId = parentId;
                menu.Title = item.title;
                menu.SortOrder = sortOrder;
                sortOrder += 1;
                menu.IsActive = true;
                menu.HasPage = !DynamicListHasElement(item.nodes);
                if (menu.Sites == null)
                {
                    menu.Sites = "EN,ES,PT";
                }
                if (item.id > -1)
                {
                    _menuRepository.Update(menu);
                }
                else
                {
                    _menuRepository.Add(menu);
                }
               sortOrder = MenuUpdateRecursive(item.nodes, menu.Id,sortOrder);
            }
            return sortOrder;
        }

        private IList<dynamic> GetMenuListRecursive(int menuId, IEnumerable<MegaMenu> menus)
        {
            var rec = new List<dynamic>();
            var data = menus.Where(x => x.ParentId == menuId && x.IsActive == true).OrderBy(y => y.SortOrder);
            foreach (var q in data)
            {
                dynamic item = new ExpandoObject();
                item.id = q.Id;
                item.title = q.Title;
                item.nodes = GetMenuListRecursive(q.Id, menus);
                rec.Add(item);
            }
            return rec;
        }

        [Route("GetMenuAndTaggingPage")]
        [HttpGet]
        public object GetMenuAndTaggingPage()
        {
            var parentMenus =
                _menuRepository.GetMany(x => x.IsActive).OrderBy(m => m.SortOrder).ToList();
            var menus = _menuRepository.GetMany(x => x.IsActive && x.Title != "").OrderBy(m=> m.SortOrder).ToList().
                Select(m => new { Title = BuildMenuHeader(m.ParentId, parentMenus, string.Empty) + m.Title, MenuId = m.Id, Url = m.Url, PageId = m.PageId, PageTitle = (m.PageId.HasValue && m.PageId.Value != 0) ? _pageRepository.GetById(m.PageId.Value).Title : string.Empty, Sites = (string.IsNullOrEmpty(m.Sites) ? string.Empty : m.Sites), IsEnglish = (string.IsNullOrEmpty(m.Sites) ? false : m.Sites.Contains("EN")), IsEspanol = (string.IsNullOrEmpty(m.Sites) ? false : m.Sites.Contains("ES")), IsPortu = (string.IsNullOrEmpty(m.Sites) ? false : m.Sites.Contains("PT")) }).ToList();
            return menus;
        }

        private string BuildMenuHeader(int? parentMenuId, IList<MegaMenu> menus, string menuHeader)
        {
            if (parentMenuId.HasValue)
            {
                var m = menus.FirstOrDefault(x => x.Id == parentMenuId && x.IsActive);
                if (m != null)
                {
                    if (menuHeader != string.Empty)
                    {
                        menuHeader = m.Title + " --- " + menuHeader;
                    }
                    else
                    {
                        menuHeader = m.Title;
                    }

                    menuHeader = BuildMenuHeader(m.ParentId, menus, menuHeader);
                }
            }
            if (string.IsNullOrEmpty(menuHeader) || menuHeader.EndsWith(" --- "))
            {
                return menuHeader;
            }
            else
            {
                return menuHeader + " --- ";
            }
        }

        [Route("updatePageId")]
        [HttpPost]
        public void UpdatePageId(dynamic menus)
        {
            foreach (var x in menus)
            {
                int? pageId = x.PageId;
                if (pageId.HasValue || x.Url != string.Empty)
                {
                    int mId = Convert.ToInt32(x.MenuId);
                    var menu = _menuRepository.GetById(mId);
                    menu.PageId = x.PageId;
                    menu.PageTitle = x.PageTitle;
                    menu.Sites = x.Sites;
                    menu.Url = x.Url;
                    _menuRepository.Update(menu);
                }
            }
            HttpContext.Current.Cache.Remove("StarResponsiveCachedMegaMenu");
        }

        [Route("deassociatePageId")]
        [HttpPost]
        public void deassociatePageId(dynamic x)
        {
            int? pageId = x.PageId;
            int mId = Convert.ToInt32(x.MenuId);
            var menu = _menuRepository.GetById(mId);
            menu.PageId = null;
            menu.PageTitle = null;
            menu.Url = string.Empty;
            _menuRepository.Update(menu);
            HttpContext.Current.Cache.Remove("StarResponsiveCachedMegaMenu");
        }
    }
}
