using System;
using System.Linq;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

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
            return this.EntitySet
                .Where(x => x.QuizYear == year && x.QuizMonth == month && x.QuizDay == day)
                .Where(x =>
                    string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0);
        }

        public void DeleteByCardId(int cardId)
        {
            var quizResults = this.EntitySet
                .Where(x => x.CardId == cardId)
                .Where(x =>
                    string.Compare(x.RowKey, CardRowTypes.QUIZ_RESULT + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.RowKey, CardRowTypes.QUIZ_RESULT + '.', StringComparison.Ordinal) < 0)
                .Where(x =>
                    string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                    && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0);

            foreach(var quizResult in quizResults)
            {
                this.Delete(quizResult.PartitionKey, quizResult.RowKey);
            }
        }
    }
}