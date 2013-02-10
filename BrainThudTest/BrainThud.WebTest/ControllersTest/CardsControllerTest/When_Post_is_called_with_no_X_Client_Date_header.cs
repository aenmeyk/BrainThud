using System.Net;
using System.Web.Http;
using BrainThud.Core.Models;
using BrainThud.Web.Resources;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Post_is_called_with_no_X_Client_Date_header : Given_a_new_CardsController
    {
        public override void When()
        {
            this.CardsController.Post(new Card());
        }

        [Test]
        public void Then_the_returned_status_code_should_be_400()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest));
        }

        [Test]
        public void Then_the_exception_message_should_be_included_in_the_response()
        {
            this.TestException<HttpResponseException>(x =>
            {
                var responseMessage = x.Response.Content.ReadAsStringAsync().Result;
                responseMessage.Should().Be(ErrorMessages.X_Client_Date_header_field_is_required);
            });
        }
    }
}