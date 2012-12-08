using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace BrainThud.Web.DependencyResolution
{
    public class StructureMapMvcResolver : IDependencyResolver 
    {
        private readonly IContainer container;

        public StructureMapMvcResolver(IContainer container) 
        {
            this.container = container;
        }

        public object GetService(Type serviceType) 
        {
            if (serviceType == null) return null;

            try 
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? this.container.TryGetInstance(serviceType)
                    : this.container.GetInstance(serviceType);
            }
            catch 
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) 
        {
            return this.container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}