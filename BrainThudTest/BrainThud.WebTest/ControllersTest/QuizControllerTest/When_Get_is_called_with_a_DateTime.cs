using System;
using System.Collections.Generic;
using BrainThud.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

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

            var allCards = Builder<Card>.CreateListOfSize(4)
                .All().With(x => x.QuizDate = new DateTime(2011, 6, 30))
                .Random(2).With(x => x.QuizDate = this.quizDate.Date)
                .Build();

            this.result = this.QuizController.Get(this.quizDate);
        }

        [Test]
        public void Then_only_those_cards_applicable_for_the_quizDate_are_returned()
        {
            this.result.Should().OnlyContain(x => x.QuizDate == this.quizDate.Date);
        }
    }
}