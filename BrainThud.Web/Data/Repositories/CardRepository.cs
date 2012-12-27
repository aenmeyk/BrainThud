using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
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

        public IQueryable<Card> GetForQuizResults(IEnumerable<QuizResult> quizResults)
        {
            var predicate = PredicateBuilder.False<Card>();

            foreach(var quizResult in quizResults)
            {
                var cardRowKey = this.KeyGenerator.GetRowKey(quizResult.CardId);
                predicate = predicate.Or(x => x.RowKey == cardRowKey);
            }

            return this.EntitySet
                .Where(predicate)
                .Where(x =>
                       string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                       && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0);
        }
    }
}