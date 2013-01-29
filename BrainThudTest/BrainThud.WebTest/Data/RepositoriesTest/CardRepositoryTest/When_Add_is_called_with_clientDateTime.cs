using System;
using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called_with_clientDateTime : Given_a_new_CardRepository
    {
        private readonly Card card = new Card { DeckName = "the deck name" };

        public override void When()
        {
            this.QuizCalendar.Setup(x => x[0]).Returns(TestValues.INT);
            this.CardRepository.Add(card, TestValues.DATETIME);
        }

        [Test]
        public void Then_the_QuizDate_should_be_set_from_the_first_entry_in_the_QuizCalendar()
        {
            var expectedDate = TestValues.DATETIME.AddDays(TestValues.INT);
            this.card.QuizDate.Should().Be(expectedDate);
        }
    }
}