using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Gateway
{
    public class SliderTopic
    {
        [Key]
        public int Id { get; set; }

        public int SliderId { get; set; }

        public string Topic { get; set; }

        public string TopicLink { get; set; }
    }
}
