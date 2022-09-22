using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.CMS
{
    public class LibraryFiles
    {
        [Key]
        public int Id { get; set; }

        public string LibraryType { get; set; }
        public int LibraryCategoryId { get; set; }
        public int? FileTypeId { get; set; }
        public string Title { get; set; }
        public string TitleSpanish { get; set; }
        public string Description { get; set; }
        public string DescriptionSpanish { get; set; }
        public string FilePath { get; set; }
        public string FileThumbnailPath { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public string AllowedPartnerTypes { get; set; }
    }
}
