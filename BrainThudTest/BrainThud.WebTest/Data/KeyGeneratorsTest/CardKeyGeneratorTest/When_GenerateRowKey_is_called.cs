using BrainThud.Web;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called : Given_a_new_CardKeyGenerator
    {
        private string rowKey;

        public override void When()
        {
            this.rowKey = this.CardKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_CardId_should_be_c_dash_NextIdentityValue()
        {
            this.rowKey.Should().Be(CardRowTypes.CARD + "-" + NEXT_IDENTITY_VALUE);
        }

        [Test]
        public void Then_the_GeneratedEntityId_should_be_set()
        {
            this.CardKeyGenerator.GeneratedEntityId.Should().Be(NEXT_IDENTITY_VALUE);
        }
    }
}