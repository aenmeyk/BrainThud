using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ReverseQuizResult_is_called : Given_a_new_QuizResultHandler
    {
        private readonly Card card = new Card();
        private QuizResult quizResult;

        public override void When()
        {
            this.quizResult = new QuizResult
            {
                IsCorrect = true, 
                CardQuizDate = TestValues.CARD_QUIZ_DATE,
                CardLevel = TestValues.CARD_QUIZ_LEVEL,
                CardIsCorrect = true,
                CardCompletedQuizYear = TestValues.YEAR,
                CardCompletedQuizMonth = TestValues.MONTH,
                CardCompletedQuizDay = TestValues.DAY
            };

            this.QuizResultHandler.ReverseQuizResult(this.quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_Level_should_be_set_to_the_card_level_of_the_QuizResult()
        {
            this.card.Level.Should().Be(quizResult.CardLevel);
        }

        [Test]
        public void Then_the_card_QuizDate_is_set_to_the_QuizResult_CardQuizDate()
        {
            this.card.QuizDate.Should().Be(quizResult.CardQuizDate);
        }

        [Test]
        public void Then_the_Card_IsCorrect_should_be_set_to_the_CardIsCorrect_of_the_QuizResult()
        {
            this.card.IsCorrect.Should().Be(quizResult.CardIsCorrect);
        }

        [Test]
        public void Then_the_Card_completed_quiz_date_is_set_to_the_QuizResult_CompletedQuizDate()
        {
            this.card.CompletedQuizYear.Should().Be(TestValues.YEAR);
            this.card.CompletedQuizMonth.Should().Be(TestValues.MONTH);
            this.card.CompletedQuizDay.Should().Be(TestValues.DAY);
        }
    }
}