

using System.Collections.Generic;
using PartnerPortal.Domain.Pages;

namespace PartnerPortal.Models
{
    public class PageInfoVM
    {

        public PageInfoVM()
        {
            Gateway = new GatewayVM();
        }
        public int Id { get; set; }
        public string PageName { get; set; }
        public int PageId { get; set; }
        public int PageType { get; set; }
        public string PartPageName { get; set; }
        public string Title { get; set; }
        public string TitleTag { get; set; }
        public string LayoutType { get; set; }

        public IList<PageInfo> PageSections { get; set; } 

        public GatewayVM Gateway { get; set; }
        public string MenuName { get; set; }
        public string PageHeader { get; set; }
    }
}
