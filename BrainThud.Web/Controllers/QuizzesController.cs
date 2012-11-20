using System;
using System.Linq;
using BrainThud.Data;
using BrainThud.Web.Dtos;

namespace BrainThud.Web.Controllers
{
    public class QuizzesController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public QuizzesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Quiz Get(int year, int month, int day)
        {
            var quizDate = new DateTime(year, month, day)
                .AddDays(1).Date
                .AddMilliseconds(-1);

            var routeValues = new { year, month, day };

            var quiz = new Quiz
            {
                Cards = this.unitOfWork.Cards.GetAll().Where(x => x.QuizDate <= quizDate),
                ResultsUri = this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues)
            };

            return quiz;
        }
    }
}