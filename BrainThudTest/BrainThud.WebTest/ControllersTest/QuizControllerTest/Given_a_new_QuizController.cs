using BrainThud.Web.Controllers;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizController : Gwt
    {
        public override void Given()
        {
            this.QuizController = new QuizController();
        }

        protected QuizController QuizController { get; private set; }
    }
}