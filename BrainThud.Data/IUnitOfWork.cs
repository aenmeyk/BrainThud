using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;

namespace BrainThud.Data
{
    public interface IUnitOfWork 
    {
        ITableStorageRepository<Card> Cards { get; }
        ITableStorageRepository<QuizResult> QuizResults { get; }
        void Commit();
    }
}