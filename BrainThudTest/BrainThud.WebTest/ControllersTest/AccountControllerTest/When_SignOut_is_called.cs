using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.AccountControllerTest
{
    [TestFixture]
    public class When_SignOut_is_called : Given_a_new_AccountController
    {
        private ActionResult result;

        public override void When()
        {
            this.result = this.AccountController.SignOut();
        }

        [Test]
        public void Then_the_user_is_sent_to_the_Login_screen()
        {
            this.result.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("Login");
        }

        [Test]
        public void Then_the_user_is_signed_out()
        {
            this.AuthenticationHelper.Verify(x => x.SignOut(), Times.Once());
        }
    }
}