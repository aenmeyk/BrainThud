using BrainThud.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data
{
    public interface IRepositoryFactory 
    {
        ITableStorageRepository<T> CreateTableStorageRepository<T>(bool createTable = true) where T : TableServiceEntity;
    }
}