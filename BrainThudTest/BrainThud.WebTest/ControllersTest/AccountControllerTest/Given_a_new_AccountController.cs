using BrainThud.Web.Controllers;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.AccountControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_AccountController : Gwt
    {
        public override void Given()
        {
            this.AuthenticationHelper = new Mock<IAuthenticationHelper>();
            this.AccountController = new AccountController(this.AuthenticationHelper.Object);
        }

        protected Mock<IAuthenticationHelper> AuthenticationHelper { get; private set; }
        protected AccountController AccountController { get; private set; }
    }
}