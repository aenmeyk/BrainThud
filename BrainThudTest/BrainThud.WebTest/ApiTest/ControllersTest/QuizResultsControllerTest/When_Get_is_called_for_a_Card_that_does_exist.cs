using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_for_a_Card_that_does_exist : Given_a_new_QuizResultsController
    {
        private QuizResult quizResult;

        public override void When()
        {
            var quizResults = Builder<QuizResult>.CreateListOfSize(3)
                .All().With(x => x.UserId = TestValues.USER_ID)
                .Random(1).With(x => x.CardId = TestValues.CARD_ID)
                .Build();

            this.TableStorageContext
                .Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(quizResults.AsQueryable());

            this.quizResult = this.QuizResultsController.Get(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID);
        }

        [Test]
        public void Then_the_QuizResult_for_that_user_for_that_card_for_that_day_should_be_returned()
        {
            this.quizResult.CardId.Should().Be(TestValues.CARD_ID);
            this.quizResult.UserId.Should().Be(TestValues.USER_ID);
        }
    }
}