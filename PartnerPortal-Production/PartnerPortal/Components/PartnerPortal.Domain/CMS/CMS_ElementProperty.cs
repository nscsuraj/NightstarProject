
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.CMS
{
    public class CMS_ElementProperty
    {
        [Key]
        public int Id { get; set; }
        public int ElementId { get; set; }
        public string ElementProperty { get; set; }
        public string PropertyType { get; set; }
        public string RenderString { get; set; }
        public string RenderIfPropertyValue { get; set; }
        public bool? AddPX { get; set; }
        public bool? IgnoreRendering { get; set; }

    }
}
