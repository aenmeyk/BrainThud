using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;

namespace BrainThud.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepositoryFactory repositoryFactory)
        {
            this.Cards = repositoryFactory.CreateTableStorageRepository<Card>();
        }

        public ITableStorageRepository<Card> Cards { get; private set; }

        public void Commit()
        {
            this.Cards.Commit();
        }
    }
}