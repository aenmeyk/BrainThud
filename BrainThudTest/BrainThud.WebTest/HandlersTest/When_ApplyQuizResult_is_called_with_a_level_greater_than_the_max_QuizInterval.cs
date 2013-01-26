using System;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ApplyQuizResult_is_called_with_a_level_greater_than_the_max_QuizInterval : Given_a_new_QuizResultHandler
    {
        private Card card;
        private int cardLevel;
        private QuizResult quizResult;

        public override void When()
        {
            this.quizResult = new QuizResult { IsCorrect = true };
            this.cardLevel = this.UserConfiguration.QuizCalendar.Count;
            this.card = new Card { Level = this.cardLevel, QuizDate = TestValues.CARD_QUIZ_DATE };
            this.QuizResultHandler.ApplyQuizResult(this.quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_level_should_be_incremented()
        {
            this.card.Level.Should().Be(this.cardLevel + 1);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_the_next_day_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.UserConfiguration.QuizCalendar[cardLevel - 1]);
            this.card.QuizDate.Should().BeWithin(10.Seconds()).Before(expectedQuizDate);
        }

        [Test]
        public void Then_the_QuizResult_CardQuizDate_should_be_set_to_the_card_QuizDate_before_the_QuizResult_was_applied()
        {
            this.quizResult.CardQuizDate.Should().Be(TestValues.CARD_QUIZ_DATE);
        }

        [Test]
        public void Then_the_QuizResult_CardLevel_should_be_set_to_the_card_level_before_it_was_incremented()
        {
            this.quizResult.CardLevel.Should().Be(this.cardLevel);
        }
    }
}