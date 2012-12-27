using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Calendars;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : CardEntityRepository<Card>, ICardRepository
    {
        private readonly IQuizCalendar quizCalendar;

        public CardRepository(
            ITableStorageContext tableStorageContext, 
            ICardEntityKeyGenerator cardKeyGenerator, 
            IQuizCalendar quizCalendar, 
            string nameIdentifier)
            : base(tableStorageContext, cardKeyGenerator, nameIdentifier, CardRowTypes.CARD)
        {
            this.quizCalendar = quizCalendar;
        }

        public override void Add(Card entity)
        {
            this.SetKeyValues(entity, this.KeyGenerator);
            entity.QuizDate = DateTime.UtcNow.AddDays(this.quizCalendar[0]).Date;
            entity.UserId = this.UserId;
            entity.EntityId = this.KeyGenerator.GeneratedEntityId;

            base.Add(entity);
        }

        public IEnumerable<Card> GetForQuizResults(IEnumerable<QuizResult> quizResults)
        {
            var quizResultCardIds = quizResults.Select(qr => qr.CardId);
            var results = this.EntitySet
                .Where(x =>
                    string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0 &&
                    string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0)
                .ToList();

            return results.Where(x => quizResultCardIds.Contains(x.EntityId));
        }
    }
}