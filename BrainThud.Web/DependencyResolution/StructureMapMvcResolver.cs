using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace BrainThud.Web.DependencyResolution
{
    public class StructureMapMvcResolver : IDependencyResolver {

        private readonly IContainer _container;

        public StructureMapMvcResolver(IContainer container) {
            this._container = container;
        }

        public object GetService(Type serviceType) {
            if (serviceType == null) return null;
            try {
                  return serviceType.IsAbstract || serviceType.IsInterface
                           ? this._container.TryGetInstance(serviceType)
                           : this._container.GetInstance(serviceType);
            }
            catch {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return this._container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}