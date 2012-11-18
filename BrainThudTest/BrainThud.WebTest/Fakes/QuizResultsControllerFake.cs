using BrainThud.Data;
using BrainThud.Web.Controllers;
using BrainThud.Web.Handlers;
using BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest;
using BrainThudTest.Tools;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class QuizResultsControllerFake : QuizResultsController
    {
        public QuizResultsControllerFake(IUnitOfWork unitOfWork, IQuizResultHandler quizResultHandler) 
            : base(unitOfWork, quizResultHandler) { }

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