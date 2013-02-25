using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_RemoveFromCardDeck_is_called_and_cards_remain_in_the_deck : Given_a_new_CardDeckRepository
    {
        private readonly Card card = new Card { EntityId = TestValues.CARD_ID, UserId = TestValues.USER_ID, DeckName = TestValues.DECK_NAME };

        public override void When()
        {
            this.CardDeck.CardIds = TestValues.CARD_ID + ",123,456";
            this.CardDeckRepository.RemoveCardFromCardDeck(card);
        }

        [Test]
        public void Then_the_CardId_should_be_removed_from_the_list_of_CardIds()
        {
            this.CardDeck.CardIds.Should().Be("123,456");
        }

        [Test]
        public void Then_the_CardDeck_should_be_udpated()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.CardDeck), Times.Once());
        }

        [Test]
        public void Then_the_card_deck_should_not_be_deleted()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(It.IsAny<CardDeck>()), Times.Never());
        }
    }
}