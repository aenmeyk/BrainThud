using BrainThud.Web.Model;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_GetById_is_called : Given_a_new_CardRepository
    {
        private Card card;

        public override void When()
        {
            this.card = this.CardRepository.GetById(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_only_the_card_matching_the_UserId_and_the_CardId_should_be_returned()
        {
            this.card.PartitionKey.Should().Be(TestValues.CARD_PARTITION_KEY);
            this.card.RowKey.Should().Be(TestValues.CARD_ROW_KEY);
        }
    }
}