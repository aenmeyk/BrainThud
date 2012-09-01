using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;

namespace BrainThud.Data
{
    public class UnitOfWork 
    {
        public UnitOfWork(IRepositoryFactory repositoryFactory)
        {
            this.Nuggets = repositoryFactory.CreateTableStorageRepository<Nugget>();
        }

        public ITableStorageRepository<Nugget> Nuggets { get; private set; }

        public void Commit()
        {
            this.Nuggets.Commit();
        }
    }
}