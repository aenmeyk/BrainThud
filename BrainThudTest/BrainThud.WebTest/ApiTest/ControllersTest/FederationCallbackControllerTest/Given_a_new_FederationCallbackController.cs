using System.Net.Http;
using BrainThud.Web.Api.Controllers;
using BrainThud.Web.Authentication;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.FederationCallbackControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_FederationCallbackController : Gwt
    {
        public override void Given()
        {
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);
            this.TokenStore = new Mock<ITokenStore>();
            this.Request = new HttpRequestMessage(HttpMethod.Post, TestUrls.FEDERATION_CALLBACK);
            this.FederationCallbackController = new FederationCallbackController(authenticationHelper.Object, this.TokenStore.Object);
        }

        protected Mock<ITokenStore> TokenStore { get; private set; }
        protected HttpRequestMessage Request { get; private set; }
        protected FederationCallbackController FederationCallbackController { get; private set; }
    }
}