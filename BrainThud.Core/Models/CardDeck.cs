using BrainThud.Core.Data.AzureTableStorage;

namespace BrainThud.Core.Models
{
    public class CardDeck :  AzureTableEntity
    {
        public CardDeck()
        {
            this.CardIds = string.Empty;
        }

        public string DeckName { get; set; }
        public string DeckNameSlug { get; set; }
        public int UserId { get; set; }
        public string CardIds { get; set; }
    }
}
