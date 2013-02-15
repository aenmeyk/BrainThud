using Microsoft.WindowsAzure.Storage;

namespace BrainThud.Web.Data.Repositories
{
    public interface ITableStorageRepository<T>: IRepository<T> where T : TableServiceEntity
    {
        void Delete(string partitionKey, string rowKey);
        T GetOrCreate(string partitionKey, string rowKey);
        void Add(T entity);
    }
}