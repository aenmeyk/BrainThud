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
            var cardKeyGenerator = keyGenerator as CardEntityKeyGenerator;

            if(cardKeyGenerator != null)
            {
                entity.UserId = cardKeyGenerator.UserId;
                entity.EntityId = cardKeyGenerator.EntityId;
            }

            this.Add(entity);
         }
    }
}