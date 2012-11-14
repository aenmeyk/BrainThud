using System;
using System.Collections.Generic;
using BrainThud.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_a_DateTime : Given_a_new_QuizController
    {
        private const int YEAR = 2012;
        private const int MONTH = 7;
        private const int DAY = 1;
        private DateTime quizDate;
        private IEnumerable<Card> result;

        public override void When()
        {
            this.quizDate = new DateTime(YEAR, MONTH, DAY);

            var dayBefore = this.quizDate.AddDays(-DAY);
            var millisecondAfter = this.quizDate.AddMilliseconds(DAY);
            var twelveHoursAfter = this.quizDate.AddHours(12);
            var dayAfter = this.quizDate.AddDays(DAY);

            // Use Level property of 0 to indicate which cards should be included in result
            var allCards = Builder<Card>.CreateListOfSize(10)
                .TheFirst(2).With(x => x.QuizDate = dayBefore).And(x => x.Level = 0)
                .TheNext(2).With(x => x.QuizDate = this.quizDate).And(x => x.Level = 0)
                .TheNext(2).With(x => x.QuizDate = millisecondAfter).And(x => x.Level = 0)
                .TheNext(2).With(x => x.QuizDate = twelveHoursAfter).And(x => x.Level = 0)
                .TheNext(2).With(x => x.QuizDate = dayAfter).And(x => x.Level = DAY)
                .Build();

            this.UnitOfWork.Setup(x => x.Cards.GetAll()).Returns(allCards.AsQueryable());
            this.result = this.QuizController.Get(YEAR, MONTH, DAY);
        }

        [Test]
        public void Then_only_cards_with_a_QuizDate_less_than_or_equal_to_the_quizDate_parameter_are_returned()
        {
            this.result.Should().OnlyContain(x => x.Level == 0).And.HaveCount(8);
        }
    }
}