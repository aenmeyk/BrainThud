using System.Net;
using System.Web.Http;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_for_a_Card_that_does_not_exist : Given_a_new_QuizResultsController
    {
        public override void When()
        {
            var quizResults = Builder<QuizResult>.CreateListOfSize(1).Build();

            this.TableStorageContext
                .Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(quizResults.AsQueryable());

            this.QuizResultsController.Get(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH,
                TestValues.DAY, 
                TestValues.CARD_ID);
        }

        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_with_a_404_status_code()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.NotFound));
        }
    }
}