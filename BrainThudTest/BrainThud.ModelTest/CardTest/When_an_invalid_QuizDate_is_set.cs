using System;
using BrainThudTest.Extensions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.ModelTest.CardTest
{
    [TestFixture]
    public class When_an_invalid_QuizDate_is_set : Given_a_new_Card
    {
        public override void When()
        {
            this.Card.QuizDate = DateTime.MinValue;
        }

        [Test]
        public void Then_a_validation_exception_should_be_thrown()
        {
            this.Card.ShouldThrowValidationException("The field QuizDate must be between 1/1/1753 12:00:00 AM and 12/31/9999 12:00:00 AM.");
        }
    }
}