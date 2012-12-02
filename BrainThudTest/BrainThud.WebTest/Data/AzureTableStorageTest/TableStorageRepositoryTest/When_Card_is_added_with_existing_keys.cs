using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Card_is_added_with_existing_keys : Given_a_new_TableStorageRepository_of_Card
    {
        private readonly Card card = new Card { PartitionKey = PARTITION_KEY, RowKey = ROW_KEY };

        public override void When()
        {
            this.TableStorageRepository.Add(this.card);
        }

        [Test]
        public void Then_the_PartitionKey_should_not_change()
        {
            this.card.PartitionKey.Should().Be(PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_should_not_change()
        {
            this.card.RowKey.Should().Be(ROW_KEY);
        }
    }
}