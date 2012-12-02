using System;
using System.Net;
using System.Net.Http;
using BrainThud.Web.Data;
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

        public HttpResponseMessage Post(int year, int month, int day, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                quizResult.QuizDate = new DateTime(year, month, day);
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);

                // TODO: Handle the situation where the card doesn't exist
                var card = tableStorageContext.Cards.Get(this.authenticationHelper.NameIdentifier, quizResult.CardId);
                this.quizResultHandler.UpdateCardLevel(quizResult, card);
                tableStorageContext.QuizResults.Add(quizResult);
                tableStorageContext.Cards.Update(card);
                tableStorageContext.Commit();
                var response = this.Request.CreateResponse(HttpStatusCode.Created, quizResult);

                var routeValues = new
                {
                    year,
                    month,
                    day,
                    id = quizResult.RowKey
                };

                response.Headers.Location = new Uri(this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues));
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(string id)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
                tableStorageContext.QuizResults.Delete(authenticationHelper.NameIdentifier, id);
                tableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}