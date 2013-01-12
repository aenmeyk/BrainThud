using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public interface IQuizResultHandler 
    {
        void IncrementCardLevel(QuizResult quizResult, Card card);
        void DecrementCardLevel(Card card);
    }
}