using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public interface ITableStorageRepository<T>: IRepository<T> where T : TableServiceEntity
    {
        void Commit();
        void Delete(string rowKey);
    }
}