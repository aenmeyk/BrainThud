﻿using System;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ApplyQuizResult_is_called_with_a_level_less_than_zero : Given_a_new_QuizResultHandler
    {
        private Card card;
        private QuizResult quizResult;

        public override void When()
        {
            var quizDate = TestValues.CARD_QUIZ_DATE;
            this.quizResult = new QuizResult { IsCorrect = true };
            this.card = new Card { Level = -10, QuizDate = quizDate };
            this.QuizResultHandler.ApplyQuizResult(this.quizResult, this.card, quizDate.Year, quizDate.Month, quizDate.Day);
        }

        [Test]
        public void Then_the_Card_level_should_be_zero()
        {
            this.card.Level.Should().Be(0);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_level_zero_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.UserConfiguration.QuizCalendar[0]);
            this.card.QuizDate.Should().BeWithin(10.Seconds()).Before(expectedQuizDate);
        }

        [Test]
        public void Then_the_QuizResult_CardQuizDate_should_be_set_to_the_card_QuizDate_before_the_QuizResult_was_applied()
        {
            this.quizResult.CardQuizDate.Should().Be(TestValues.CARD_QUIZ_DATE);
        }

        [Test]
        public void Then_the_QuizResult_CardLevel_should_be_set_to_zero()
        {
            this.quizResult.CardLevel.Should().Be(0);
        }
    }
}