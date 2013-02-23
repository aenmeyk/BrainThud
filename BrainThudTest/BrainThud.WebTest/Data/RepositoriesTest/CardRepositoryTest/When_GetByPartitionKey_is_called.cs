using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_GetByPartitionKey_is_called : Given_a_new_CardRepository
    {
        private Card card;

        public override void When()
        {
            this.card = this.CardRepository.GetByPartitionKey(TestValues.CARD_PARTITION_KEY, TestValues.CARD_ID);
        }

        [Test]
        public void Then_only_the_card_matching_the_PartitionKey_and_the_CardId_should_be_returned()
        {
            this.card.PartitionKey.Should().Be(TestValues.CARD_PARTITION_KEY);
            this.card.RowKey.Should().Be(TestValues.CARD_ROW_KEY);
        }
    }
}