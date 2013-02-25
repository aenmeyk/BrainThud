using System;
using System.Linq;
using BrainThud.Core.Data.AzureTableStorage;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.Repositories
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : AzureTableEntity
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
            if (string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = keyGenerator.GeneratePartitionKey(this.UserId);
            if (string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = keyGenerator.GenerateRowKey();
        }

        public virtual void Add(T entity)
        {
            entity.CreatedTimestamp = DateTime.UtcNow;
            this.TableStorageContext.AddObject(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity.CreatedTimestamp <= DateTime.MinValue)
            {
                entity.CreatedTimestamp = entity.Timestamp;
            }

            this.TableStorageContext.UpdateObject(entity);
        }

        public void Delete(string partitionKey, string rowKey)
        {
            var entity = this.Get(partitionKey, rowKey);
            this.TableStorageContext.DeleteObject(entity);
        }

        public T Get(string partitionKey, string rowKey)
        {
            return this.Get(partitionKey)
                .Where(x => x.RowKey == rowKey)
                .FirstOrDefault();
        }

        public IQueryable<T> Get(string partitionKey)
        {
            return this.EntitySet.Where(x => x.PartitionKey == partitionKey);
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