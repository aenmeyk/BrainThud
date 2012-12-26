using System;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.Repositories
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        private readonly Lazy<int> userId;

        public TableStorageRepository(ITableStorageContext tableStorageContext)
        {
            this.TableStorageContext = tableStorageContext;
            this.userId = new Lazy<int>(() => this.TableStorageContext.UserConfigurations.GetByNameIdentifier().UserId);
        }

        protected ITableStorageContext TableStorageContext { get; private set; }
        protected virtual IQueryable<T> EntitySet { get { return this.TableStorageContext.CreateQuery<T>(); } }
        protected int UserId { get { return this.userId.Value; } }

        protected void SetKeyValues(T entity, ITableStorageKeyGenerator keyGenerator)
        {
            if(string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = keyGenerator.GeneratePartitionKey(this.UserId);
            if(string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = keyGenerator.GenerateRowKey();
        }

        public virtual void Add(T entity)
        {
            this.TableStorageContext.AddObject(entity);
        }

        public void Update(T entity)
        {
            this.TableStorageContext.UpdateObject(entity);
        }

        public void Delete(string partitionKey, string rowKey)
        {
            var entity = this.Get(partitionKey, rowKey);
            this.TableStorageContext.DeleteObject(entity);
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