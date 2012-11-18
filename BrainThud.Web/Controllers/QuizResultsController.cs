using System;
using System.Net;
using System.Net.Http;
using BrainThud.Data;
using BrainThud.Model;
using BrainThud.Web.Handlers;

namespace BrainThud.Web.Controllers
{
    public class QuizResultsController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuizResultHandler quizResultHandler;

        public QuizResultsController(IUnitOfWork unitOfWork, IQuizResultHandler quizResultHandler)
        {
            this.unitOfWork = unitOfWork;
            this.quizResultHandler = quizResultHandler;
        }

        public HttpResponseMessage Post(int year, int month, int day, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                quizResult.QuizDate = new DateTime(year, month, day);

                // TODO: Handle the situation where the card doesn't exist
                var card = this.unitOfWork.Cards.Get(quizResult.CardId);
                this.quizResultHandler.UpdateCardLevel(quizResult, card);
                this.unitOfWork.QuizResults.Add(quizResult);
                this.unitOfWork.Cards.Update(card);
                this.unitOfWork.Commit();
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
                this.unitOfWork.QuizResults.Delete(id);
                this.unitOfWork.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}