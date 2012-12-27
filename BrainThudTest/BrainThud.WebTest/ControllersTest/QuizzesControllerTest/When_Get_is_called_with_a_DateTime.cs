using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Dtos;
using BrainThud.Web.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_a_DateTime : Given_a_new_QuizzesController
    {
        private DateTime quizDate;
        private Quiz quiz;
        private IList<QuizResult> quizResults;
        private IEnumerable<Card> quizResultCards;

        public override void When()
        {
            this.quizDate = new DateTime(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);

            var dayBefore = this.quizDate.AddDays(-TestValues.DAY);
            var millisecondAfter = this.quizDate.AddMilliseconds(TestValues.DAY);
            var twelveHoursAfter = this.quizDate.AddHours(12);
            var dayAfter = this.quizDate.AddDays(TestValues.DAY);

            // Use Level property of 1 to indicate which cards should be included in result
            var userCards = Builder<Card>.CreateListOfSize(10)
                .TheFirst(2).With(x => x.QuizDate = dayBefore).And(x => x.Level = 1)
                .TheNext(2).With(x => x.QuizDate = this.quizDate).And(x => x.Level = 1)
                .TheNext(2).With(x => x.QuizDate = millisecondAfter).And(x => x.Level = 1)
                .TheNext(2).With(x => x.QuizDate = twelveHoursAfter).And(x => x.Level = 1)
                .TheNext(2).With(x => x.QuizDate = dayAfter)
                .Build();

            this.quizResultCards = Builder<Card>.CreateListOfSize(2)
                .All().With(x => x.Level = 1)
                .Build();

            // Use IsCorrect = true to indicate which cards should be included in result
            this.quizResults = Builder<QuizResult>.CreateListOfSize(3).Build();

            this.TableStorageContext.Setup(x => x.Cards.GetForUser()).Returns(userCards.AsQueryable());
            this.TableStorageContext.Setup(x => x.Cards.GetForQuizResults(this.quizResults)).Returns(this.quizResultCards.AsQueryable());
            this.TableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY)).Returns(this.quizResults.AsQueryable());

            var userConfiguration = new UserConfiguration { UserId = TestValues.USER_ID };
            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns(userConfiguration);
            this.quiz = this.QuizzesController.Get(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);
        }

        [Test]
        public void Then_only_cards_with_a_QuizDate_less_than_or_equal_to_the_quizDate_parameter_are_returned()
        {
            this.quiz.Cards.Should().OnlyContain(x => x.Level == 1).And.HaveCount(10);
        }

        [Test]
        public void Then_the_cards_referenced_from_the_QuizResults_should_be_included_in_the_QuizCards()
        {
            this.quiz.Cards.Should().Contain(this.quizResultCards);
        }

        [Test]
        public void Then_the_ResultsUri_is_returned()
        {
            this.quiz.ResultsUri.Should().Be(TestUrls.LOCALHOST);
        }

        [Test]
        public void Then_the_UserId_is_returned_in_the_Quiz_object()
        {
            this.quiz.UserId.Should().Be(TestValues.USER_ID);
        }

        [Test]
        public void Then_the_QuizDate_is_returned()
        {
            this.quiz.QuizDate.Should().Be(this.quizDate.Date);
        }

        [Test]
        public void Then_the_QuizResults_for_the_quiz_are_returned()
        {
            this.quiz.QuizResults.Should().BeEquivalentTo(this.quizResults);
        }
    }
}