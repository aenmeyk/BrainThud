using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public class When_UpdateCardAndRelations_is_called : Given_a_new_TableStorageContext_for_the_Card_entity
    {
        private readonly Card originalCard = new Card { DeckName = TestValues.DECK_NAME };
        private Card card;

        public override void When()
        {
            this.card = new Card { PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = TestValues.CARD_ROW_KEY };
            this.CardRepository.Setup(x => x.Get(TestValues.CARD_PARTITION_KEY, TestValues.CARD_ROW_KEY)).Returns(this.originalCard);
            this.TableStorageContext.UpdateCardAndRelations(this.card);
        }

        [Test]
        public void Then_Update_should_be_called_on_the_card_repository()
        {
            this.CardRepository.Verify(x => x.Update(this.card), Times.Once());
        }

        [Test]
        public void Then_the_original_card_deck_name_is_removed()
        {
            this.CardDeckRepository.Verify(x => x.RemoveCardFromCardDeck(this.originalCard), Times.Once());
        }

        [Test]
        public void Then_the_new_card_deck_name_is_added()
        {
            this.CardDeckRepository.Verify(x => x.AddCardToCardDeck(this.card), Times.Once());
        }
    }
}