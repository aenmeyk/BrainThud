using BrainThud.Core.Models;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsBatchControllerTest
{
    [TestFixture]
    public class When_Put_is_called_and_the_CardDeck_has_not_changed : Given_a_new_CardsBatchController
    {
        private readonly Card originalCard = new Card { DeckName = TestValues.DECK_NAME };

        public override void When()
        {
            var cards = Builder<Card>.CreateListOfSize(10)
                .All().With(x => x.DeckName = TestValues.DECK_NAME)
                .Build();

            this.TableStorageContext
                .Setup(x => x.Cards.Get(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(this.originalCard);

            this.CardsBatchController.Put(cards);
        }

        [Test]
        public void Then_the_OriginalCard_is_not_removed_from_the_CardDeck()
        {
            this.TableStorageContext.Verify(x => x.CardDecks.RemoveCardFromCardDeck(this.originalCard), Times.Never());
        }

        [Test]
        public void Then_the_Card_is_not_added_to_the_CardDeck()
        {
            this.TableStorageContext.Verify(x => x.CardDecks.AddCardToCardDeck(It.IsAny<Card>()), Times.Never());
        }
    }
}