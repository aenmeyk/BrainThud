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
    public class When_ReverseIfExists_is_called_with_existing_QuizResult_and_card_is_level_0 : Given_a_new_QuizResultHandler
    {
        private readonly DateTime quizDate = TestValues.DATETIME;
        private Card card;
        private Mock<ITableStorageContext> tableStorageContext;
        private QuizResult quizResult;

        public override void When()
        {
            this.quizResult = new QuizResult { QuizDate = TestValues.DATETIME };
            this.card = new Card { EntityId = TestValues.CARD_ID, Level = 0, QuizDate = quizDate };
            this.tableStorageContext = new Mock<ITableStorageContext>();
            var existingQuizResult = new QuizResult{ CardId = TestValues.CARD_ID };

            this.tableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(new[] { existingQuizResult }.AsQueryable());

            this.QuizResultHandler.ReverseIfExists(this.tableStorageContext.Object, this.quizResult, this.card);
        }

        [Test]
        public void Then_the_card_level_should_remain_the_same()
        {
            this.card.Level.Should().Be(0);
        }

        [Test]
        public void Then_the_QuizDate_should_remain_the_same()
        {
            this.card.QuizDate.Should().Be(this.quizDate);
        }
    }
}