using System.ComponentModel.DataAnnotations;
namespace PartnerPortal.Domain.Import
{
    public class ImportedResources
    {
        [Key]
        public int Id { get; set; }

        public string ImportID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string ResourceType{get;set;}
        public string PhotoId { get; set; }
        public string PhotoThumbnailSrc { get; set; }
        public string PhotoMediumSrc { get; set; }
        public string PhotoLargeSrc { get; set; }
        public string Status { get; set; }
        public string ParentID { get; set; }
    }
}
