using System;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : CardEntityRepository<Card>
    {
        public CardRepository(
            ITableStorageContext tableStorageContext, 
            ICardEntityKeyGenerator cardKeyGenerator, 
            string nameIdentifier)
            : base(tableStorageContext, cardKeyGenerator, nameIdentifier, CardRowTypes.CARD) { }

        public override void Add(Card entity)
         {
             this.SetKeyValues(entity, this.KeyGenerator);
             entity.QuizDate = DateTime.UtcNow.AddDays(1).Date;
            entity.UserId = this.UserId;
            entity.EntityId = this.KeyGenerator.GeneratedEntityId;

            base.Add(entity);
         }
    }
}