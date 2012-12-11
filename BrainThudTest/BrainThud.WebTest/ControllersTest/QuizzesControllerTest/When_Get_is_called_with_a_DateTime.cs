using System;
using System.Linq;
using BrainThud.Web.Dtos;
using BrainThud.Web.Model;
using BrainThudTest.Tools;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_a_DateTime : Given_a_new_QuizzesController
    {
        private const int YEAR = 2012;
        private const int MONTH = 7;
        private const int DAY = 1;
        private DateTime quizDate;
        private Quiz quiz;

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

            this.TableStorageContext.Setup(x => x.Cards.GetAllForUser()).Returns(allCards.AsQueryable());
            var userConfiguration = new UserConfiguration { UserId = TestValues.USER_ID };
            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns(userConfiguration);
            this.quiz = this.QuizzesController.Get(YEAR, MONTH, DAY);
        }

        [Test]
        public void Then_only_cards_with_a_QuizDate_less_than_or_equal_to_the_quizDate_parameter_are_returned()
        {
            this.quiz.Cards.Should().OnlyContain(x => x.Level == 0).And.HaveCount(8);
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
    }
}