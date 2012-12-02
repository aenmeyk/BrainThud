using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data
{
    public interface IUnitOfWork 
    {
        ITableStorageRepository<Card> Cards { get; }
        ITableStorageRepository<QuizResult> QuizResults { get; }
        void Commit();
    }
}