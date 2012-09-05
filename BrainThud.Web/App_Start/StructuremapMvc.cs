using System.Web.Http;
using System.Web.Mvc;
using BrainThud.Web.DependencyResolution;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(BrainThud.Web.App_Start.StructuremapMvc), "Start")]

namespace BrainThud.Web.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
			IContainer container = IoC.Initialize();
//            DependencyResolver.SetResolver(new StructureMapWebApiResolver(container));
            DependencyResolver.SetResolver(new StructureMapMvcResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiResolver(container);
        }
    }
}