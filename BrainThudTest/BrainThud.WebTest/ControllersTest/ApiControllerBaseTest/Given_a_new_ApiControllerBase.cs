using BrainThud.Web.Controllers;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.ApiControllerBaseTest
{
    [TestFixture]
    public abstract class Given_a_new_ApiControllerBase : Gwt
    {
        public override void Given()
        {
            this.ApiControllerBase = new ApiControllerBase();
        }

        protected ApiControllerBase ApiControllerBase { get; private set; }
    }
}