using System;
using System.Net;
using System.Net.Http;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Controllers
{
    public class QuizResultsController : ApiControllerBase
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly IQuizResultHandler quizResultHandler;
        private readonly IAuthenticationHelper authenticationHelper;

        public QuizResultsController(
            ITableStorageContextFactory tableStorageContextFactory,
            IQuizResultHandler quizResultHandler,
            IAuthenticationHelper authenticationHelper)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.quizResultHandler = quizResultHandler;
            this.authenticationHelper = authenticationHelper;
        }

        public HttpResponseMessage Post(int userId, int year, int month, int day, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                quizResult.QuizDate = new DateTime(year, month, day);
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);

                // TODO: Handle the situation where the card doesn't exist
                var card = tableStorageContext.Cards.GetById(userId, quizResult.CardId);
                this.quizResultHandler.ReverseIfExists(tableStorageContext, quizResult, card);
                this.quizResultHandler.UpdateCardLevel(quizResult, card);
                tableStorageContext.QuizResults.Add(quizResult);
                tableStorageContext.Cards.Update(card);
                tableStorageContext.CommitBatch();
                var response = this.Request.CreateResponse(HttpStatusCode.Created, quizResult);
                var routeValues = new { userId, year, month, day, quizResultId = quizResult.EntityId };
                response.Headers.Location = new Uri(this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues));

                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(int userId, int year, int month, int day, int quizResultId)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
                tableStorageContext.QuizResults.DeleteById(userId, quizResultId);
                tableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}