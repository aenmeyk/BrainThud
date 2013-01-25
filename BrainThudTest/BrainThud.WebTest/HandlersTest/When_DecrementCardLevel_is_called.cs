using System;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_DecrementCardLevel_is_called : Given_a_new_QuizResultHandler
    {
        private readonly DateTime quizDate = TestValues.DATETIME;
        private Card card;

        public override void When()
        {
            this.card = new Card { EntityId = TestValues.CARD_ID, Level = CALENDAR_LEVEL + 1, QuizDate = quizDate };
            this.QuizResultHandler.DecrementCardLevel(this.card);
        }

        [Test]
        public void Then_the_card_level_should_be_reduced_by_one()
        {
            this.card.Level.Should().Be(CALENDAR_LEVEL);
        }

        [Test]
        public void Then_the_QuizDate_should_be_reduced_by_days_of_previous_level()
        {
            var expectedQuizDate = quizDate.AddDays(-this.UserConfiguration.QuizCalendar[CALENDAR_LEVEL]);
            this.card.QuizDate.Should().Be(expectedQuizDate);
        }
    }
}