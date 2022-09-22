using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Import
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string FrozenFor { get; set; }
        public string ProductClass { get; set; }
        public string ProductSubClass { get; set; }
        public string PurchaseItem { get; set; }
        public string SellItem { get; set; }
        public string InventoryItem { get; set; }
        public string InspectionFlag { get; set; }
        public string CountryOfOrigin { get; set; }
        public string DiscontinueDate { get; set; }
        public string CreateDate { get; set; }
        public string SalesDescription { get; set; }
        public string SparePartFor { get; set; }
        public string PalletQty { get; set; }
        public string PSIExclude { get; set; }
        public string CustomerSpecific { get; set; }
        public string CSpecialOrder { get; set; }
        public string UPC { get; set; }
        public string Exclusive { get; set; }
        public string PricelistExclude { get; set; }
        public string BindingPart { get; set; }
        public string NonStandard { get; set; }
        public string VirtualPN { get; set; }
        public string UWebsite { get; set; }
    }
}
