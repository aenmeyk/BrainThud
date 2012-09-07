using BrainThud.Data;
using BrainThud.Model;
using BrainThud.Web.Controllers;
using BrainThudTest.Tools;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class NuggetsControllerFake : NuggetsController
    {
        public NuggetsControllerFake(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        public string RouteName { get; set; }
        public object RouteValues { get; set; }

        public override string GetLink(string routeName, object routeValues)
        {
            this.RouteValues = routeValues;
            this.RouteName = routeName;
            return TestValues.LOCALHOST;
        }
    }
}