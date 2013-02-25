using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_RemoveFromCardDeck_is_called_and_no_cards_remain_in_the_deck : Given_a_new_CardDeckRepository
    {
        private readonly Card card = new Card { UserId = TestValues.USER_ID, DeckName = TestValues.DECK_NAME };

        public override void When()
        {
            this.CardDeckRepository.RemoveCardFromCardDeck(card);
        }

        [Test]
        public void Then_the_card_deck_should_be_deleted()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(It.Is<CardDeck>(y => y.DeckName == TestValues.DECK_NAME )), Times.Once());
        }
    }
}