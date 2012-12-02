using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using BrainThud.Web;
using BrainThud.Web.Model;
using BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest;
using BrainThudTest.Tools;
using NUnit.Framework;
using FluentAssertions;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_a_successful_Post_is_made : Given_a_new_QuizResultsController
    {
        private const int YEAR = 2012;
        private const int MONTH = 8;
        private const int DAY = 21;
        private QuizResult quizResult;
        private HttpResponseMessage response;

        public override void When()
        {
            this.quizResult = new QuizResult();
            this.response = this.QuizResultsController.Post(YEAR, MONTH, DAY, this.quizResult);
        }

        [Test]
        public void Then_a_201_status_code_should_be_returned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void Then_the_QuizResult_is_added_to_the_UnitOfWork()
        {
            this.UnitOfWork.Verify(x => x.QuizResults.Add(this.quizResult), Times.Once());
        }

        [Test]
        public void Then_the_UnitOfWork_should_commit_its_changes()
        {
            this.UnitOfWork.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_the_QuizDate_should_be_set_from_the_date_parameters()
        {
            this.quizResult.QuizDate.Year.Should().Be(YEAR);
            this.quizResult.QuizDate.Month.Should().Be(MONTH);
            this.quizResult.QuizDate.Day.Should().Be(DAY);
        }

        [Test]
        public void Then_the_QuizResult_should_be_returned_in_the_response()
        {
            var task = this.response.Content.ReadAsStringAsync();
            var quizResultInResponse = Json.Decode(task.Result);
            ((string)quizResultInResponse.RowKey).Should().Be(this.quizResult.RowKey);
        }

        [Test]
        public void Then_the_location_should_be_created_from_the_QuizResult_RowKey()
        {
            var type = this.QuizResultsController.RouteValues.GetType();
            var propertyInfo = type.GetProperty("id");
            var id = propertyInfo.GetValue(this.QuizResultsController.RouteValues, null);

            id.Should().Be(this.quizResult.RowKey);
            this.QuizResultsController.RouteName.Should().Be(RouteNames.API_QUIZ_RESULTS);
            this.response.Headers.Location.ToString().Should().Be(TestUrls.LOCALHOST);
        }

        [Test]
        public void Then_UpdateCardLevel_is_called_on_the_QuizResultHandler()
        {
            this.QuizResultHandler.Verify(x => x.UpdateCardLevel(this.quizResult, It.Is<Card>(c => c.RowKey == this.quizResult.CardId)), Times.Once());
        }

        [Test]
        public void Then_the_card_is_updated_in_the_UnitOfWork()
        {
            this.UnitOfWork.Verify(x => x.Cards.Update(It.Is<Card>(c => c.RowKey == this.quizResult.CardId)), Times.Once());
        }
    }
}