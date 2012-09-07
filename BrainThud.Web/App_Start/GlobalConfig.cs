using System.Web.Http;
using BrainThud.Web.ActionFilters;
using Newtonsoft.Json.Serialization;

namespace BrainThud.Web.App_Start
{
    public static class GlobalConfig
    {
        public static void CustomizeConfig(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Filters.Add(new ValidationActionFilter());
        }
    }
}