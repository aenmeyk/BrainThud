using BrainThud.Web.Authentication;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest.TokenStoreTest
{
    [TestFixture]
    public abstract class Given_a_new_TokenStore : Gwt
    {
        public override void Given()
        {
            this.TokenStore = new TokenStore();
            this.TokenStore.ClearTokens();
        }

        protected TokenStore TokenStore { get; set; }
    }
}