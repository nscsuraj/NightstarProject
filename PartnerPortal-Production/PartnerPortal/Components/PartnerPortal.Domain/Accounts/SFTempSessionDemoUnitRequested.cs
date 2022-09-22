using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class SFTempSessionDemoUnitRequested
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
        public string CaseType { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }

        public string ProductInterface { get; set; }
        public double Quantity { get; set; }
    }
}
