using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace PartnerPortal.Business.AppRegistration.StructureMap
{
    public class ServiceRegistry : Registry
    {
        /// <summary>
        ///     Initializes a new instance of the class.
        /// </summary>
        public ServiceRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                x.AddAllTypesOf<IService>();
            });
        }
    }
}
