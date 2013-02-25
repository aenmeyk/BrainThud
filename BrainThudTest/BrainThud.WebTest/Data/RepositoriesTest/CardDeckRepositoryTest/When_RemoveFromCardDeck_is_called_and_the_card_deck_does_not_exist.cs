using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_RemoveFromCardDeck_is_called_and_the_card_deck_does_not_exist : Given_a_new_CardDeckRepository
    {
        private readonly Card card = new Card { DeckName = "Nonexistant card deck" };

        public override void When()
        {
            this.CardDeckRepository.RemoveCardFromCardDeck(card);
        }

        [Test]
        public void Then_an_exception_should_not_be_thrown()
        {
            // An exception should not be thrown
        }
    }
}