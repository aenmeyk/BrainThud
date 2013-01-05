using System.Linq;
using System.Net;
using System.Web.Http;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_for_a_Quiz_that_does_not_exist : Given_a_new_QuizResultsController
    {
        public override void When()
        {
            this.TableStorageContext
                .Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns((IQueryable<QuizResult>)null);

            this.QuizResultsController.Get(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH,
                TestValues.DAY);
        }

        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_with_a_404_status_code()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.NotFound));
        }
    }
}