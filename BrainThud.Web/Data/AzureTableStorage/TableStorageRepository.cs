using System;
using System.Linq;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        private readonly ITableStorageContext tableStorageContext;
        private readonly IAuthenticationHelper authenticationHelper;

        public TableStorageRepository(ITableStorageContext tableStorageContext, IAuthenticationHelper authenticationHelper)
        {
            this.tableStorageContext = tableStorageContext;
            this.authenticationHelper = authenticationHelper;
        }

        private IQueryable<T> entitySet
        {
            get
            {
                var queryable = this.tableStorageContext.CreateQuery<T>();

                // TODO: Find a better way of limiting user results
                return queryable.Where(x =>
                    string.Compare(x.PartitionKey, this.authenticationHelper.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.PartitionKey, this.authenticationHelper.NameIdentifier + '.', StringComparison.Ordinal) < 0 
                    || x.PartitionKey == PartitionKeys.MASTER);
            }
        }

        public void Add(T entity, ITableStorageKeyGenerator keyGenerator)
        {
            if (string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = keyGenerator.GeneratePartitionKey();
            if (string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = keyGenerator.GenerateRowKey();
            this.Add(entity);
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
            // TODO: Make the "string.Compare" stuff custom to and individual entity repository type
            return this.entitySet
                .Where(x => string.Compare(x.PartitionKey, partitionKey + '-', StringComparison.Ordinal) >= 0 && string.Compare(x.PartitionKey, partitionKey + '.', StringComparison.Ordinal) < 0 && x.RowKey == rowKey)
                .FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return this.entitySet;
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