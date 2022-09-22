using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        public string ImagePath { get; set; }
        public string MobileImagePath { get; set; }
        public string MobileLandscapeImagePath { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string ButtonText { get; set; }

        public string ButtonLink { get; set; }

        public int Type { get; set; }

        public int? OrderBy { get; set; }

        public string ImgAltText { get; set; }
    }
}
