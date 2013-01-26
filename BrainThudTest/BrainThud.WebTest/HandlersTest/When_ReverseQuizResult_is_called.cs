using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ReverseQuizResult_is_called : Given_a_new_QuizResultHandler
    {
        private readonly Card card = new Card();
        
        
        public override void When()
        {
            var quizResult = new QuizResult { IsCorrect = true, CardQuizDate = TestValues.CARD_QUIZ_DATE, CardLevel = TestValues.CARD_QUIZ_LEVEL };
            this.QuizResultHandler.ReverseQuizResult(quizResult, this.card);
        }

        [Test]
        public void Then_the_Card_level_should_be_set_to_the_card_level_of_the_QuizResult()
        {
            this.card.Level.Should().Be(TestValues.CARD_QUIZ_LEVEL);
        }

        [Test]
        public void Then_the_card_QuizDate_is_set_to_the_QuizResult_CardQuizDate()
        {
            this.card.QuizDate.Should().Be(TestValues.CARD_QUIZ_DATE);
        }
    }
}