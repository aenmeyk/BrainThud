using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_Update_is_called : Given_a_new_CardRepository
    {
        private readonly Card card = new Card { DeckName = "the deck name" };

        public override void When()
        {
            this.CardRepository.Update(this.card);
        }

        [Test]
        public void Then_the_DeckNameSlug_should_be_updated()
        {
            this.card.DeckNameSlug.Should().Be("the-deck-name");
        }
    }
}