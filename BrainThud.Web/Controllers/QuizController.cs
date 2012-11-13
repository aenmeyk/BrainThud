using System;
using System.Collections.Generic;
using System.Web.Http;
using BrainThud.Model;

namespace BrainThud.Web.Controllers
{
    public class QuizController : ApiController
    {
        public IEnumerable<Card> Get(DateTime quizDate)
        {
            return null;
        }
    }
}