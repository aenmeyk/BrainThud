using BrainThud.Web.Api.Controllers;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.ApiControllerBaseTest
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