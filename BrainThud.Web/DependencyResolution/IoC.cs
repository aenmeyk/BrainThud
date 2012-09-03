using System.Web.Mvc;
using BrainThud.Data;
using StructureMap;

namespace BrainThud.Web.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.Assembly(typeof(UnitOfWork).Assembly);
                                        scan.WithDefaultConventions();
                                    });
                            x.For<IControllerFactory>().Use<DefaultControllerFactory>();
                        });
            return ObjectFactory.Container;
        }
    }
}