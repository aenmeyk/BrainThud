using System.Collections.Generic;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        private readonly ITableStorageContext<T> tableStorageContext;
        private readonly ITableStorageKeyGenerator keyGenerator;

        public TableStorageRepository(ITableStorageContext<T> tableStorageContext, ITableStorageKeyGenerator keyGenerator)
        {
            this.tableStorageContext = tableStorageContext;
            this.keyGenerator = keyGenerator;
        }

        public void Add(T entity)
        {
            if (string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = this.keyGenerator.GeneratePartitionKey();
            if (string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = this.keyGenerator.GenerateRowKey();
            this.tableStorageContext.AddObject(entity);
        }

        public void Commit()
        {
            this.tableStorageContext.SaveChangesWithRetries();
        }

        public IEnumerable<T> GetAll()
        {
            return this.tableStorageContext.CreateQuery(typeof(T).Name);
        }
    }
}