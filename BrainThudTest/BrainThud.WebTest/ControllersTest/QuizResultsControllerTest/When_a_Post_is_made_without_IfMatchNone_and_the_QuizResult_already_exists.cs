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

            this.quizResult = new QuizResult { EntityId = TestValues.QUIZ_RESULT_ID, UserId = TestValues.USER_ID, CardId = TestValues.CARD_ID };
            this.QuizResultsController.Post(TestValues.USER_ID, TestValues.YEAR, TestValues.MONTH, TestValues.DAY, this.quizResult);
        }

        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_with_a_409_status_code()
        {
            HttpResponseException exception = null;

            try
            {
                this.ThrowUnhandledExceptions();
            }
            catch (HttpResponseException e)
            {
                exception = e;
            }
            finally
            {
                exception.Response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            }
        }
    }
}