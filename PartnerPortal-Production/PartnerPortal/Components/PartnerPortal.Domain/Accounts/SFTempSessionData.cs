using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionData
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
        public String SessionKey { get; set; }
        public String AccountId { get; set; }
        public String AccountName { get; set; }
        public String PartnerType { get; set; }
        public String AccountType { get; set; }
        public String PartnerNumber { get; set; }
        public String AccountExpert { get; set; }
        public String SupportExpert { get; set; }
        public String DiscountRate { get; set; }   
        public string LoyaltyLevel { get; set; }
    }
}
