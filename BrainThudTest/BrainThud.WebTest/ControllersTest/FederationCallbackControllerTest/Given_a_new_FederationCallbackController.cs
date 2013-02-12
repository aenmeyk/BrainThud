using System.Net.Http;
using BrainThud.Web.Authentication;
using BrainThud.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.FederationCallbackControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_FederationCallbackController : Gwt
    {
        public override void Given()
        {
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);
            this.CookieStore = new Mock<ICookieStore>();
            this.Request = new HttpRequestMessage(HttpMethod.Post, TestUrls.FEDERATION_CALLBACK);
            this.FederationCallbackController = new FederationCallbackController(authenticationHelper.Object, this.CookieStore.Object);
        }

        protected Mock<ICookieStore> CookieStore { get; private set; }
        protected HttpRequestMessage Request { get; private set; }
        protected FederationCallbackController FederationCallbackController { get; private set; }
    }
}