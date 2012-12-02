using System.Web.Mvc;
using BrainThud.Web.Calendars;
using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
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
                    x.For<ITableStorageRepository<Configuration>>().Use<TableStorageRepository<Configuration>>();
                });

            return ObjectFactory.Container;
        }
    }
}