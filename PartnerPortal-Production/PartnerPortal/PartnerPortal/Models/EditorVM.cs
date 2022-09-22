
using System.Collections.Generic;
using PartnerPortal.Domain.Pages;

namespace PartnerPortal.Models
{
    public class EditorVM
    {
        public EditorVM()
        {
            Templates = new List<PageInfo>();
            CustomTemplates = new List<PageInfo>();
        }
        public IList<PageInfo> Templates { get; set; }
        public IList<PageInfo> CustomTemplates { get; set; }

    }
}
