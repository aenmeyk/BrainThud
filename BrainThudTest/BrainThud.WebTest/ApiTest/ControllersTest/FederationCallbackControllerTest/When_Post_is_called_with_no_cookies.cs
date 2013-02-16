using System.Net;
using System.Net.Http;
using BrainThud.Web.Api.Controllers;
using BrainThudTest.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.FederationCallbackControllerTest
{
    [TestFixture]
    public class When_Post_is_called_with_no_cookies : Given_a_new_FederationCallbackController
    {
        private HttpResponseMessage response;
        private FederationCallbackController controller;

        public override void When()
        {
            this.controller = new ApiControllerBuilder<FederationCallbackController>(this.FederationCallbackController)
                .CreateRequest(this.Request)
                .Build();

            this.response = this.controller.Post();
        }

        [Test]
        public void Then_a_400_status_code_is_returned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}