using System;
using System.Collections.Generic;
using System.Web.Http;
using BrainThud.Data;
using BrainThud.Model;
using System.Linq;

namespace BrainThud.Web.Controllers
{
    public class QuizController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public QuizController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Card> Get(DateTime quizDate)
        {
            return this.unitOfWork.Cards.GetAll().Where(x => x.QuizDate.Date <= quizDate.Date);
        }
    }
}