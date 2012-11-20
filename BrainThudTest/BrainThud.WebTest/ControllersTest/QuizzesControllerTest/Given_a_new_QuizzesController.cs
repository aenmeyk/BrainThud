using BrainThud.Data;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizzesController : Gwt
    {
        public override void Given()
        {
            this.UnitOfWork = new Mock<IUnitOfWork>();
            this.QuizzesController = new QuizzesControllerFake(this.UnitOfWork.Object);
        }

        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected QuizzesControllerFake QuizzesController { get; private set; }
    }
}