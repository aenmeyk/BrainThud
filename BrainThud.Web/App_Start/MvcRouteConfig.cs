using System.Web.Mvc;
using System.Web.Routing;

namespace BrainThud.Web.App_Start
{
    public class MvcRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: RouteNames.HOME,
                url: "home",
                defaults: new { controller = "Home", action = "Home" }
            );

            routes.MapRoute(
                name: RouteNames.DECK,
                url: "library/{userId}/{deckNameSlug}",
                defaults: new { controller = "Library", action = "Deck" }
            );

            routes.MapRoute(
                name: RouteNames.CARD,
                url: "flashcard/{userId}/{cardId}/{cardSlug}",
                defaults: new { controller = "Library", action = "Card" }
            );

            routes.MapRoute(
                name: RouteNames.DEFAULT,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}