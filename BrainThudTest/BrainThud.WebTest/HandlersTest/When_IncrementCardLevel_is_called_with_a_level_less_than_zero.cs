using System;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_IncrementCardLevel_is_called_with_a_level_less_than_zero : Given_a_new_QuizResultHandler
    {
        private Card card;

        public override void When()
        {
            var quizResult = new QuizResult { IsCorrect = true };
            this.card = new Card { Level = -10 };
            this.QuizResultHandler.IncrementCardLevel(quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_level_should_be_zero()
        {
            this.card.Level.Should().Be(0);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_level_zero_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.UserConfiguration.QuizCalendar[0]).Date;
            this.card.QuizDate.Should().Be(expectedQuizDate);
        }
    }
}