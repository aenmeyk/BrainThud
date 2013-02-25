using BrainThud.Core;
using BrainThud.Web;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GetRowKey_is_called : Given_a_new_CardKeyGenerator
    {
        private string rowKey;

        public override void When()
        {
             this.rowKey = this.CardKeyGenerator.GetRowKey(TestValues.CARD_ID);
        }

        [Test]
        public void Then_the_RowKey_should_be_c_dash_CardId()
        {
            this.rowKey.Should().Be(CardRowTypes.CARD + "-" + TestValues.CARD_ID);
        }
    }
}