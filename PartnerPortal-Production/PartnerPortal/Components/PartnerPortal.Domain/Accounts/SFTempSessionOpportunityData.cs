using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionOpportunityData
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
        public string OpportunityId { get; set; }
        public string OpportunityName { get; set; }
        public string Description { get; set; }
        public string OpportunityType { get; set; }
        public double? TotalOpportunityQuantity { get; set; }
        public double? TotalProductQuantity { get; set; }
        public double? Amount { get; set; }
        public string StageName { get; set; }
        public string CloseDate { get; set; }
        public bool? IsClosed { get; set; }
        public string CreatedDate { get; set; }

        public string Distributor { get; set; }
        public string ProductInterest { get; set; }
        public string ProductQuantity { get; set; }
        public string CompetitiveProducts { get; set; }
        public string Software { get; set; }
        public string RollOutBulkBuy { get; set; }
        public string LaunchDate { get; set; }
        public string DemoProductRequested { get; set; }
        public string SpecialPricingRequested { get; set; }
        public string EndUser { get; set; }
        public string BusinessCase { get; set; }

        public string Status { get; set; }
        public string SupportRequest { get; set; }
        public string StarExpert { get; set; }
        public string StarExpertEmail { get; set; }
        public string StarUpdate { get; set; }
        public string CancelledDate { get; set; }
    }
}
