using BrainThud.Core.Models;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_AddCardToCardDeck_is_called_twice_for_the_same_DeckName : Given_a_new_CardDeckRepository
    {
        public override void When()
        {
            this.CardDeckRepository.AddCardToCardDeck(new Card { DeckName = TestValues.STRING });
            this.CardDeckRepository.AddCardToCardDeck(new Card { DeckName = TestValues.STRING });
        }

        [Test]
        public void Then_a_CardDeck_is_only_created_once()
        {
            this.TableStorageContext.Verify(x => x.AddObject(It.IsAny<TableServiceEntity>()), Times.Once());
        }
    }
}