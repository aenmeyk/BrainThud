using System;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : CardEntityRepository<Card>
    {
        public CardRepository(ITableStorageContext tableStorageContext, string nameIdentifier) 
            : base(tableStorageContext, nameIdentifier, CardRowTypes.CARD) {}

        public override void Add(Card entity, ITableStorageKeyGenerator keyGenerator)
         {
            SetKeyValues(entity, keyGenerator);
            entity.QuizDate = DateTime.UtcNow.AddDays(1).Date;
            var cardKeyGenerator = keyGenerator as ICardEntityKeyGenerator;

            if(cardKeyGenerator != null)
            {
                entity.UserId = cardKeyGenerator.GeneratedUserId;
                entity.EntityId = cardKeyGenerator.GeneratedEntityId;
            }

            this.Add(entity);
         }
    }
}