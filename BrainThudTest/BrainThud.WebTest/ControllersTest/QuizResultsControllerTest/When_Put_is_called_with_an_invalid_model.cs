using System.Net;
using System.Net.Http;
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
            this.QuizResultsController.Put(TestValues.USER_ID, 2012, 1, 1, new QuizResult());
        }

        [Test]
        public void Then_an_exception_should_be_thrown_and_400_should_be_returned_as_the_status_code()
        {
            HttpResponseException exception = null;

            try
            {
                this.ThrowUnhandledExceptions();
            }
            catch (HttpResponseException e)
            {
                exception = e;
            }
            finally
            {
                exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }
    }
}