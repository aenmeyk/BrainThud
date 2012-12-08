using System.Net;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Delete_is_called_with_an_invalid_model : Given_a_new_QuizResultsController
    {
        private HttpResponseMessage response;

        public override void When()
        {
            this.QuizResultsController.ModelState.AddModelError("Error Key", "Error Message");
            this.response = this.QuizResultsController.Delete(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_the_returned_status_code_should_be_400()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}