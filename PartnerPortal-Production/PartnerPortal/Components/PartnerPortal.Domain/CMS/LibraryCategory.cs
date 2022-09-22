using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.CMS
{
    public class LibraryCategory
    {
        [Key]
        public int Id { get; set; }

        public string LibraryType { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public string Description { get; set; }
        public string AllowedPartnerTypes { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
    }
}
