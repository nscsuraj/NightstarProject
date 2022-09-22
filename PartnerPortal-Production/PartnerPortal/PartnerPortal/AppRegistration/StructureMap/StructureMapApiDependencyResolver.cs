using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace PartnerPortal.AppRegistration.StructureMap
{
    /// <summary>
    ///     Structure Map dependency resolver for Web API calls.
    /// </summary>
    /// <remarks>
    ///     Date            Developer       Description
    ///     07/04/2013      Dwarika         Created   
    /// </remarks>
    public class StructureMapApiDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initialzize the dependence resolver with the Structure Map container.
        /// </summary>
        /// <param name="container">StructureMap IContainer object.</param>
        public StructureMapApiDependencyResolver(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Get the the service of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>An instance of the service.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }

            return _container.GetInstance(serviceType);
        }

        /// <summary>
        /// Get a collection of services of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>A collection of services matching the specified type.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>()
                .Where(s => s.GetType() == serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.GetNestedContainer();
            return new StructureMapApiDependencyResolver(child);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}