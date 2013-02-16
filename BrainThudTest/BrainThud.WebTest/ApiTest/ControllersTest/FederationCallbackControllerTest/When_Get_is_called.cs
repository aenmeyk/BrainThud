using System.Net;
using System.Net.Http;
using BrainThud.Web.Api.Controllers;
using BrainThudTest.Builders;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.FederationCallbackControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_FederationCallbackController
    {
        private HttpResponseMessage response;
        private FederationCallbackController controller;

        public override void When()
        {
            this.TokenStore.Setup(x => x.GetAndDeleteToken(TestValues.NAME_IDENTIFIER)).Returns(TestValues.FED_AUTH_TOKEN);

            this.controller = new ApiControllerBuilder<FederationCallbackController>(this.FederationCallbackController)
                .CreateRequest(this.Request)
                .Build();

            this.response = this.controller.Get(TestValues.NAME_IDENTIFIER);
        }

        [Test]
        public void Then_a_200_status_code_is_returned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Then_the_FedAuth_token_is_returned_in_the_response_content()
        {
            var content = this.response.Content.ReadAsStringAsync().Result;
            content.Should().Be(TestValues.FED_AUTH_TOKEN);
        }

        [Test]
        public void Then_the_token_is_retrieved_and_deleted_from_the_TokenStore()
        {
            this.TokenStore.Verify(x => x.GetAndDeleteToken(TestValues.NAME_IDENTIFIER), Times.Once());
        }
    }
}