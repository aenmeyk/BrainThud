using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using System.Linq;

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

                // Ensure that a QuizResult doesn't already exist for this user for this card for this day
                var existingQuizResult = tableStorageContext.QuizResults
                    .GetForQuiz(year, month, day)
                    .Where(x => x.CardId == quizResult.CardId)
                    .FirstOrDefault();

                if (existingQuizResult != null)
                {
                    var eTagHeader = this.Request.Headers.IfNoneMatch.FirstOrDefault();
                    var responseStatusCode = eTagHeader != null && eTagHeader.Tag == "*"
                                                 ? HttpStatusCode.PreconditionFailed
                                                 : HttpStatusCode.Conflict;

                    throw new HttpResponseException(new HttpResponseMessage(responseStatusCode));
                }

                var card = GetCard(userId, quizResult, tableStorageContext);
                this.quizResultHandler.UpdateCardLevel(quizResult, card);
                tableStorageContext.QuizResults.Add(quizResult);
                tableStorageContext.Cards.Update(card);
                tableStorageContext.CommitBatch();

                // Create the response
                var response = this.Request.CreateResponse(HttpStatusCode.Created, quizResult);
                var routeValues = new { userId, year, month, day, quizResultId = quizResult.EntityId };
                response.Headers.Location = new Uri(this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues));

                return response;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        public HttpResponseMessage Put(int userId, int year, int month, int day, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                quizResult.QuizDate = new DateTime(year, month, day);
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
                var card = GetCard(userId, quizResult, tableStorageContext);
                this.quizResultHandler.ReverseIfExists(tableStorageContext, quizResult, card);
                this.quizResultHandler.UpdateCardLevel(quizResult, card);
                tableStorageContext.QuizResults.Add(quizResult);
                tableStorageContext.Cards.Update(card);
                tableStorageContext.CommitBatch();

                // Create the response
                var response = this.Request.CreateResponse(HttpStatusCode.OK, quizResult);
                var routeValues = new { userId, year, month, day, quizResultId = quizResult.EntityId };
                response.Headers.Location = new Uri(this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues));

                return response;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
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

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        private static Card GetCard(int userId, QuizResult quizResult, ITableStorageContext tableStorageContext)
        {
            var card = tableStorageContext.Cards.GetById(userId, quizResult.CardId);

            if(card == null)
            {
                // I'm not convinced that 422 is the right status code to return here since it's not a standard code
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode)422));
            }
            
            return card;
        }
    }
}