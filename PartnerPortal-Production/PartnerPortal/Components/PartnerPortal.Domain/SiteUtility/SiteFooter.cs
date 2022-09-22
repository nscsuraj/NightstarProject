using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.SiteUtility
{
    public class SiteFooter
    {
        [Key]
        public int Id { get; set; }

        public string FooterCms { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public string MinimumHeight { get; set; }

    }
}
