
using System.Data.Services.Client;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public interface ITableStorageContext<T> where T: TableServiceEntity
    {
        void AddObject(T entity);
        DataServiceResponse SaveChangesWithRetries();
    }
}