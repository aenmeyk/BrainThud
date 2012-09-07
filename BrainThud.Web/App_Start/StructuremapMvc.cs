using System.Web.Http;
using System.Web.Mvc;
using BrainThud.Web.App_Start;
using BrainThud.Web.DependencyResolution;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]

namespace BrainThud.Web.App_Start
{
    public static class StructuremapMvc
    {
        public static void Start()
        {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapMvcResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiResolver(container);
        }
    }
}