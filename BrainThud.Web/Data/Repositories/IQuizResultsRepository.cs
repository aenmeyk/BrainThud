using System.Linq;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public interface IQuizResultsRepository: ICardEntityRepository<QuizResult>
    {
        IQueryable<QuizResult> GetForQuiz(int year, int month, int day);
    }
}