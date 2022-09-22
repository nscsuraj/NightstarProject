using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    public class FeaturedStarProductDetails
    {
        [Key]
        public int Id { get; set; }
        public int FSPDId { get; set; }
        public string BackgroundImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public int OrderBy { get; set; }
    }
}
