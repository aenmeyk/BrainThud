using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetsControllerTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_NuggetController
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_the_NuggetController_should_inherit_from_ApiController()
        {
            this.NuggetsController.Should().BeAssignableTo<ApiController>();
        }
    }
}