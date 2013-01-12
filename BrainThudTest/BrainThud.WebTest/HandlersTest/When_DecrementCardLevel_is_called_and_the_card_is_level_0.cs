using System;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_DecrementCardLevel_is_called_and_the_card_is_level_0 : Given_a_new_QuizResultHandler
    {
        private readonly DateTime quizDate = TestValues.DATETIME;
        private Card card;

        public override void When()
        {
            this.card = new Card { EntityId = TestValues.CARD_ID, Level = 0, QuizDate = quizDate };
            this.QuizResultHandler.DecrementCardLevel(this.card);
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