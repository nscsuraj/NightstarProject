using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PartnerPortal.Domain.Import
{
    public class ImportedMarketingMaterial
    {
        public ImportedMarketingMaterial()
        {
            Images = new List<ImportedMarketingMaterialImages>();
            Categories = new List<ImportedMarketingMaterialCategories>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductTagline { get; set; }
        public string PermALink { get; set; }
        public string Description { get; set; }
        public int CatalogVisibility { get; set; }
        public double Price { get; set; }
        public string DataSheetUrl { get; set; }
        public string ActualDataSheetUrl { get; set; }
        public string DataSheetDownloadLink { get; set; }
        public string VideoUrl { get; set; }
        public string ImageDownloadLink { get; set; }
        public string DownloadAllLink { get; set; }

        [NotMapped]
        public IList<ImportedMarketingMaterialImages> Images { get; set; }
        [NotMapped]
        public IList<ImportedMarketingMaterialCategories> Categories { get; set; }
    }
}
