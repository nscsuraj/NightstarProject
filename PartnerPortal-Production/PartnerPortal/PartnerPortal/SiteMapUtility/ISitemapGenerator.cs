using System.Collections.Generic;
using System.Xml.Linq;

namespace PartnerPortal.SiteMapUtility
{
    public interface ISitemapGenerator
    {
        XDocument GenerateSiteMap(IEnumerable<ISitemapItem> items);
    }
}