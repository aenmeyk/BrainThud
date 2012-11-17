using System.Web.Http;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_QuizzesController
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_QuizzesController_should_derive_from_ApiController()
        {
            this.QuizzesController.Should().BeAssignableTo<ApiController>();
        }
    }
}