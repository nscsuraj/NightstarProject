using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionPurchaseByProductClasses
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
        public string ProductClass { get; set; }
        public string SixMonthsSale { get; set; }
        public string TwelveMonthsSale { get; set; }
        public string TwentyFourMonthsSale { get; set; }
        public string GrowthRate { get; set; }
    }
}
