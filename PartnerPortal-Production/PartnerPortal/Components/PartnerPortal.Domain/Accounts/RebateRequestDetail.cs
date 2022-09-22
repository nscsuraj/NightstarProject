using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class RebateRequestDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public long RequestId { get; set; }

        public string PartNumber { get; set; }
        public string ItemName { get; set; }
        public string DelegateRebate { get; set; }
        public double Quantity { get; set; }
        public string Distributor { get; set; }
    }
}
