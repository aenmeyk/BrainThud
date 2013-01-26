using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public interface IQuizResultHandler 
    {
        void ApplyQuizResult(QuizResult quizResult, Card card);
        void ReverseQuizResult(QuizResult quizResult, Card card);
    }
}