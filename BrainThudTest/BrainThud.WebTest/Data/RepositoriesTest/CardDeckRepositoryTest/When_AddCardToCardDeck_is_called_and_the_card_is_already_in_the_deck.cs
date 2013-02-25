using BrainThud.Core.Models;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_AddCardToCardDeck_is_called_and_the_card_is_already_in_the_deck : Given_a_new_CardDeckRepository
    {
        private readonly Card card = new Card { EntityId = TestValues.CARD_ID, UserId = TestValues.USER_ID, DeckName = TestValues.DECK_NAME };

        public override void When()
        {
            this.CardDeck.CardIds = TestValues.CARD_ID.ToString();
            this.CardDeckRepository.AddCardToCardDeck(this.card);
        }

        [Test]
        public void Then_the_EntityId_should_not_be_appended_to_the_CardIds_property()
        {
            this.CardDeck.CardIds.Should().Be(TestValues.CARD_ID.ToString());
        }
    }
}