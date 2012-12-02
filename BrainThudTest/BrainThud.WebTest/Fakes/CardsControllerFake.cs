using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using BrainThudTest.Tools;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class CardsControllerFake : CardsController
    {
        public CardsControllerFake(
            ITableStorageContextFactory tableStorageContextFactory, 
            IAuthenticationHelper authenticationHelper)
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