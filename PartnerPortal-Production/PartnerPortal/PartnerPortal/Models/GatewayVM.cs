
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Utility;

namespace PartnerPortal.Models
{
    public class GatewayVM
    {
        public GatewayVM()
        {
            MetaTags = new List<MetaTags>();
            Identity = new CurrentIdentity();
        }
        public IList<MetaTags> MetaTags { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string LoginPassword { get; set; }
        [Required]
        public string ConfirmLoginPassword { get; set; }

        public string PageTitle { get; set; }
        public bool RememberMe { get; set; }
        /// <summary>
        /// Gets or sets the login failed.
        /// </summary>
        /// <value>
        /// The login failed.
        /// </value>
        public string LoginFailed { get; set; }
        public CurrentIdentity Identity { get; set; }
    }
}
