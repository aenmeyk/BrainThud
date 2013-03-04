using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using BrainThud.Core;

namespace BrainThud.Web.Api
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: RouteNames.API_QUIZ_CARDS,
                routeTemplate: "api/cards/{userid}/{year}/{month}/{day}",
                defaults: new { controller = "Cards" });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_CARDS,
                routeTemplate: "api/cards/{userid}/{cardid}",
                defaults: new { controller = "Cards" });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_CARDS_BATCH,
                routeTemplate: "api/cards/{userid}",
                defaults: new { controller = "CardsBatch" });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_CARD_DECKS,
                routeTemplate: "api/card-decks/{userid}",
                defaults: new { controller = "CardDecks", userid = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
            name: RouteNames.API_CARD_DECK_CARDS,
            routeTemplate: "api/card-decks/{userid}/{deckNameSlug}",
            defaults: new { controller = "Cards", action = "GetForCardDeck" });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_QUIZ_RESULTS,
                routeTemplate: "api/quiz-results/{userid}/{year}/{month}/{day}/{cardId}",
                defaults: new { controller = "QuizResults", cardId = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: RouteNames.API_DEFAULT,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}