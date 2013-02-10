using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Core.Models;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_GetForQuiz_is_called_with_a_DateTime : Given_a_new_CardsController
    {
        private DateTime quizDate;
        private IEnumerable<Card> cards;
        private IList<QuizResult> quizResults;
        private IEnumerable<Card> quizResultCards;

        public override void When()
        {
            this.quizDate = new DateTime(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);

            var dayBefore = this.quizDate.AddDays(-TestValues.DAY);
            var millisecondAfter = this.quizDate.AddMilliseconds(TestValues.DAY);
            var twelveHoursAfter = this.quizDate.AddHours(12);
            var dayAfter = this.quizDate.AddDays(TestValues.DAY);

            var generator = new UniqueRandomGenerator();

            // Use EntityId of >= 100 to indicate which cards should be included in result
            var userCards = Builder<Card>.CreateListOfSize(10)
                .All().With(x => x.CreatedTimestamp = generator.Next(DateTime.MinValue, DateTime.MaxValue))
                .TheFirst(2).With(x => x.QuizDate = dayBefore).And(x => x.EntityId = generator.Next(100, 1000))
                .TheNext(2).With(x => x.QuizDate = this.quizDate).And(x => x.EntityId = generator.Next(100, 1000))
                .TheNext(2).With(x => x.QuizDate = millisecondAfter).And(x => x.EntityId = generator.Next(100, 1000))
                .TheNext(2).With(x => x.QuizDate = twelveHoursAfter).And(x => x.EntityId = generator.Next(100, 1000))
                .TheNext(2).With(x => x.QuizDate = dayAfter)
                .Build();

            this.quizResultCards = Builder<Card>.CreateListOfSize(2)
                .All().With(x => x.EntityId = generator.Next(100, 1000))
                .Build();

            // Use IsCorrect = true to indicate which cards should be included in result
            this.quizResults = Builder<QuizResult>.CreateListOfSize(3).Build();

            this.TableStorageContext.Setup(x => x.Cards.GetForUser()).Returns(userCards.AsQueryable());
            this.TableStorageContext.Setup(x => x.Cards.GetForQuizResults(this.quizResults)).Returns(this.quizResultCards.AsQueryable());
            this.TableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY)).Returns(this.quizResults.AsQueryable());

            var userConfiguration = new UserConfiguration { UserId = TestValues.USER_ID };
            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns(userConfiguration);
            this.cards = this.CardsController.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);
        }

        [Test]
        public void Then_only_cards_with_a_QuizDate_less_than_or_equal_to_the_quizDate_parameter_are_returned()
        {
            this.cards.Select(x => x.EntityId).Should().OnlyContain(x => x >= 100).And.HaveCount(10);
        }

        [Test]
        public void Then_the_cards_referenced_from_the_QuizResults_should_be_included_in_the_QuizCards()
        {
            this.cards.Select(x => x.EntityId).Should().Contain(this.quizResultCards.Select(x => x.EntityId));
        }

        [Test]
        public void Then_the_cards_should_be_returned_in_CreatedTimestamp_order()
        {
            this.cards.Select(x => x.CreatedTimestamp).Should().BeInAscendingOrder();
        }
    }
}