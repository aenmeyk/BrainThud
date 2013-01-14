using System;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ReverseIfExists_is_called_and_the_QuizResult_does_not_exist : Given_a_new_QuizResultHandler
    {
        private const int CARD_LEVEL = 3;
        private readonly DateTime quizDate = TestValues.DATETIME;
        private Card card;

        public override void When()
        {
            var quizResult = new QuizResult { QuizDate = TestValues.DATETIME };
            this.card = new Card { Level = CARD_LEVEL, QuizDate = quizDate };
            var tableStorageContext = new Mock<ITableStorageContext>();
            tableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(new QuizResult[0].AsQueryable());

            this.QuizResultHandler.ReverseIfExists(tableStorageContext.Object, quizResult, this.card);
        }

        [Test]
        public void Then_the_card_level_should_remain_the_same()
        {
            this.card.Level.Should().Be(CARD_LEVEL);
        }

        [Test]
        public void Then_the_QuizDate_should_remain_the_same()
        {
            this.card.QuizDate.Should().Be(this.quizDate);
        }
    }
}