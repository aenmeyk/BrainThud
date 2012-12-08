using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : TableStorageRepository<Card>
    {
        private readonly string nameIdentifier;

        public CardRepository(
            ITableStorageContext tableStorageContext, 
            string nameIdentifier) 
            : base(tableStorageContext)
        {
            this.nameIdentifier = nameIdentifier;
        }

        public Card Get(string userId, string cardId)
        {
            var partitionKey = string.Format("{0}-{1}", nameIdentifier, userId);
            var rowKey = string.Format("{0}-{1}", CardRowTypes.CARD, cardId);

            var firstOrDefault = this.EntitySet.Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey).FirstOrDefault();
            return firstOrDefault;
        }
    }
}