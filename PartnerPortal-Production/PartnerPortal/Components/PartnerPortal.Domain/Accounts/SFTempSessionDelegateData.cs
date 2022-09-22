using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionDelegateData
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the session key.
        /// </summary>
        /// <value>
        /// The session key.
        /// </value>
        public string SessionKey { get; set; }
        public string AccountId { get; set; }
        public string PartnerNumber { get; set; }
        public string AccountName { get; set; }
        public string TechnologyPartnerEPN { get; set; }
        public string DelegatePartnerEPN { get; set; }
        public string TechnologyPartnerAccountName { get; set; }
        public string DelegatePartnerAccountName { get; set; }

        public string DPN { get; set; }
    }
}
