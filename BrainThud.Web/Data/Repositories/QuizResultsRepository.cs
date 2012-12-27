using System;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class QuizResultsRepository : CardEntityRepository<QuizResult>, IQuizResultsRepository
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

        public IQueryable<QuizResult> GetForQuiz(int year, int month, int day)
        {
            var quizDate = new DateTime(year, month, day);
            return this.EntitySet
                .Where(x => x.QuizDate == quizDate)
                .Where(x =>
                    string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0);
        }
    }
}