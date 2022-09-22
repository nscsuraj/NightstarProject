using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.SiteUtility
{
    public class MegaMenu
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public string PageTitle { get; set; }

        public bool? HasPage { get; set; }

        public int? PageId { get; set; }

        public int? SortOrder { get; set; }

        public bool IsActive { get; set; }

        public string Url { get; set; }

        public string Sites { get; set; }

        public string AllowedPartnerTypes { get; set; }
    }
}
