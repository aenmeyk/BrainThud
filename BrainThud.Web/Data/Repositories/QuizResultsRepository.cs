using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class QuizResultsRepository : CardEntityRepository<QuizResult>
    {
        public QuizResultsRepository(
            ITableStorageContext tableStorageContext,
            ICardEntityKeyGenerator quizResultKeyGenerator, 
            string nameIdentifier)
            : base(tableStorageContext, quizResultKeyGenerator, nameIdentifier, CardRowTypes.QUIZ_RESULT) { }

        public override void Add(QuizResult entity)
        {
            this.SetKeyValues(entity, this.KeyGenerator);
            entity.UserId = this.UserId;
            entity.EntityId = this.KeyGenerator.GeneratedEntityId;

            base.Add(entity);
        }
    }
}