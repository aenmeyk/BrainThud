using System;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ReverseIfExists_is_called_with_existing_QuizResult : Given_a_new_QuizResultHandler
    {
        private readonly DateTime quizDate = TestValues.DATETIME;
        private Card card;
        private Mock<ITableStorageContext> tableStorageContext;
        private QuizResult quizResult;

        public override void When()
        {
            this.quizResult = new QuizResult { QuizDate = TestValues.DATETIME };
            this.card = new Card { EntityId = TestValues.CARD_ID, Level = CALENDAR_LEVEL + 1, QuizDate = quizDate };
            this.tableStorageContext = new Mock<ITableStorageContext>();

            var existingQuizResult = new QuizResult
            {
                PartitionKey = TestValues.PARTITION_KEY,
                RowKey = TestValues.ROW_KEY,
                CardId = TestValues.CARD_ID
            };

            this.tableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(new[] { existingQuizResult }.AsQueryable());

            this.QuizResultHandler.ReverseIfExists(this.tableStorageContext.Object, this.quizResult, this.card);
        }

        [Test]
        public void Then_the_card_level_should_be_reduced_by_one()
        {
            this.card.Level.Should().Be(CALENDAR_LEVEL);
        }

        [Test]
        public void Then_the_QuizDate_should_be_reduced_by_days_of_previous_level()
        {
            var expectedQuizDate = quizDate.AddDays(-CALENDAR_DAYS);
            this.card.QuizDate.Should().Be(expectedQuizDate);
        }

        [Test]
        public void Then_the_existing_QuizResult_is_deleted()
        {
            this.tableStorageContext.Verify(x => x.QuizResults.Delete(TestValues.PARTITION_KEY, TestValues.ROW_KEY), Times.Once());
        }
    }
}