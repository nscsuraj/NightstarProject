using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    public class OurIntegrations
    {
        [Key]
        public int Id { get; set; }
        public string CMSContent { get; set; }
        public string Title { get; set; }
        public int OrderBy { get; set; }
    }
}
