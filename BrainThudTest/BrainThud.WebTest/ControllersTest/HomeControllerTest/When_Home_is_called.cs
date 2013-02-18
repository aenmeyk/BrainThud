using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.HomeControllerTest
{
    [TestFixture]
    public class When_Home_is_called : Given_a_new_HomeController
    {
        private ActionResult result;

        public override void When()
        {
            this.result = this.HomeController.Home();
        }

        [Test]
        public void Then_the_home_view_should_be_returned()
        {
            ((ViewResult)this.result).ViewName.Should().Be("home");
        }
    }
}