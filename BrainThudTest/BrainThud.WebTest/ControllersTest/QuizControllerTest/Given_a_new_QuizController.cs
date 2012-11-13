using BrainThud.Data;
using BrainThud.Web.Controllers;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizController : Gwt
    {
        public override void Given()
        {
            this.UnitOfWork = new Mock<IUnitOfWork>();
            this.QuizController = new QuizController(this.UnitOfWork.Object);
        }

        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected QuizController QuizController { get; private set; }
    }
}