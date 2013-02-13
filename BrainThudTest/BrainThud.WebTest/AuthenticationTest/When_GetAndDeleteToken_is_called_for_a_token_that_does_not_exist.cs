using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.AuthenticationTest
{
    [TestFixture]
    public class When_GetAndDeleteToken_is_called_for_a_token_that_does_not_exist : Given_a_new_TokenStore
    {
        private string result;

        public override void When()
        {
            this.result = this.TokenStore.GetAndDeleteToken(TestValues.NAME_IDENTIFIER);
        }

        [Test]
        public void Then_an_empty_string_is_returned()
        {
            this.result.Should().Be(string.Empty);
        }
    }
}