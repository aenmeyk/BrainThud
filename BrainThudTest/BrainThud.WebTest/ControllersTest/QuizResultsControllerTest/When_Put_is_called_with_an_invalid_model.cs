using System.Net;
using System.Web.Http;
using BrainThud.Web.Model;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Put_is_called_with_an_invalid_model : Given_a_new_QuizResultsController
    {
        public override void When()
        {
            this.QuizResultsController.ModelState.AddModelError("Error Key", "Error Message");
            this.QuizResultsController.Put(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID, 
                new QuizResult());
        }

        [Test]
        public void Then_an_exception_should_be_thrown_and_400_should_be_returned_as_the_status_code()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest));
        }
    }
}