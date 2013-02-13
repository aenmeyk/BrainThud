using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest.TokenStoreTest
{
    [TestFixture]
    public class When_GetAndDeleteToken_is_called : Given_a_new_TokenStore
    {
        private string result;

        public override void When()
        {
            this.TokenStore.AddTokenCookie(TestValues.NAME_IDENTIFIER, TestValues.FED_AUTH_COOKIE_KEY, TestValues.FED_AUTH_COOKIE_VALUE);
            this.result = this.TokenStore.GetAndDeleteToken(TestValues.NAME_IDENTIFIER);
        }

        [Test]
        public void Then_the_added_cookie_should_be_able_to_be_retrieved_from_the_TokenStore()
        {
            var expectedResult = string.Format("{0}={1};", TestValues.FED_AUTH_COOKIE_KEY, TestValues.FED_AUTH_COOKIE_VALUE);
            this.result.Should().Be(expectedResult);
        }
    }
}