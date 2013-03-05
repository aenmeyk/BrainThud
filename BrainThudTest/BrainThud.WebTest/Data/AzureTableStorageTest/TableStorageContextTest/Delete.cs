using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public class When_DeleteCardAndRelations_is_called : Given_a_new_TableStorageContext_for_the_Card_entity
    {
        private readonly Card card = new Card { UserId = TestValues.USER_ID, EntityId = TestValues.CARD_ID };

        public override void When()
        {
            this.TableStorageContext.DeleteCardAndRelations(this.card);
        }

        [Test]
        public void Then_the_card_should_be_deleted()
        {
            this.CardRepository.Verify(x => x.DeleteById(this.card.UserId, this.card.EntityId), Times.Once());
        }

        [Test]
        public void Then_the_associated_QuizResults_are_deleted()
        {
            this.QuizResultsRepository.Verify(x => x.DeleteByCardId(this.card.EntityId), Times.Once());
        }

        [Test]
        public void Then_the_card_is_removed_from_the_CardDeck()
        {
            this.CardDeckRepository.Verify(x => x.RemoveCardFromCardDeck(this.card));
        }
    }
}