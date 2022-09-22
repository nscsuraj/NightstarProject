using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionMdfData
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
        public string MdfContact { get; set; }
        public string MdfContactEmail { get; set; }
        public string MdfId { get; set; }
        public string MdfName { get; set; }

        public string MdfAwardYear { get; set; }
        public string MdfAwardQuarter { get; set; }
        public string MdfAwardPriorYear { get; set; }
        public string MdfAwardCurrentYear { get; set; }
        public string MdfAwardPercent { get; set; }
        public string MdfAwardDollar { get; set; }
        public string MdfAwardTotalClaimed { get; set; }
        public string MdfAwardBalance { get; set; }
        public string MdfTotalAmount { get; set; }
        public string MdfPaymentStatus { get; set; }
    }
}
