using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_a_Post_is_made_without_IfMatchNone_and_the_QuizResult_already_exists : Given_a_new_QuizResultsController
    {
        private QuizResult quizResult;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(new[] {new QuizResult {CardId = TestValues.CARD_ID}}.AsQueryable());

            this.quizResult = new QuizResult();
            this.QuizResultsController.Post(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID, 
                this.quizResult);
        }

        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_with_a_409_status_code()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.Conflict));
        }

        [Test]
        public void Then_the_location_of_the_existing_QuizResult_is_returned_in_the_response()
        {
            this.TestException<HttpResponseException>(x => x.Response.Headers.Location.Should().Be(TestUrls.LOCALHOST));
        }
    }
}