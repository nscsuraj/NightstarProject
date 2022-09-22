using System.Web.Http;
using PartnerPortal.Core.Attributes;

namespace PartnerPortal.Controllers
{
    [AuthorizeCms]
    public class BaseApiController : ApiController
    {
       protected bool DynamicListHasElement(dynamic items)
        {
            if (items == null) return false;
            foreach (var item in items)
            {
                return true;
            }
            return false;
        }
    }
}