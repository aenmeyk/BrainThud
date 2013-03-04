using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public class When_UpdateCardAndRelations_is_called : Given_a_new_TableStorageContext_for_the_Card_entity
    {
        private Card card;

        public override void When()
        {
            this.card = new Card();
            this.TableStorageContext.UpdateCardAndRelations(this.card);
        }

//        [Test]
//        public void Then_Update_should_be_called_on_the_card_repository()
//        {
//            this.TableStorageContext.Verify(x => x.Cards.Update(this.updatedCard), Times.Once());
//        }
//
//        [Test]
//        public void Then_the_original_card_deck_name_is_removed()
//        {
//            this.TableStorageContext.Verify(x => x.CardDecks.RemoveCardFromCardDeck(this.originalCard));
//        }
//
//        [Test]
//        public void Then_the_original_card_should_be_detached()
//        {
//            this.TableStorageContext.Verify(x => x.Detach(this.originalCard), Times.Once());
//        }
//
//        [Test]
//        public void Then_the_new_card_deck_name_is_added()
//        {
//            this.TableStorageContext.Verify(x => x.CardDecks.AddCardToCardDeck(this.updatedCard));
//        }
    }
}