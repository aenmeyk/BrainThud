using BrainThud.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data
{
    public interface IRepositoryFactory 
    {
        ITableStorageRepository<T> CreateTableStorageRepository<T>() where T : TableServiceEntity;
    }
}