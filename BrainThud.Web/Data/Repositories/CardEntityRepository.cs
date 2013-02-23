using System;
using System.Linq;
using BrainThud.Core.Data.AzureTableStorage;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.Repositories
{
    public abstract class CardEntityRepository<T> : TableStorageRepository<T>, ICardEntityRepository<T> where T : AzureTableEntity
    {
        protected CardEntityRepository(
            ITableStorageContext tableStorageContext, 
            ICardEntityKeyGenerator keyGenerator,
            string nameIdentifier, 
            string rowKeyPrefix) 
            : base(tableStorageContext)
        {
            this.KeyGenerator = keyGenerator;
            this.RowKeyPrefix = rowKeyPrefix;
            this.NameIdentifier = nameIdentifier;
        }

        protected ICardEntityKeyGenerator KeyGenerator { get; private set; }
        protected string RowKeyPrefix { get; private set; }
        protected string NameIdentifier { get; private set; }

        protected override IQueryable<T> EntitySet
        {
            get
            {
                return base.EntitySet.Where(x =>
                    string.Compare(x.RowKey, this.RowKeyPrefix + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.RowKey, this.RowKeyPrefix + '.', StringComparison.Ordinal) < 0);
            }
        }

        public T GetById(int userId, int entityId)
        {
            var partitionKey = this.KeyGenerator.GetPartitionKey(userId);
            return this.GetByPartitionKey(partitionKey, entityId);
        }

        public T GetByPartitionKey(string partitionKey, int entityId)
        {
            var rowKey = this.KeyGenerator.GetRowKey(entityId);
            return this.Get(partitionKey, rowKey);
        }

        public void DeleteById(int userId, int entityId)
        {
            var partitionKey = this.KeyGenerator.GetPartitionKey(userId);
            var rowKey = this.KeyGenerator.GetRowKey(entityId);
            this.Delete(partitionKey, rowKey);
        }

        public IQueryable<T> GetForUser()
        {
            return this.EntitySet.Where(x =>
                string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0);
        }
    }
}