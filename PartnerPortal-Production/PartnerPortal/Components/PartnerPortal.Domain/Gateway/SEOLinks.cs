using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    /// <summary>
    /// News
    /// </summary>
    public class SEOLinks
    {
        [Key]
        public int Id { get; set; }
        public string SEOImage { get; set; }
        public string SEOLink { get; set; }
        public string HoverImage { get; set; }
        public string LinkTitle { get; set; }
        public int? OrderBy { get; set; }
    }
}
