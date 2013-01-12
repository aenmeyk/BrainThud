using System;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_IncrementCardLevel_is_called_with_an_incorrect_result : Given_a_new_QuizResultHandler
    {
        private const int CARD_LEVEL = 3;
        private Card card;

        public override void When()
        {
            var quizResult = new QuizResult { IsCorrect = false };
            this.card = new Card { Level = CARD_LEVEL };
            this.QuizCalendar.Setup(x => x[0]).Returns(1);
            this.QuizResultHandler.IncrementCardLevel(quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_level_should_be_set_to_zero()
        {
            this.card.Level.Should().Be(0);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_level_zero_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.QuizCalendar.Object[0]).Date;
            this.card.QuizDate.Should().Be(expectedQuizDate);
        }
    }
}