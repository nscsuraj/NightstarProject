using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PartnerPortal.Domain.Import
{
    public class ImportedMarketingMaterialImages
    {
        [Key]
        public int Id { get; set; }
        public int MarketingMaterialID { get; set; }
        public string ImageName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ImageSrc { get; set; }

    }
}
