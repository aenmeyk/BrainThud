
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public interface ITableStorageContext<T> where T: TableServiceEntity
    {
        void AddObject(T entity);
        void UpdateObject(T entity);
        void DeleteObject(T entity);
        DataServiceResponse SaveChangesWithRetries();
        IQueryable<T> CreateQuery(string entitySetName);
    }
}