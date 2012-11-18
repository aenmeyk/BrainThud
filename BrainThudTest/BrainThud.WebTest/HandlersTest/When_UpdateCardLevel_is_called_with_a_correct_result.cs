using System;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_UpdateCardLevel_is_called_with_a_correct_result : Given_a_new_QuizResultHandler
    {
        private const int CARD_LEVEL = 3;
        private Card card;

        public override void When()
        {
            var quizResult = new QuizResult { IsCorrect = true };
            this.card = new Card { Level = CARD_LEVEL };
            this.QuizCalendar.Setup(x => x[CARD_LEVEL + 1]).Returns(90);
            this.QuizResultHandler.UpdateCardLevel(quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_level_should_be_incremented()
        {
            this.card.Level.Should().Be(CARD_LEVEL + 1);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_the_next_day_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.QuizCalendar.Object[CARD_LEVEL + 1]).Date;
            this.card.QuizDate.Should().Be(expectedQuizDate);
        }
    }
}