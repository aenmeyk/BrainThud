using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BrainThud.Core.AzureServices;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.CoreTest.AzureServicesTest
{
    [TestFixture]
    public class When_GetIdentityProviders_is_called : Given_a_new_AccessControlService
    {
        private IEnumerable<IdentityProvider> identityProviders;

        public override void When()
        {
            this.MessageHandler.ResponseGenerator = outerRequest =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(TestValues.IDENTITY_PROVIDER_RESPONSE);
                return response;
            };

            this.identityProviders = this.AccessControlService.GetIdentityProviders().Result;
        }

        [Test]
        public void Then_the_IdentityProviders_from_the_HttpClient_are_returned()
        {
            this.identityProviders.Count().Should().Be(4);
            this.identityProviders.Select(x => x.Name).Should().BeEquivalentTo(new[] {"Windows Live™ ID", "Google", "Yahoo!", "Facebook"});
        }

        [Test]
        public void Then_the_LoginUrls_should_be_returned_with_the_IdentityProviders()
        {
            this.identityProviders.Select(x => x.LoginUrl).Should().NotContainNulls();
        }

        [Test]
        public void Then_the_ImageUrls_should_be_returned_with_the_IdentityProviders()
        {
            this.identityProviders.Select(x => x.ImageUrl).Should().NotContainNulls();
        }


    }
}