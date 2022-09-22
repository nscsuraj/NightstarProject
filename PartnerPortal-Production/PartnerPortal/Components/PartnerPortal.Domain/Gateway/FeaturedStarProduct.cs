using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    public class FeaturedStarProduct
    {
        [Key]
        public int Id { get; set; }
        public string FSPTitle { get; set; }
        public int OrderBy { get; set; }
        public bool IsActive { get; set; }
    }
}
