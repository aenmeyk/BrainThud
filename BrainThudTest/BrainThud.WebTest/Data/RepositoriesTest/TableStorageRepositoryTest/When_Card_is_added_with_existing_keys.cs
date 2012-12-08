using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Card_is_added_with_existing_keys : Given_a_new_TableStorageRepository_of_Card
    {
        private readonly Card card = new Card { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY };

        public override void When()
        {
            this.TableStorageRepository.Add(this.card, new Mock<ITableStorageKeyGenerator>().Object);
        }

        [Test]
        public void Then_the_PartitionKey_should_not_change()
        {
            this.card.PartitionKey.Should().Be(TestValues.PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_should_not_change()
        {
            this.card.RowKey.Should().Be(TestValues.ROW_KEY);
        }
    }
}