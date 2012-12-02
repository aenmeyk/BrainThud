using BrainThud.Web.Controllers;
using BrainThud.Web.Data;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
using BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest;
using BrainThudTest.Tools;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class QuizResultsControllerFake : QuizResultsController
    {
        public QuizResultsControllerFake(IUnitOfWork unitOfWork, IQuizResultHandler quizResultHandler, IAuthenticationHelper authenticationHelper)
            : base(unitOfWork, quizResultHandler, authenticationHelper) { }

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