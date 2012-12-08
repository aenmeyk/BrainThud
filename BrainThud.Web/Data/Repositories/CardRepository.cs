﻿using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : TableStorageRepository<Card>, ICardRepository
    {
        private readonly string nameIdentifier;

        public CardRepository(ITableStorageContext tableStorageContext, string nameIdentifier) 
            : base(tableStorageContext)
        {
            this.nameIdentifier = nameIdentifier;
        }

        public Card GetCard(int userId, int cardId)
        {
            var partitionKey = this.GetPartitionKey(userId);
            var rowKey = GetRowKey(cardId);

            return this.Get(partitionKey, rowKey);
        }

        public void DeleteCard(int userId, int cardId)
        {
            var partitionKey = this.GetPartitionKey(userId);
            var rowKey = GetRowKey(cardId);

            this.Delete(partitionKey, rowKey);
        }

        private static string GetRowKey(int cardId)
        {
            return string.Format("{0}-{1}", CardRowTypes.CARD, cardId);
        }

        private string GetPartitionKey(int userId)
        {
            return string.Format("{0}-{1}", this.nameIdentifier, userId);
        }
    }
}