using System.Web.Http;

namespace BrainThud.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: RouteNames.API_QUIZZES,
                routeTemplate: "api/quizzes/{userid}/{year}/{month}/{day}",
                defaults: new { controller = "Quizzes" });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_QUIZ_RESULTS,
                routeTemplate: "api/quizzes/{userid}/{year}/{month}/{day}/results/{quizResultId}",
                defaults: new { controller = "QuizResults", quizResultId = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_CARDS,
                routeTemplate: "api/cards/{userid}/{cardid}",
                defaults: new
                {
                    controller = "Cards", 
                    userid = RouteParameter.Optional,
                    cardid = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_DEFAULT,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}