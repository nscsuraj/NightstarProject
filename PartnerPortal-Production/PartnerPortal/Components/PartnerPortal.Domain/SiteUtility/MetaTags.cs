using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.SiteUtility
{
    public class MetaTags
    {
        [Key]
        public int Id { get; set; }

        public int TagType { get; set; }
        public string TagKey { get; set; }
        public string TagValue { get; set; }
        public int PageId { get; set; }
    }
}
