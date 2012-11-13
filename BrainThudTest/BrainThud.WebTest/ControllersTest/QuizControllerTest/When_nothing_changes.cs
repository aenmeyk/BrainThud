using System.Web.Http;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizControllerTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_QuizController
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_QuizController_should_derive_from_ApiController()
        {
            this.QuizController.Should().BeAssignableTo<ApiController>();
        }
    }
}