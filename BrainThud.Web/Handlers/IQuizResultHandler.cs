using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public interface IQuizResultHandler 
    {
        void ApplyQuizResult(QuizResult quizResult, Card card, int year, int month, int day);
        void ReverseQuizResult(QuizResult quizResult, Card card);
    }
}