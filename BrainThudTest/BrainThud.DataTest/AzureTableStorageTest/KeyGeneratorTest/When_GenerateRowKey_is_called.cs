using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.KeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called : Given_a_new_TableStorageKeyGenerator
    {
        private string rowKey;

        public override void When()
        {
            this.rowKey = this.TableStorageKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_generated_key_should_not_be_empty()
        {
            this.rowKey.Should().NotBeEmpty();
        }
    }
}