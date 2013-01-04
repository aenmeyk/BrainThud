using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_a_Post_is_made_and_the_related_card_does_not_exist : Given_a_new_QuizResultsController
    {
        private const int YEAR = 2012;
        private const int MONTH = 8;
        private const int DAY = 19;
        private QuizResult quizResult;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetById(TestValues.USER_ID, TestValues.CARD_ID)).Returns((Card)null);
            this.quizResult = new QuizResult { EntityId = TestValues.QUIZ_RESULT_ID, UserId = TestValues.USER_ID, CardId = TestValues.CARD_ID };
            this.QuizResultsController.Post(TestValues.USER_ID, YEAR, MONTH, DAY, this.quizResult);
        }

        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_and_422_should_be_returned_in_the_response()
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
                exception.Response.StatusCode.Should().Be((HttpStatusCode)422);
            }
        }
    }
}