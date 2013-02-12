using System.Net.Http;
using BrainThud.Web.Authentication;
using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class QuizResultsControllerFake : QuizResultsController
    {
        public QuizResultsControllerFake( 
            ITableStorageContextFactory tableStorageContextFactory, 
            IQuizResultHandler quizResultHandler,
            IAuthenticationHelper authenticationHelper)
            : base(tableStorageContextFactory, quizResultHandler, authenticationHelper) { }

        public string RouteName { get; set; }
        public object RouteValues { get; set; }

        public override string GetLink(string routeName, object routeValues)
        {
            this.RouteValues = routeValues;
            this.RouteName = routeName;
            return TestUrls.LOCALHOST;
        }
    }
}