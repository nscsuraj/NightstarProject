using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace PartnerPortal.AppRegistration.StructureMap
{
    /// <summary>
    ///     Structure Map implementation of the IDependencyResolver interface.
    /// </summary>
    /// <remarks>
    ///     Date            Developer       Description
    ///     07/04/2013      Dwarika         Created   
    /// </remarks>
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _Container;

        /// <summary>
        /// Initialzize the dependence resolver with the Structure Map container.
        /// </summary>
        /// <param name="container">StructureMap IContainer object.</param>
        public StructureMapDependencyResolver(IContainer container)
        {
            _Container = container;
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
                return _Container.TryGetInstance(serviceType);
            }

            return _Container.GetInstance(serviceType);
        }

        /// <summary>
        /// Get a collection of services of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>A collection of services matching the specified type.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetAllInstances<object>()
                .Where(s => s.GetType() == serviceType);
        }
    }
}