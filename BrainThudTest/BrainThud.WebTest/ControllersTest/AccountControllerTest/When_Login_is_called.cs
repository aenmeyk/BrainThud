using System.Net;
using System.Web.Mvc;
using BrainThud.Core;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.AccountControllerTest
{
    [TestFixture]
    public class When_Login_is_called : Given_a_new_AccountController
    {
        private ActionResult result;

        public override void When()
        {
            this.result = this.AccountController.Login();
        }

        [Test]
        public void Then_the_user_is_sent_to_the_Login_screen()
        {
            this.result.As<ViewResult>().ViewName.Should().Be("Login");
        }

        [Test]
        public void Then_the_MetaDataScript_is_set()
        {
            ((string)this.AccountController.ViewBag.MetaDataScript).Should().BeOneOf(string.Format(Urls.IDENTITY_PROVIDERS, WebUtility.UrlEncode(Urls.LOCALHOST)), string.Format(Urls.IDENTITY_PROVIDERS, WebUtility.UrlEncode(Urls.BRAINTHUD)));
        }
    }
}