using System;
using System.Globalization;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Extensions;
using System.Linq;

namespace BrainThud.Web.Data.Repositories
{
    public class CardDeckRepository : CardEntityRepository<CardDeck>, ICardDeckRepository
    {
        public CardDeckRepository(ITableStorageContext tableStorageContext, ICardEntityKeyGenerator cardKeyGenerator, string nameIdentifier)
            : base(tableStorageContext, cardKeyGenerator, nameIdentifier, CardRowTypes.CARD_DECK) { }

        public void AddCardToCardDeck(Card card)
        {
            if (string.IsNullOrEmpty(card.DeckName)) return;
            var existingCardDeck = this.GetExistingCardDeck(card);
            var cardId = card.EntityId.ToString(CultureInfo.InvariantCulture);

            // If a card deck with this name doesn't already exist, then create it
            if (existingCardDeck == null)
            {
                var cardDeck = new CardDeck
                {
                    PartitionKey = this.KeyGenerator.GeneratePartitionKey(card.UserId),
                    RowKey = this.KeyGenerator.GenerateRowKey(),
                    DeckName = card.DeckName,
                    DeckNameSlug = card.DeckName.GenerateSlug(ConfigurationSettings.CARD_DECK_SLUG_LENGTH),
                    UserId = this.UserId,
                    CardIds = cardId // Add the first of a comma-separated list of card ids
                };

                this.Add(cardDeck);
            }
            else
            {
                var existingCardIds = existingCardDeck.CardIds.Split(',');

                if (!existingCardIds.Contains(cardId))
                {
                    existingCardDeck.CardIds = string.Join(",", existingCardDeck.CardIds, cardId);
                }

                this.Update(existingCardDeck);
            }
        }

        public void RemoveCardFromCardDeck(Card card)
        {
            var cardDeck = this.GetExistingCardDeck(card);

            if (cardDeck != null)
            {
                var cardIds = cardDeck.CardIds.Split(',').ToList();
                cardIds.Remove(card.EntityId.ToString(CultureInfo.InvariantCulture));

                if(cardIds.Count == 0 || string.IsNullOrEmpty(cardDeck.CardIds))
                {
                    this.TableStorageContext.DeleteObject(cardDeck);
                }
                else
                {
                    cardDeck.CardIds = string.Join(",", cardIds);
                    this.Update(cardDeck);
                }
            }
        }

        private CardDeck GetExistingCardDeck(Card card)
        {
            return card.DeckName == null
                ? null
                : this.GetForUser().Where(x => x.DeckName == card.DeckName).FirstOrDefault();
        }
    }
}