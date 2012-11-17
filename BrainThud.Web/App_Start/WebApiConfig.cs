using System.Web.Http;

namespace BrainThud.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: RouteNames.API_QUIZZES,
                routeTemplate: "api/{controller}/{year}/{month}/{day}",
                defaults: new { controller = "Quizzes" });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_DEFAULT,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}