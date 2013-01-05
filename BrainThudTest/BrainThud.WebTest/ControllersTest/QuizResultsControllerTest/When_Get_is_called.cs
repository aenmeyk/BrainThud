using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_QuizResultsController
    {
        private IQueryable<QuizResult> expectedQuizResults;
        private IEnumerable<QuizResult> returnedQuizResults;

        public override void When()
        {
            this.expectedQuizResults = new HashSet<QuizResult> { new QuizResult(), new QuizResult() }.AsQueryable();
            this.TableStorageContext.Setup(x => x.QuizResults.GetForUser()).Returns(this.expectedQuizResults);
            this.returnedQuizResults = this.QuizResultsController.Get();
        }

        [Test]
        public void Then_all_QuizResults_are_returned_from_the_QuizResults_repository()
        {
            this.returnedQuizResults.Should().Equal(this.expectedQuizResults);
        }
    }
}