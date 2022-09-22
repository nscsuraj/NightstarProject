using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PartnerPortal.Domain.Import
{
    public class ImportedMarketingMaterialCategories
    {
        [Key]
        public int Id { get; set; }
        public int MarketingMaterialID { get; set; }
        public string CategoryName { get; set; }
    }
}
