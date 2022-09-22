using PartnerPortal.Business.AppRegistration.StructureMap;
using PartnerPortal.Repository.AppRegistration.StructureMap;
using StructureMap;

namespace PartnerPortal.AppRegistration.StructureMap
{
    /// <summary>
    ///     IoC Registrar Class Set up the container.
    /// </summary>
    /// <remarks>
    ///     Date            Developer       Description
    ///     07/04/2013      Dwarika         Created   
    /// </remarks>
    public class StructureMapIoCContainerRegistrar
    {
        /// <summary>
        /// Setup the main container.
        /// </summary>
        /// <returns></returns>

        public IContainer SetupAppContainer()
        {
            ObjectFactory.Configure(x =>
            {
                x.AddRegistry<RepositoryRegistry>();
                x.AddRegistry<ServiceRegistry>();
            });
            return ObjectFactory.Container;
        }
    }
}