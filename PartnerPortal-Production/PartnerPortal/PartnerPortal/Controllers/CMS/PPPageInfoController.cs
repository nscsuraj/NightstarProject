using PartnerPortal.Domain.Pages;
using PartnerPortal.Repository;
using System;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using PartnerPortal.Business.Email;
using PartnerPortal.Domain.Import;
using PartnerPortal.Domain.SiteUtility;

namespace PartnerPortal.Controllers.CMS
{
    [RoutePrefix("api/0")]
    public class PPPageInfoController : BaseApiController
    {
        //
        // GET: /Editor/
        private readonly IEFRepository<PageInfo> _pageRepository;
        private readonly IEFRepository<MegaMenu> _menuRepository;
        private readonly IEFRepository<ProductDetails> _productRepository;

        private readonly ISMNEmailService _smnEmailService;

        public PPPageInfoController(IEFRepository<MegaMenu> menuRepository, IEFRepository<PageInfo> pageRepository,
            IEFRepository<ProductDetails> productRepository,

            ISMNEmailService smnEmailService
            )
        {
            _menuRepository = menuRepository;
            _pageRepository = pageRepository;
            _productRepository = productRepository;

            _smnEmailService = smnEmailService;
        }

        [Route("SavePage")]
        [HttpPost]
        public string SavePage(dynamic data)
        {
            if (data.IsCustomTemplate == null)
            {
                data.IsCustomTemplate = false;
            }
            var page = new PageInfo();
            var pageSavedAsCustomTemplate = false;
            if (data.Id > 0)
            {
                page = _pageRepository.GetById((int)data.Id);
                if (!page.IsCustomTemplate)
                {
                    if ((bool)data.IsCustomTemplate)
                    {
                        pageSavedAsCustomTemplate = true;
                    }
                }
            }
            page.Id = data.Id;
            page.PageType = data.PageType;
            page.PageId = data.PageId;
            page.SectionOrder = data.SectionOrder;
            page.Title = data.Title;
            page.TitleTag = data.TitleTag;
            page.PageHeader = data.PageHeader;
            page.Description = data.Description;
            page.Status = true;
            if (data.PageJson != null)
            {
                page.CMSJson = data.PageJson.ToString();
            }
            page.IsTemplate = data.IsTemplate;
            page.LayoutType = data.LayoutType;
            page.LastUpdated = DateTime.Now;
            page.IsCustomTemplate = data.IsCustomTemplate;
            page.Sites = data.Sites;
            if (pageSavedAsCustomTemplate)
            {
                page.CreateDate = DateTime.Now;
                _pageRepository.Add(page);
            }
            if (data.Id > 0)
            {
                var p = _pageRepository.GetMany(x => x.Title == page.Title && x.Id != page.Id && x.IsCustomTemplate == page.IsCustomTemplate && x.PageType == page.PageType).ToList();
                if (p.Count >= 1)
                {
                    return "Page with same title already exists";
                }
                _pageRepository.Update(page);
            }
            else
            {
                var p = _pageRepository.GetMany(x => x.Title == page.Title && x.IsCustomTemplate == page.IsCustomTemplate && x.PageType == page.PageType).ToList();
                if (p.Count == 0)
                {
                    page.CreateDate = DateTime.Now;
                    _pageRepository.Add(page);
                }
                else
                {
                    return "Page with same title already exists";
                }
            }
            // RETURN A MESSAGE.
            return "Page saved successfully";
        }

        [Route("SavePageProperties")]
        [HttpPost]
        public string SavePageProperties(dynamic data)
        {
            var page = new PageInfo();
            if (data.Id > 0)
            {
                page = _pageRepository.GetById((int)data.Id);

                page.Id = data.Id;
                page.TitleTag = data.TitleTag;
                page.PageHeader = data.PageHeader;
                page.Description = data.Description;
                _pageRepository.Update(page);
            }
            
            // RETURN A MESSAGE.
            return "Page saved successfully";
        }

        [Route("GetPageById/{id:int}")]
        [HttpGet]
        public dynamic GetPageById(int id)
        {
            var ser = new JavaScriptSerializer();
            var obj = _pageRepository.Get(x => x.Id == id);
            if (obj == null)
                return null;
            return new
            {
                Id = obj.Id,
                PageJson = ser.Deserialize<dynamic>(obj.CMSJson)
            };
        }

        [Route("GetSpecialPageByTypeId/{id:int}")]
        [HttpGet]
        public dynamic GetSpecialPageByTypeId(int id)
        {
            var ser = new JavaScriptSerializer();
            var obj = _pageRepository.Get(x => x.PageType == id);
            if (obj == null)
                return null;
            return new
            {
                Id = obj.Id,
                PageJson = ser.Deserialize<dynamic>(obj.CMSJson)
            };
        }

