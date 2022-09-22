using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class PortalItemMaster
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
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ProductClass { get; set; }
        public string ProductSubClass { get; set; }
        public string RepClass { get; set; }
        public string ListPrice { get; set; }
        public string MAP { get; set; }
        public string Discount { get; set; }
        public string DiscountedPrice { get; set; }
        public string SalesDescription { get; set; }

        public string BindingPart { get; set; }
        public string CSpecialOrder { get; set; }
        public string VirtualPN { get; set; }
        public string UPC { get; set; }
        public string UAmazon { get; set; }
        public string ValidUSCA { get; set; }
        public string ValidLA { get; set; }

    }
}
