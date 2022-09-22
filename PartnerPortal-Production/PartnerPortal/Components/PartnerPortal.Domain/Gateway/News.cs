using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    /// <summary>
    /// News
    /// </summary>
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string NewsThumbnailPath { get; set; }
        public string NewsTitle { get; set; }
        public int NewsType { get; set; }
        public string NewsContent { get; set; }
        public int? HeaderVideoOrImage { get; set; }
        public int? FooterVideoOrImage { get; set; }
        public int? HeaderVideoType { get; set; }
        public int? FooterVideoType { get; set; }
        public string HeaderVideoPath { get; set; }
        public string FooterVideoPath { get; set; }
        public int? OrderBy { get; set; }
        public string ImgAltText { get; set; }
    }
}