        [Route("GetPageList/{id:int}")]
        [HttpGet]
        public dynamic GetPageList(int id)
        {
            var ser = new JavaScriptSerializer();
            return
                _pageRepository.GetMany(m => m.PageType == id && m.Status == true)
                    .ToList()
                    .Select(
                        x =>
                            new
                            {
                                Id = x.Id,
                                PageType=x.PageType,
                                Title = x.Title,
                                TitleTag = x.TitleTag,
                                PageHeader = x.PageHeader,
                                Description = x.Description,
                                Status = x.Status,
                                CreateDate = x.CreateDate,
                                LayoutType = x.LayoutType,
                                IsTemplate = x.IsTemplate,
                                IsCustomTemplate = x.IsCustomTemplate,
                                SearchTextGlobal = x.Title + " " + x.Description,
                                Sites = (string.IsNullOrEmpty(x.Sites) ? string.Empty : x.Sites ),
                                PageJson = string.IsNullOrEmpty(x.CMSJson)?"":
                                    ser.Deserialize<dynamic>(Convert.ToString(x.CMSJson))
                            });
        }

        [Route("GetPageForCMS/{id:int}")]
        [HttpGet]
        public dynamic GetPageForCMS(int id)
        {
            var ser = new JavaScriptSerializer();
            var x =
                _pageRepository.Get(m => m.Id == id && m.Status == true);
            return new
            {
                Id = x.Id,
                PageType = x.PageType,
                Title = x.Title,
                TitleTag = x.TitleTag,
                Description = x.Description,
                Status = x.Status,
                CreateDate = x.CreateDate,
                LayoutType = x.LayoutType,
                IsTemplate = x.IsTemplate,
                IsCustomTemplate = x.IsCustomTemplate,
                SearchTextGlobal = x.Title + " " + x.Description,
                Sites = (string.IsNullOrEmpty(x.Sites) ? string.Empty : x.Sites),
                PageJson =
                    ser.Deserialize<dynamic>(Convert.ToString(x.CMSJson))
            };
        }

        [Route("GetPageListForMenuAssociation")]
        [HttpGet]
        public dynamic GetPageListForMenuAssociation()
        {
            var ser = new JavaScriptSerializer();
            return
                _pageRepository.GetMany(m => (m.PageType == 2 || m.PageType==3) && m.Status == true)
                    .ToList()
                    .Select(
                        x =>
                            new
                            {
                                Id = x.Id,
                                PageType = x.PageType,
                                Title = x.Title,
                                TitleTag = x.TitleTag,
                                Description = x.Description,
                                Status = x.Status,
                                CreateDate = x.CreateDate,
                                LayoutType = x.LayoutType,
                                IsTemplate = x.IsTemplate,
                                IsCustomTemplate = x.IsCustomTemplate,
                                SearchTextGlobal = x.Title + " " + x.Description,
                                Sites = (string.IsNullOrEmpty(x.Sites) ? string.Empty : x.Sites)
                            });
        }

        [Route("GetPages")]
        [HttpGet]
        public dynamic GetPages()
        {
            return
                _pageRepository.GetMany(m => m.IsCustomTemplate == false && m.IsTemplate == false && m.Status == true && (m.PageType == 2 || m.PageType == 3 || m.PageType == 5))
                    .ToList()
                    .Select(
                        x =>
                            new
                            {
                                Id = x.Id,
                                PageType = x.PageType,
                                Title = x.Title,
                                TitleTag = x.TitleTag,
                                PageHeader = x.PageHeader
                            });
        }

        [Route("ChangeStatus")]
        [HttpPost]
        public string ChangeStatus(dynamic pageD)
        {
            var pageO = _pageRepository.GetById((int)pageD.Id);
            var st = (bool) pageD.Status;
            pageO.Status = !st;

            _pageRepository.Update(pageO);
            return "Status updated successfully.";
        }

        [Route("Delete")]
        [HttpPost]
        public string Delete(dynamic pageD)
        {
            var pageO = _pageRepository.GetById((int)pageD.Id);
            _pageRepository.Delete(pageO);

            int pageId = Convert.ToInt32(pageD.Id);

            var m = _menuRepository.GetMany(x => x.PageId == pageId).ToList();
            if (m.Count > 0)
            {
                foreach (var t in m)
                {
                    t.PageId = null;
                    _menuRepository.Update(t);
                }
            }

            return "Page deleted successfully.";
        }

        [Route("GetProductNumbers")]
        [HttpGet]
        public object GetProductNumbers()
        {
            return _productRepository.GetAll().ToList().Select(x=> new{ItemCode = x.BindingPart, Item = x.BindingPart}).ToList().Distinct().ToList();
        }

        
    }
}
