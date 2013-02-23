using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GetPartitionKey_is_called_with_a_NameIdentifier_and_a_UserId : Given_a_new_CardKeyGenerator
    {
        private string partitionKey;

        public override void When()
        {
             this.partitionKey = this.CardKeyGenerator.GetPartitionKey(TestValues.STRING,TestValues.USER_ID);
        }

        [Test]
        public void Then_the_PartitionKey_should_be_the_NameIdentifier_dash_UserId()
        {
            this.partitionKey.Should().Be(TestValues.STRING + "-" + TestValues.USER_ID);
        }
    }
}