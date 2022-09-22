using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFAccounts
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
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string AccountEmail { get; set; }
        public string PartnerNumber { get; set; }
        public string AccountExpert { get; set; }
        public string AccountExpertPhone { get; set; }
        public string AccountExpertEmail { get; set; }
        public string SupportExpert { get; set; }
        public string SupportExpertPhone { get; set; }
        public string SupportExpertEmail { get; set; }
        public string PartnerProgramStatus { get; set; }
        public string PartnerType { get; set; }
        public string LoyaltyLevel { get; set; }
        public string DiscountRate { get; set; }
        public string Revenue6Month { get; set; }
        public string Revenue12Month { get; set; }
        public string Rate612 { get; set; }
        public string AccountPassword { get; set; }
        public string AccountShortName { get; set; }
        public string Region { get; set; }
        public string DistributorDiscount { get; set; }
        public string SuggestedMarkup { get; set; }
        public bool IsActive { get; set; }

        public bool? IsStockingPartner { get; set; }

        public Guid? ResetPasswordToken { get; set; }
    }
}
