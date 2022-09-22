using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class MdfAdded
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
        public string MdfNo { get; set; }

        public string MdfPerQuarter { get; set; }
        public string TotalMdfAvailable { get; set; }
        public string TotalMdfPending { get; set; }
        public string TotalMdfUsed { get; set; }
        public string Amount { get; set; }
        public string ActivityDescription { get; set; }
        public string ProjectStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string CreatedDate { get; set; }
        public string MDFQuarterOne { get; set; }
        public string MDFQuarterTwo { get; set; }
        public string MDFQuarterThree { get; set; }
        public string MDFQuarterFour { get; set; }
    }
}
