using BrainThud.Data;
using BrainThud.Web.Controllers;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_NuggetController : Gwt
    {
        public override void Given()
        {
            this.UnitOfWork = new Mock<IUnitOfWork>();
            this.NuggetController = new NuggetController(this.UnitOfWork.Object);
        }

        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected NuggetController NuggetController { get; private set; }
    }
}