using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_AddCardToCardDeck_is_called_for_card_deck_that_exists : Given_a_new_CardDeckRepository
    {
        private readonly Card card = new Card { EntityId = TestValues.CARD_ID, UserId = TestValues.USER_ID, DeckName = TestValues.DECK_NAME };
            const string CARD_IDS = "123";

        public override void When()
        {
            this.CardDeck.CardIds = CARD_IDS;
            this.CardDeckRepository.AddCardToCardDeck(this.card);
        }

        [Test]
        public void Then_a_new_CardDeck_is_not_added()
        {
            this.TableStorageContext.Verify(x => x.AddObject(It.IsAny<CardDeck>()), Times.Never());
        }

        [Test]
        public void Then_the_EntityId_is_added_to_the_list_of_CardIds()
        {
            this.CardDeck.CardIds.Should().Be(CARD_IDS + "," + this.card.EntityId.ToString());
        }

        [Test]
        public void Then_the_CardDeck_is_updated()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.CardDeck), Times.Once());
        }
    }
}