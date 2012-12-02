using System;
using System.Net;
using System.Net.Http;
using BrainThud.Web.Data;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Controllers
{
    public class QuizResultsController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuizResultHandler quizResultHandler;
        private readonly IAuthenticationHelper authenticationHelper;

        public QuizResultsController(
            IUnitOfWork unitOfWork, 
            IQuizResultHandler quizResultHandler, 
            IAuthenticationHelper authenticationHelper)
        {
            this.unitOfWork = unitOfWork;
            this.quizResultHandler = quizResultHandler;
            this.authenticationHelper = authenticationHelper;
        }

        public HttpResponseMessage Post(int year, int month, int day, QuizResult quizResult)
        {
            if (this.ModelState.IsValid)
            {
                quizResult.QuizDate = new DateTime(year, month, day);

                // TODO: Handle the situation where the card doesn't exist
                var card = this.unitOfWork.Cards.Get(this.authenticationHelper.NameIdentifier, quizResult.CardId);
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
                this.unitOfWork.QuizResults.Delete(authenticationHelper.NameIdentifier, id);
                this.unitOfWork.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}