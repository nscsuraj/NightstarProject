using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    /// <summary>
    /// News
    /// </summary>
    public class Videos
    {
        [Key]
        public int Id { get; set; }
        public string VideoThumbnailPath { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDescription { get; set; }
        public int? VideoType { get; set; }
        public string VideoPath { get; set; }
        public int? OrderBy { get; set; }
        public int? VideoCategory { get; set; }
    }
}
