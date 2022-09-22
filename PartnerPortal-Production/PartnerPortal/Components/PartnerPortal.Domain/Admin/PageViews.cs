using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Admin
{
    public class PageViews
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public long Id { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PartnerNumber { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
