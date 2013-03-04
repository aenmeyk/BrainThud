using System.Net;
using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsBatchControllerTest
{
    [TestFixture]
    public class When_Put_is_called_with_an_invalid_model : Given_a_new_CardsBatchController
    {
        public override void When()
        {
            this.CardsBatchController.ModelState.AddModelError("Error Key", "Error Message");
            this.CardsBatchController.Put(null);
        }

        [Test]
        public void Then_the_returned_status_code_should_be_400()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest));
        }
    }
}