using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class CardsControllerFake : CardsController
    {
        public CardsControllerFake(
            ITableStorageContextFactory tableStorageContextFactory, 
            IAuthenticationHelper authenticationHelper,
            IUserHelper userHelper,
            IIdentityQueueManager identityQueueManager)
            : base(tableStorageContextFactory, authenticationHelper, userHelper, identityQueueManager) { }

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