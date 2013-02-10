using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Core.Models;

namespace BrainThud.Web.Data.Repositories
{
    public interface ICardRepository : ICardEntityRepository<Card>
    {
        IEnumerable<Card> GetForQuizResults(IEnumerable<QuizResult> quizResults);
        void Add(Card entity, DateTime clientDateTime);
    }
}