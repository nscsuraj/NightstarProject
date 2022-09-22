using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.SiteUtility
{
    public class SystemConfig
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}
