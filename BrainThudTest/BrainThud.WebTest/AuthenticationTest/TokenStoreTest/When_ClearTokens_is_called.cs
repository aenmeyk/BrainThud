using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest.TokenStoreTest
{
    [TestFixture]
    public class When_ClearTokens_is_called : Given_a_new_TokenStore
    {
        public override void When()
        {
            this.TokenStore.AddTokenCookie(TestValues.NAME_IDENTIFIER, TestValues.FED_AUTH_COOKIE_KEY, TestValues.FED_AUTH_COOKIE_VALUE);
            this.TokenStore.ClearTokens();
        }

        [Test]
        public void Then_no_tokens_should_remain_in_the_token_store()
        {
            var actualResult = this.TokenStore.GetAndDeleteToken(TestValues.NAME_IDENTIFIER);
            actualResult.Should().Be(string.Empty);
        }
    }
}