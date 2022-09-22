
using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Pages
{
    public class PageInfo
    {
        [Key]
        public int Id { get; set; }
        public int? PageId { get; set; }
        public int PageType { get; set; }
        public string Title { get; set; }
        public string TitleTag { get; set; }
        public string Description { get; set; }
        public string CMSJson { get; set; }

        public DateTime? LastUpdated { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LayoutType { get; set; }
        public bool IsTemplate { get; set; }
        public bool IsCustomTemplate { get; set; }
        public string Sites { get; set; }
        public int? SectionOrder { get; set; }
        public string AllowedPartnerTypes { get; set; }
        public string PageHeader { get; set; }
    }
}
