using BrainThud.Data.AzureTableStorage;

namespace BrainThud.Data
{
    public interface IRepositoryFactory 
    {
        ITableStorageRepository<T> CreateTableStorageRepository<T>();
    }
}