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
        private IEnumerable<Card> result;
        private DateTime quizDate;

        public override void When()
        {
            this.quizDate = new DateTime(2012, 1, 1);

            var allCards = Builder<Card>.CreateListOfSize(6)
                .TheFirst(2).With(x => x.QuizDate = this.quizDate.AddDays(-1).Date)
                .TheNext(2).With(x => x.QuizDate = this.quizDate.Date)
                .TheNext(2).With(x => x.QuizDate = this.quizDate.AddDays(1).Date)
                .Build();

            this.UnitOfWork.Setup(x => x.Cards.GetAll()).Returns(allCards.AsQueryable());
            this.result = this.QuizController.Get(this.quizDate);
        }

        [Test]
        public void Then_only_cards_with_a_QuizDate_less_than_or_equal_to_the_quizDate_parameter_are_returned()
        {
            this.result.Should().OnlyContain(x => x.QuizDate <= this.quizDate.Date);
        }
    }
}