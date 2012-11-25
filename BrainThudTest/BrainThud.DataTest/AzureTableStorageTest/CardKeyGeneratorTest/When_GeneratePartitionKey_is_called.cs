using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.CardKeyGeneratorTest
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
        public void Then_the_user_RowKey_is_returned()
        {
            this.partitionKey.Should().Be(this.user.RowKey);
        }
    }
}