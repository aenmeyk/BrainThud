using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Put_is_called_and_the_CardDeck_has_not_changed : Given_a_new_CardsController
    {
        private readonly Card originalCard = new Card { DeckName = TestValues.DECK_NAME };
        private readonly Card updatedCard = new Card { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY, DeckName = TestValues.DECK_NAME };

        public override void When()
        {
            this.TableStorageContext
                .Setup(x => x.Cards.Get(this.updatedCard.PartitionKey, this.updatedCard.RowKey))
                .Returns(this.originalCard);

            this.CardsController.Put(this.updatedCard);
        }

        [Test]
        public void Then_the_OriginalCard_is_not_removed_from_the_CardDeck()
        {
            this.TableStorageContext.Verify(x => x.CardDecks.RemoveCardFromCardDeck(originalCard), Times.Never());
        }

        [Test]
        public void Then_the_Card_is_not_added_to_the_CardDeck()
        {
            this.TableStorageContext.Verify(x => x.CardDecks.AddCardToCardDeck(updatedCard), Times.Never());
        }
    }
}