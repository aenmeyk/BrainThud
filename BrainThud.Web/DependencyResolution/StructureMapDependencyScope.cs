using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace BrainThud.Web.DependencyResolution
{
    public class StructureMapDependencyScope : ServiceLocatorImplBase, IDependencyScope
    {
        protected readonly IContainer Container;

        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            this.Container = container;
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }

        public new object GetService(Type serviceType)
        {
            if (serviceType == null) return null;

            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? this.Container.TryGetInstance(serviceType)
                    : this.Container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Container.GetAllInstances(serviceType).Cast<object>();
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return this.Container.GetAllInstances(serviceType).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (string.IsNullOrEmpty(key)) return this.Container.GetInstance(serviceType);
            return this.Container.GetInstance(serviceType, key);
        }
    }
}