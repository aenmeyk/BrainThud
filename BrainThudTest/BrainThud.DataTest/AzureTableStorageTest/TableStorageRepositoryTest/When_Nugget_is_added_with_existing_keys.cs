using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Nugget_is_added_with_existing_keys : Given_a_new_TableStorageRepository_of_Nugget
    {
        private readonly Nugget nugget = new Nugget { PartitionKey = PARTITION_KEY, RowKey = ROW_KEY };

        public override void When()
        {
            this.TableStorageRepository.Add(this.nugget);
        }

        [Test]
        public void Then_the_PartitionKey_should_not_change()
        {
            this.nugget.PartitionKey.Should().Be(PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_should_not_change()
        {
            this.nugget.RowKey.Should().Be(ROW_KEY);
        }
    }
}