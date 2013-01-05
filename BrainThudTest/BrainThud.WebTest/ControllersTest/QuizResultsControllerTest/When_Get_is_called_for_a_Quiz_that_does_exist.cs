using System.Collections.Generic;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_for_a_Quiz_that_does_exist : Given_a_new_QuizResultsController
    {
        private IQueryable<QuizResult> expectedQuizResults;
        private IEnumerable<QuizResult> returnedQuizResults;

        public override void When()
        {
            this.expectedQuizResults = new HashSet<QuizResult> { new QuizResult(), new QuizResult() }.AsQueryable();

            this.TableStorageContext
                .Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(expectedQuizResults);

            this.returnedQuizResults = this.QuizResultsController.Get(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY);
        }

        [Test]
        public void Then_the_QuizResult_for_that_user_for_that_card_for_that_day_should_be_returned()
        {
            this.returnedQuizResults.Should().Equal(this.expectedQuizResults);
        }
    }
}