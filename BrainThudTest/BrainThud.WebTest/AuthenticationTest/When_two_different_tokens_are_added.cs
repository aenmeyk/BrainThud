using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest
{
    [TestFixture]
    public class When_two_different_tokens_are_added : Given_a_new_TokenStore
    {
        public override void When()
        {
            this.TokenStore.AddTokenCookie(TestValues.NAME_IDENTIFIER, TestValues.FED_AUTH_COOKIE_KEY, TestValues.FED_AUTH_COOKIE_VALUE);
            this.TokenStore.AddTokenCookie(TestValues.NAME_IDENTIFIER, TestValues.FED_AUTH_1_COOKIE_KEY, TestValues.FED_AUTH_1_COOKIE_VALUE);
        }

        [Test]
        public void Then_the_added_cookie_should_be_able_to_be_retrieved_from_the_TokenStore()
        {
            var actualResult = this.TokenStore.GetAndDeleteToken(TestValues.NAME_IDENTIFIER);
            actualResult.Should().Be(TestValues.FED_AUTH_TOKEN);
        }
    }
}