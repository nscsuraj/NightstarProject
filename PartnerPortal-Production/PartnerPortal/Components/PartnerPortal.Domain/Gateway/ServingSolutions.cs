using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    public class ServingSolutions
    {
        [Key]
        public int Id { get; set; }
        public string TitleText { get; set; }
        public string ImagePath { get; set; }
        public string TitleTextColor { get; set; }
        public string TitleTextHoverColor { get; set; }
        public string HoverImagePath { get; set; }
        public string LinkUrl { get; set; }
        public int OrderBy { get; set; }
        public string ImgAltText { get; set; }
    }
}
