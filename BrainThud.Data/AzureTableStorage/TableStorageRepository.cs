using System.Collections.Generic;
using System.Linq;
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

        private IQueryable<T> entitySet
        {
            get { return this.tableStorageContext.CreateQuery(typeof(T).Name); }
        }

        public void Add(T entity)
        {
            if (string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = this.keyGenerator.GeneratePartitionKey();
            if (string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = this.keyGenerator.GenerateRowKey();
            this.tableStorageContext.AddObject(entity);
        }

        public void Update(T entity)
        {
            this.tableStorageContext.UpdateObject(entity);
        }

        public void Delete(string rowKey)
        {
            var item = this.Get(rowKey);
            this.tableStorageContext.DeleteObject(item);
        }

        public T Get(string rowKey)
        {
            return this.entitySet.Where(x => x.PartitionKey == Keys.TEMP_PARTITION_KEY && x.RowKey == rowKey).First();
        }

        public IQueryable<T> GetAll()
        {
            return this.entitySet;
        }

        public void Commit()
        {
            this.tableStorageContext.SaveChangesWithRetries();
        }
    }
}