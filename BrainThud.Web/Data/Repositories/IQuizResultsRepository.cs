using System.Linq;
using BrainThud.Core.Models;

namespace BrainThud.Web.Data.Repositories
{
    public interface IQuizResultsRepository: ICardEntityRepository<QuizResult>
    {
        IQueryable<QuizResult> GetForQuiz(int year, int month, int day);
        void DeleteByCardId(int cardId);
    }
}