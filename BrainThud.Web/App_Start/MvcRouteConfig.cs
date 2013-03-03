using System.Web.Mvc;
using System.Web.Routing;
using BrainThud.Core;

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
                name: RouteNames.SITEMAP,
                url: "sitemap.xml",
                defaults: new { controller = "Sitemap", action = "Feed" }
            );

            routes.MapRoute(
                name: RouteNames.DECK,
                url: "card-decks/{userId}/{deckNameSlug}",
                defaults: new { controller = "Cards", action = "Deck" }
            );

            routes.MapRoute(
                name: RouteNames.CARD,
                url: "cards/{userId}/{cardId}/{cardSlug}",
                defaults: new { controller = "Cards", action = "Card" }
            );

            routes.MapRoute(
                name: RouteNames.DEFAULT,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}