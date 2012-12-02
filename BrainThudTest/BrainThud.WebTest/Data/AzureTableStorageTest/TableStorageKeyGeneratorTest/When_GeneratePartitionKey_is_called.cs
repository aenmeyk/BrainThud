using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageKeyGeneratorTest
{
    [TestFixture]
    public class When_GeneratePartitionKey_is_called : Given_a_new_TableStorageKeyGenerator
    {
        private string partitionKey;

        public override void When()
        {
            this.partitionKey = this.TableStorageKeyGenerator.GeneratePartitionKey();
        }

        [Test]
        public void Then_the_generated_key_should_not_be_empty()
        {
            this.partitionKey.Should().NotBeEmpty();
        }
    }
}