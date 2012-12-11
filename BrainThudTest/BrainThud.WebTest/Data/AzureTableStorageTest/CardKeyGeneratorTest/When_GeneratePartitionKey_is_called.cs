using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GeneratePartitionKey_is_called : Given_a_new_CardKeyGenerator
    {
        private string partitionKey;

        public override void When()
        {
            this.partitionKey = this.CardKeyGenerator.GeneratePartitionKey();
        }

        [Test]
        public void Then_the_user_NameIdentifier_dash_UserId_is_returned()
        {
            this.partitionKey.Should().Be(TestValues.NAME_IDENTIFIER + "-" + USER_ID);
        }

        [Test]
        public void Then_the_GeneratedUserId_should_be_set()
        {
            this.CardKeyGenerator.GeneratedUserId.Should().Be(USER_ID);
        }
    }
}