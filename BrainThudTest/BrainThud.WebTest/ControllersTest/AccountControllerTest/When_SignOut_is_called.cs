using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.AccountControllerTest
{
    [TestFixture]
    public class When_SignOut_is_called : Given_a_new_AccountController
    {
        private const string EXPECTED_SIGNOUT_URL = "http://www.brainthud.com/?wa=wsignout1.0";
        private ActionResult result;

        public override void When()
        {
            this.AuthenticationHelper.Setup(x => x.SignOut()).Returns(EXPECTED_SIGNOUT_URL);
            this.result = this.AccountController.SignOut();
        }

        [Test]
        public void Then_the_user_is_signed_out_and_sent_to_the_Login_screen()
        {
            ((RedirectResult)this.result).Url.Should().Be(EXPECTED_SIGNOUT_URL);
        }
    }
}