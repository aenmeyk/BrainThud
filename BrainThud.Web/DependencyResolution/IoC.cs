using System.Web.Mvc;
using BrainThud.Web.Calendars;
using BrainThud.Web.Data.KeyGenerators;
using StructureMap;

namespace BrainThud.Web.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.Scan(scan =>
                        {
                            scan.TheCallingAssembly();
                            scan.WithDefaultConventions();
                        });

                    x.For<IControllerFactory>().Use<DefaultControllerFactory>();
                    x.For<IQuizCalendar>().Use<DefaultQuizCalendar>();
                    x.For<ITableStorageKeyGenerator>().Use<CardKeyGenerator>();
                });

            return ObjectFactory.Container;
        }
    }
}