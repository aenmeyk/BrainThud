using System;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.Repositories
{
    public abstract class CardEntityRepository<T> : TableStorageRepository<T>, ICardEntityRepository<T> where T : TableServiceEntity
    {
        protected string RowKeyPrefix { get; private set; }
        protected string NameIdentifier { get; private set; }

        protected CardEntityRepository(
            ITableStorageContext tableStorageContext, 
            string nameIdentifier, 
            string rowKeyPrefix) 
            : base(tableStorageContext)
        {
            this.RowKeyPrefix = rowKeyPrefix;
            this.NameIdentifier = nameIdentifier;
        }

        protected override IQueryable<T> EntitySet
        {
            get
            {
                return base.EntitySet.Where(x =>
                    string.Compare(x.RowKey, this.RowKeyPrefix + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.RowKey, this.RowKeyPrefix + '.', StringComparison.Ordinal) < 0);
            }
        }

        public T GetById(int userId, int cardId)
        {
            var partitionKey = this.GetPartitionKey(userId);
            var rowKey = GetRowKey(cardId);

            return this.Get(partitionKey, rowKey);
        }

        public void DeleteById(int userId, int cardId)
        {
            var partitionKey = this.GetPartitionKey(userId);
            var rowKey = GetRowKey(cardId);

            this.Delete(partitionKey, rowKey);
        }

        public IQueryable<T> GetAllForUser()
        {
            return this.EntitySet.Where(x =>
                string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0);
        }

        private string GetRowKey(int cardId)
        {
            return string.Format("{0}-{1}", this.RowKeyPrefix, cardId);
        }

        private string GetPartitionKey(int userId)
        {
            return string.Format("{0}-{1}", this.NameIdentifier, userId);
        }
    }
}