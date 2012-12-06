using BrainThud.Web.Data.KeyGenerators;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ITableStorageRepository<T>: IRepository<T> where T : TableServiceEntity
    {
        void Delete(string partitionKey, string rowKey);
        void Add(T entity, ITableStorageKeyGenerator keyGenerator);
        T GetOrCreate(string partitionKey, string rowKey);
    }
}