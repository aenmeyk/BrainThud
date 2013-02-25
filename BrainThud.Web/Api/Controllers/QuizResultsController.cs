using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Handlers;

namespace BrainThud.Web.Api.Controllers
{
    public class QuizResultsController : ApiControllerBase
    {
        private readonly IQuizResultHandler quizResultHandler;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public QuizResultsController(
            ITableStorageContextFactory tableStorageContextFactory,
            IQuizResultHandler quizResultHandler,
            IAuthenticationHelper authenticationHelper)
        {
            this.quizResultHandler = quizResultHandler;
            this.authenticationHelper = authenticationHelper;
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier));
        }

        public IEnumerable<QuizResult> Get(int userId, int year, int month, int day)
        {
            var quizResult = this.TableStorageContext.QuizResults
                .GetForQuiz(year, month, day);

            if (quizResult != null) return quizResult;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public QuizResult Get(int userId, int year, int month, int day, int cardId)
        {
            var quizResult = this.TableStorageContext.QuizResults
                .GetForQuiz(year, month, day)
                .Where(x => x.CardId == cardId)
                .FirstOrDefault();

            if(quizResult != null) return quizResult;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpResponseMessage Post(int userId, int year, int month, int day, int cardId, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                quizResult.QuizDate = new DateTime(year, month, day);
                quizResult.QuizYear = year;
                quizResult.QuizMonth = month;
                quizResult.QuizDay = day;
                quizResult.UserId = userId;
                quizResult.CardId = cardId;

                // Ensure that a QuizResult doesn't already exist for this user for this card for this day
                var existingQuizResult = this.TableStorageContext.QuizResults
                    .GetForQuiz(year, month, day)
                    .Where(x => x.CardId == cardId)
                    .FirstOrDefault();

                if (existingQuizResult != null)
                {
                    var eTagHeader = this.Request.Headers.IfNoneMatch.FirstOrDefault();
                    var responseStatusCode = eTagHeader != null && eTagHeader.Tag == "*"
                                                 ? HttpStatusCode.PreconditionFailed
                                                 : HttpStatusCode.Conflict;

                    var httpResponseMessage = new HttpResponseMessage(responseStatusCode);
                    var existingRouteValues = new {userId, year, month, day, cardId};
                    httpResponseMessage.Headers.Location = this.GetQuizResultUri(existingRouteValues);
                    throw new HttpResponseException(httpResponseMessage);
                }

                var card = GetCard(userId, cardId, this.TableStorageContext);
                this.quizResultHandler.ApplyQuizResult(quizResult, card, year, month, day);
                this.TableStorageContext.QuizResults.Add(quizResult);
                this.TableStorageContext.Cards.Update(card);
                this.TableStorageContext.CommitBatch();

                // Create the response
                var response = this.Request.CreateResponse(HttpStatusCode.Created, quizResult);
                var routeValues = new { userId, year, month, day, cardId };
                response.Headers.Location = this.GetQuizResultUri(routeValues);

                return response;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        public HttpResponseMessage Put(int userId, int year, int month, int day, int cardId, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                // Maybe do some sort of valid state check at the beginning of this
                // method and throw exception if not valid.  Check: card exists, quiz result exists, quizResult param
                // contains valid data (keys not null, etc).

                var card = GetCard(userId, cardId, this.TableStorageContext);

                // We are updating the QuizResult for this card so first reverse the previous result then apply the new one
                var existingQuizResult = this.TableStorageContext.QuizResults.Get(quizResult.PartitionKey, quizResult.RowKey);
                this.quizResultHandler.ReverseQuizResult(existingQuizResult, card);
                this.TableStorageContext.Detach(existingQuizResult);
                this.quizResultHandler.ApplyQuizResult(quizResult, card, year, month, day);
                this.TableStorageContext.QuizResults.Update(quizResult);
                this.TableStorageContext.Cards.Update(card);
                this.TableStorageContext.CommitBatch();

                // Create the response
                var response = this.Request.CreateResponse(HttpStatusCode.OK, quizResult);
                var routeValues = new { userId, year, month, day, cardId };
                response.Headers.Location = this.GetQuizResultUri(routeValues);

                return response;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        public HttpResponseMessage Delete(int userId, int year, int month, int day, int cardId)
        {
            if (this.ModelState.IsValid)
            {
                var quizResult = this.TableStorageContext.QuizResults
                    .GetForQuiz(year, month, day)
                    .Where(x => x.CardId == cardId)
                    .FirstOrDefault();

                if(quizResult == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }

                this.TableStorageContext.QuizResults.DeleteById(userId, quizResult.EntityId);
                this.TableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        private Uri GetQuizResultUri(object routeValues)
        {
            return new Uri(this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues));
        }

        private static Card GetCard(int userId, int cardId, ITableStorageContext tableStorageContext)
        {
            var card = tableStorageContext.Cards.GetById(userId, cardId);

            if (card == null)
            {
                // I'm not convinced that 422 (Unprocessable Entity) is the correct status code to return here since it's not a standard code
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode)422));
            }

            return card;
        }
    }
}