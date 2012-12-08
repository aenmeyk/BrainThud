using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class QuizzesControllerFake : QuizzesController
    {
        public QuizzesControllerFake(ITableStorageContextFactory tableStorageContextFactory, IAuthenticationHelper authenticationHelper)
            : base(tableStorageContextFactory, authenticationHelper) { }

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