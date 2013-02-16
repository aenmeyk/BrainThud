using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using BrainThud.Core.Models;
using BrainThud.Web;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_a_successful_Put_is_called : Given_a_new_QuizResultsController
    {
        private HttpResponseMessage response;
        private readonly Card card = new Card { EntityId = TestValues.CARD_ID };
        private readonly QuizResult quizResult = new QuizResult { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY, EntityId = TestValues.QUIZ_RESULT_ID, UserId = TestValues.USER_ID, CardId = TestValues.CARD_ID };
        private readonly QuizResult existingQuizResult = new QuizResult();

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetById(TestValues.USER_ID, TestValues.CARD_ID)).Returns(this.card);
            this.TableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(new[] { new QuizResult { CardId = TestValues.CARD_ID } }.AsQueryable());

            this.TableStorageContext.Setup(x => x.QuizResults.Get(TestValues.PARTITION_KEY, TestValues.ROW_KEY))
                .Returns(this.existingQuizResult);

            this.response = this.QuizResultsController.Put(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID, 
                this.quizResult);
        }

        [Test]
        public void Then_DecrementCardLevel_should_be_called_on_QuizResultHandler()
        {
            this.QuizResultHandler.Verify(x => x.ReverseQuizResult(this.existingQuizResult, this.card), Times.Once());
        }

        [Test]
        public void Then_a_200_status_code_should_be_returned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Then_the_QuizResult_is_updated_in_the_QuizResults_repository()
        {
            this.TableStorageContext.Verify(x => x.QuizResults.Update(this.quizResult), Times.Once());
        }

        [Test]
        public void Then_the_TableStorageContext_should_commit_its_changes_in_a_batch()
        {
            this.TableStorageContext.Verify(x => x.CommitBatch(), Times.Once());
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

            var userIdPropertyInfo = type.GetProperty("userId");
            var userId = userIdPropertyInfo.GetValue(this.QuizResultsController.RouteValues, null);
            userId.Should().Be(this.quizResult.UserId);

            var quizResultIdPropertyInfo = type.GetProperty("cardId");
            var cardId = quizResultIdPropertyInfo.GetValue(this.QuizResultsController.RouteValues, null);
            cardId.Should().Be(this.quizResult.CardId);

            this.QuizResultsController.RouteName.Should().Be(RouteNames.API_QUIZ_RESULTS);
            this.response.Headers.Location.ToString().Should().Be(TestUrls.LOCALHOST);
        }

        [Test]
        public void Then_IncrementCardLevel_is_called_on_the_QuizResultHandler()
        {
            this.QuizResultHandler.Verify(x => x.ApplyQuizResult(this.quizResult, It.Is<Card>(c => c.EntityId == this.quizResult.CardId), TestValues.YEAR, TestValues.MONTH, TestValues.DAY), Times.Once());
        }

        [Test]
        public void Then_the_card_is_updated_in_the_cards_repository()
        {
            this.TableStorageContext.Verify(x => x.Cards.Update(It.Is<Card>(c => c.EntityId == this.quizResult.CardId)), Times.Once());
        }

        [Test]
        public void Then_the_existing_QuizResult_should_be_detached_before_updating_the_new_QuizResult()
        {
            this.TableStorageContext.Verify(x => x.Detach(this.existingQuizResult), Times.Once());
        }
    }
}