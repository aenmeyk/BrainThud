using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.Repositories
{
    public abstract class CardEntityRepository<T> : TableStorageRepository<T>, ICardRepository<T> where T : TableServiceEntity
    {
        private readonly string rowKeyPrefix;
        protected string NameIdentifier { get; private set; }

        protected CardEntityRepository(ITableStorageContext tableStorageContext, string nameIdentifier, string rowKeyPrefix) 
            : base(tableStorageContext)
        {
            this.rowKeyPrefix = rowKeyPrefix;
            this.NameIdentifier = nameIdentifier;
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

        private string GetRowKey(int cardId)
        {
            return string.Format("{0}-{1}", this.rowKeyPrefix, cardId);
        }

        private string GetPartitionKey(int userId)
        {
            return string.Format("{0}-{1}", this.NameIdentifier, userId);
        }
    }
}