using System.Net;
using System.Net.Http;
using BrainThud.Web.Controllers;
using BrainThudTest.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.FederationCallbackControllerTest
{
    [TestFixture]
    public class When_Post_is_called : Given_a_new_FederationCallbackController
    {
        private HttpResponseMessage response;
        private FederationCallbackController controller;

        public override void When()
        {
            this.controller = new ApiControllerBuilder<FederationCallbackController>(this.FederationCallbackController)
                .CreateRequest(this.Request)
                .AddCookie(TestValues.FED_AUTH_COOKIE_KEY, TestValues.FED_AUTH_COOKIE_VALUE)
                .AddCookie(TestValues.FED_AUTH_1_COOKIE_KEY, TestValues.FED_AUTH_1_COOKIE_VALUE)
                .Build();

            response = this.controller.Post();
        }

        [Test]
        public void Then_a_302_status_code_is_returned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        }

        [Test]
        public void Then_the_NameIdentifier_is_returned_in_the_Location_header()
        {
            this.response.Headers.Location.OriginalString.Should().EndWith(TestValues.NAME_IDENTIFIER);
        }

        [Test]
        public void Then_the_FedAuth_cookies_are_added_to_the_TokenStore()
        {
            this.TokenStore.Verify(x => x.AddTokenCookie(TestValues.NAME_IDENTIFIER, TestValues.FED_AUTH_COOKIE_KEY, TestValues.FED_AUTH_COOKIE_VALUE));
            this.TokenStore.Verify(x => x.AddTokenCookie(TestValues.NAME_IDENTIFIER, TestValues.FED_AUTH_1_COOKIE_KEY, TestValues.FED_AUTH_1_COOKIE_VALUE));
        }
    }
}