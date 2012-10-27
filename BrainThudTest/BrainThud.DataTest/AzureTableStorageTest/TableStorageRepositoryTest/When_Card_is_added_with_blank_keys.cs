using BrainThud.Model;
using NUnit.Framework;
using FluentAssertions;
using BrainThud.Data;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Card_is_added_with_blank_keys : Given_a_new_TableStorageRepository_of_Card
    {
        private readonly Card card = new Card();

        public override void When()
        {
            this.TableStorageRepository.Add(this.card);
        }

        [Test]
        public void Then_AddObject_is_called_on_the_TableServiceContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.card));
        }

        [Test]
        public void Then_the_PartitionKey_is_set_from_the_TableStorageKeyGenerator()
        {
            this.card.PartitionKey.Should().Be(PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_is_set_from_the_TableStorageKeyGenerator()
        {
            this.card.RowKey.Should().Be(ROW_KEY);
        }
    }
}