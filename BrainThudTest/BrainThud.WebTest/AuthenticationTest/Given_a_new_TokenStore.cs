using BrainThud.Web.Authentication;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest
{
    [TestFixture]
    public abstract class Given_a_new_TokenStore : Gwt
    {
        public override void Given()
        {
            this.TokenStore = new TokenStore();
        }

        protected TokenStore TokenStore { get; set; }
    }
}