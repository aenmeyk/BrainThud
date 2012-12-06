using BrainThud.Web.Controllers;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.HomeControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_HomeController : Gwt
    {
        public override void Given()
        {
            this.HomeController = new HomeController();
        }

        protected HomeController HomeController { get; private set; }
    }
}