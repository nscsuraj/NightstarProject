using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionPurchaseByDistributors
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
        public string ResellerAccountId { get; set; }
        public string ResellerAccountName { get; set; }
        public string ResellerDiscount { get; set; }
        public string ResellerPartnerType { get; set; }
        public string ResellerPartnerNumber { get; set; }
        public string CustomerName { get; set; }
        public string DistributorShortName { get; set; }
        public string SixMonthsSale { get; set; }
        public string TwelveMonthsSale { get; set; }
        public string TwentyFourMonthsSale { get; set; }
        public string SuggestedMarkup { get; set; }
        public string DistributorDiscount { get; set; }
    }
}
