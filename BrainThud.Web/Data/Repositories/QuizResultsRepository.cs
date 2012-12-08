using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class QuizResultsRepository : CardEntityRepository<QuizResult>
    {
        public QuizResultsRepository(ITableStorageContext tableStorageContext, string nameIdentifier) 
            : base(tableStorageContext, nameIdentifier, CardRowTypes.QUIZ_RESULT) {}
    }
}