using BrainThud.Core.Data.AzureTableStorage;
using System.Linq;

namespace BrainThud.Core.Models
{
    public class CardDeck : AzureTableEntity
    {
        private string cardIds;

        public CardDeck()
        {
            this.CardIds = string.Empty;
        }

        public string DeckName { get; set; }
        public string DeckNameSlug { get; set; }
        public int UserId { get; set; }
        public int CardCount { get; set; }

        public string CardIds
        {
            get { return this.cardIds; }
            set
            {
                if (value != this.cardIds)
                {
                    this.cardIds = value;
                    this.CardCount = string.IsNullOrWhiteSpace(this.cardIds)
                        ? 0
                        : this.cardIds.Split(',').Count();
                }
            }
        }
    }
}
