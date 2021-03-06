﻿using System;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_ApplyQuizResult_is_called_with_a_correct_result : Given_a_new_QuizResultHandler
    {
        private const int CARD_LEVEL = 3;
        private Card card;
        private QuizResult quizResult;

        public override void When()
        {
            this.quizResult = new QuizResult { IsCorrect = true };
            this.card = new Card
            {
                Level = CARD_LEVEL,
                QuizDate = TestValues.CARD_QUIZ_DATE, 
                IsCorrect = false, 
                CompletedQuizYear = TestValues.YEAR, 
                CompletedQuizMonth = TestValues.MONTH, 
                CompletedQuizDay = TestValues.DAY
            };

            this.QuizResultHandler.ApplyQuizResult(this.quizResult, this.card, TestValues.CARD_QUIZ_DATE.Year, TestValues.CARD_QUIZ_DATE.Month, TestValues.CARD_QUIZ_DATE.Day);
        }

        [Test]
        public void Then_the_Card_level_should_be_incremented()
        {
            this.card.Level.Should().Be(CARD_LEVEL + 1);
        }

        [Test]
        public void Then_the_QuizDate_is_updated_to_the_next_day_in_the_calendar()
        {
            var expectedQuizDate = DateTime.UtcNow.AddDays(this.UserConfiguration.QuizCalendar[CARD_LEVEL + 1]);
            this.card.QuizDate.Should().BeWithin(10.Seconds()).Before(expectedQuizDate);
        }

        [Test]
        public void Then_IsCorrect_is_set_to_the_QuizResult_IsCorrect()
        {
            this.card.IsCorrect.Should().Be(this.quizResult.IsCorrect);
        }

        [Test]
        public void Then_completed_quiz_date_should_be_set_to_the_quiz_date()
        {
            this.card.CompletedQuizYear.Should().Be(TestValues.CARD_QUIZ_DATE.Year);
            this.card.CompletedQuizMonth.Should().Be(TestValues.CARD_QUIZ_DATE.Month);
            this.card.CompletedQuizDay.Should().Be(TestValues.CARD_QUIZ_DATE.Day);
        }

        [Test]
        public void Then_the_QuizResult_CardQuizDate_should_be_set_to_the_card_QuizDate_before_the_QuizResult_was_applied()
        {
            this.quizResult.CardQuizDate.Should().Be(TestValues.CARD_QUIZ_DATE);
        }

        [Test]
        public void Then_the_QuizResult_CardLevel_should_be_set_to_the_card_level_before_it_was_incremented()
        {
            this.quizResult.CardLevel.Should().Be(CARD_LEVEL);
        }

        [Test]
        public void Then_the_QuizResult_CardIsCorrect_is_set_to_the_QuizResult_IsCorrect()
        {
            this.quizResult.CardIsCorrect.Should().BeFalse();
        }

        [Test]
        public void Then_the_QuizResult_Card_completed_quiz_date_should_be_set_to_today()
        {
            this.quizResult.CardCompletedQuizYear.Should().Be(TestValues.YEAR);
            this.quizResult.CardCompletedQuizMonth.Should().Be(TestValues.MONTH);
            this.quizResult.CardCompletedQuizDay.Should().Be(TestValues.DAY);
        }
    }
}