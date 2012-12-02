using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ITableStorageRepository<T>: IRepository<T> where T : TableServiceEntity
    {
        void Commit();
        void Delete(string partitionKey, string rowKey);
    }
}