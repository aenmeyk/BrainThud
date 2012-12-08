using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class QuizResultsRepository : CardEntityRepository<QuizResult>
    {
        public QuizResultsRepository(ITableStorageContext tableStorageContext, string nameIdentifier) 
            : base(tableStorageContext, nameIdentifier, CardRowTypes.QUIZ_RESULT) {}

        // TODO: Write tests for this
        public override void Add(QuizResult entity, ITableStorageKeyGenerator keyGenerator)
        {
            SetKeyValues(entity, keyGenerator);
            var cardKeyGenerator = keyGenerator as CardEntityKeyGenerator;

            if (cardKeyGenerator != null)
            {
                entity.UserId = cardKeyGenerator.UserId;
                entity.EntityId = cardKeyGenerator.EntityId;
            }

            this.Add(entity);
        }
    }
}