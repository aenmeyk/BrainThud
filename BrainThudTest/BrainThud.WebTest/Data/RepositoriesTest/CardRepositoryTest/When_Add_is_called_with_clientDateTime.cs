using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called_with_clientDateTime : Given_a_new_CardRepository
    {
        private readonly Card card = new Card { DeckName = "the deck name", Question = "the question"};

        public override void When()
        {
            this.CardRepository.Add(this.card, TestValues.DATETIME);
        }

        [Test]
        public void Then_the_QuizDate_should_be_set()
        {
            this.card.QuizDate.Should().Be(TestValues.DATETIME);
        }

        [Test]
        public void Then_the_DeckNameSlug_should_be_set()
        {
            this.card.DeckNameSlug.Should().Be("the-deck-name");
        }

        [Test]
        public void Then_the_CardSlug_should_be_set()
        {
            this.card.CardSlug.Should().Be("the-question");
        }
    }
}