using System;
using System.Linq;
using BrainThud.Web;
using BrainThud.Web.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.QuizResultsRepositoryTest
{
    [TestFixture]
    public class When_GetForQuiz_is_called : Given_a_new_QuizResultsRepository
    {
        private IQueryable<QuizResult> result;

        public override void When()
        {
            var quizDate = new DateTime(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);

            // Use IsCorrect = true to indicate which cards should be included in result
            var allQuizResults = Builder<QuizResult>.CreateListOfSize(10)
                .All().With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY).And(x => x.RowKey = CardRowTypes.QUIZ_RESULT + "-x")
                .TheFirst(5).With(x => x.QuizDate = quizDate).And(x => x.IsCorrect = true)
                .TheNext(5).With(x => x.QuizDate = new DateTime(1990, 1, 1)).And(x => x.IsCorrect = false)
                .TheFirst(2).With(x => x.PartitionKey = TestValues.PARTITION_KEY)
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<QuizResult>()).Returns(allQuizResults.AsQueryable);
            this.result = this.QuizResultsRepository.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);
        }

        [Test]
        public void Then_only_QuizResults_for_the_QuizDate_should_be_returned()
        {
            this.result.Should().OnlyContain(x => x.IsCorrect);
        }

        [Test]
        public void Then_only_QuizResults_for_this_user_should_be_returned()
        {
            this.result.Should().OnlyContain(x => x.PartitionKey == TestValues.CARD_PARTITION_KEY);
        }
    }
}