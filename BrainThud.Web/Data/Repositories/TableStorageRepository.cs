using System;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.Repositories
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        private readonly ITableStorageContext tableStorageContext;

        public TableStorageRepository(ITableStorageContext tableStorageContext)
        {
            this.tableStorageContext = tableStorageContext;
        }

        protected virtual IQueryable<T> EntitySet { get { return this.tableStorageContext.CreateQuery<T>(); } }

        public virtual void Add(T entity, ITableStorageKeyGenerator keyGenerator)
        {
            SetKeyValues(entity, keyGenerator);
            this.Add(entity);
        }

        protected static void SetKeyValues(T entity, ITableStorageKeyGenerator keyGenerator)
        {
            if(string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = keyGenerator.GeneratePartitionKey();
            if(string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = keyGenerator.GenerateRowKey();
        }

        public void Add(T entity)
        {
            this.tableStorageContext.AddObject(entity);
        }

        public void Update(T entity)
        {
            this.tableStorageContext.UpdateObject(entity);
        }

        public void Delete(string partitionKey, string rowKey)
        {
            var entity = this.Get(partitionKey, rowKey);
            this.tableStorageContext.DeleteObject(entity);
        }

        public T Get(string partitionKey, string rowKey)
        {
// ReSharper disable ReplaceWithSingleCallToFirstOrDefault
            return this.EntitySet
                .Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey)
                .FirstOrDefault();
// ReSharper restore ReplaceWithSingleCallToFirstOrDefault
        }

        public IQueryable<T> GetAll()
        {
            return this.EntitySet;
        }

        public T GetOrCreate(string partitionKey, string rowKey)
        {
            var entity = this.Get(partitionKey, rowKey);

            if (entity == null)
            {
                entity = Activator.CreateInstance<T>();
                entity.PartitionKey = partitionKey;
                entity.RowKey = rowKey;

                this.Add(entity);
            }

            return entity;
        }
    }
}