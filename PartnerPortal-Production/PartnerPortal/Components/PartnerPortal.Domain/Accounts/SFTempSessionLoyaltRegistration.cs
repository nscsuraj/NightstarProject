using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionLoyaltRegistration
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Int64 Id { get; set; }

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HowToSupportStar { get; set; }

        public string ProductsThatSupport { get; set; }
        public string HowToSupportStarOther { get; set; }
        public string ProductsThatSell { get; set; }
        public string DoesSellHardware { get; set; }
        public string HardwareSource { get; set; }
        public bool AgreedToNewsLetter { get; set; }
        public bool TermsAgreed { get; set; }

    }
}
