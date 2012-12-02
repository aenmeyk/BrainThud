using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepositoryFactory repositoryFactory)
        {
            this.Cards = repositoryFactory.CreateTableStorageRepository<Card>();
            this.QuizResults = repositoryFactory.CreateTableStorageRepository<QuizResult>();
        }

        public ITableStorageRepository<Card> Cards { get; private set; }
        public ITableStorageRepository<QuizResult> QuizResults { get; private set; }

        public void Commit()
        {
            this.Cards.Commit();
            this.QuizResults.Commit();
        }
    }
}