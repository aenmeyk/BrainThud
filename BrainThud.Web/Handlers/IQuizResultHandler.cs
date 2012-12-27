using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public interface IQuizResultHandler 
    {
        void UpdateCardLevel(QuizResult quizResult, Card card);
        void ReverseIfExists(ITableStorageContext tableStorageContext, QuizResult quizResult, Card card);
    }
}