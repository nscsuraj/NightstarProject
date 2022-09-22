using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace PartnerPortal.Repository.AppRegistration.StructureMap
{
    public class RepositoryRegistry : Registry
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RepositoryRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
            For(typeof(IEFRepository<>)).Use(typeof(EFRepository<>));
        }
    }
}
