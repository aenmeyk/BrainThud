using System.Net;
using System.Net.Http;
using BrainThud.Web.Model;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Post_is_called_with_an_invalid_model : Given_a_new_QuizResultsController
    {
        private HttpResponseMessage response;

        public override void When()
        {
            this.QuizResultsController.ModelState.AddModelError("Error Key", "Error Message");
            this.response = this.QuizResultsController.Post(TestValues.USER_ID, 2012, 1, 1, new QuizResult());
        }

        [Test]
        public void Then_the_returned_status_code_should_be_400()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}