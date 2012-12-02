using System.Linq;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        private readonly ITableStorageContext tableStorageContext;

        public TableStorageRepository(ITableStorageContext tableStorageContext)
        {
            this.tableStorageContext = tableStorageContext;
        }

        private IQueryable<T> entitySet
        {
            get
            {
                var queryable = this.tableStorageContext.CreateQuery<T>();
#if DEBUG
                if(typeof(ITestData).IsAssignableFrom(typeof(T)))
                {
                    return queryable.Where(x => ((ITestData)x).IsTestData);
                }
#endif
                return queryable;
            }
        }

        public void Add(T entity, ITableStorageKeyGenerator keyGenerator)
        {
            if (string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = keyGenerator.GeneratePartitionKey();
            if (string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = keyGenerator.GenerateRowKey();

#if DEBUG
            var mockable = entity as ITestData;
            if(mockable != null) mockable.IsTestData = true;
#endif

            this.tableStorageContext.AddObject(entity);
        }

        public void Update(T entity)
        {
            this.tableStorageContext.UpdateObject(entity);
        }

        public void Delete(string partitionKey, string rowKey)
        {
            var item = this.Get(partitionKey, rowKey);
            this.tableStorageContext.DeleteObject(item);
        }

        public T Get(string partitionKey, string rowKey)
        {
            return this.entitySet.Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey).First();
        }

        public IQueryable<T> GetAll()
        {
            return this.entitySet;
        }
    }
}