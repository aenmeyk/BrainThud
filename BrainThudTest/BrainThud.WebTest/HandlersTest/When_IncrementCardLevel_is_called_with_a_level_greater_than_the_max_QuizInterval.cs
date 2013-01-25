using System;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_IncrementCardLevel_is_called_with_a_level_greater_than_the_max_QuizInterval : Given_a_new_QuizResultHandler
    {
        private Card card;
        private int cardLevel;

        public override void When()
        {
            var quizResult = new QuizResult { IsCorrect = true };
            this.cardLevel = this.UserConfiguration.QuizCalendar.Count;
            this.card = new Card { Level = this.cardLevel };
            this.QuizResultHandler.IncrementCardLevel(quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_level_should_be_incremented()
        {
            this.card.Level.Should().Be(this.cardLevel + 1);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_the_next_day_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.UserConfiguration.QuizCalendar[cardLevel - 1]).Date;
            this.card.QuizDate.Should().Be(expectedQuizDate);
        }
    }
}