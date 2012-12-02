using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data
{
    public interface IRepositoryFactory 
    {
        ITableStorageRepository<T> CreateTableStorageRepository<T>() where T : TableServiceEntity;
    }
}