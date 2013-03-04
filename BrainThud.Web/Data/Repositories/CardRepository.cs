using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Extensions;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : CardEntityRepository<Card>, ICardRepository
    {
        public CardRepository(
            ITableStorageContext tableStorageContext,
            ICardEntityKeyGenerator cardKeyGenerator,
            string nameIdentifier)
            : base(tableStorageContext, cardKeyGenerator, nameIdentifier, CardRowTypes.CARD) { }

        public void Add(Card entity, DateTime quizDate)
        {
            this.SetKeyValues(entity, this.KeyGenerator);
            entity.QuizDate = quizDate;
            entity.UserId = this.UserId;
            entity.EntityId = this.KeyGenerator.GeneratedEntityId;
            GenerateSlugs(entity);

            base.Add(entity);
        }

        private static void GenerateSlugs(Card entity)
        {
            entity.DeckNameSlug = entity.DeckName.GenerateSlug(ConfigurationSettings.CARD_DECK_SLUG_LENGTH);
            entity.CardSlug = entity.Question.GenerateSlug(ConfigurationSettings.CARD_SLUG_LENGTH);
        }

        public override void Update(Card entity)
        {
            GenerateSlugs(entity);
            base.Update(entity);
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