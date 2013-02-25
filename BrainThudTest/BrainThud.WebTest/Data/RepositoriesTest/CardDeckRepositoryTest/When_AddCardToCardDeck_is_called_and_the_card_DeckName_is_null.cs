using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_AddCardToCardDeck_is_called_and_the_card_DeckName_is_null : Given_a_new_CardDeckRepository
    {
        public override void When()
        {
            this.CardDeckRepository.AddCardToCardDeck(new Card());
        }

        [Test]
        public void Then_the_CardDeck_should_not_be_added_or_updated()
        {
            this.TableStorageContext.Verify(x => x.AddObject(It.IsAny<CardDeck>()), Times.Never());
            this.TableStorageContext.Verify(x => x.UpdateObject(It.IsAny<CardDeck>()), Times.Never());
        }
    }
}