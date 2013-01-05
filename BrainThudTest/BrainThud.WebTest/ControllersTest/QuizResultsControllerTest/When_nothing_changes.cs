using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_QuizResultsController
    {
        [Test]
        public void Then_the_controller_should_derive_from_ApiController()
        {
            this.QuizResultsController.Should().BeAssignableTo<ApiController>();
        }
    }
}