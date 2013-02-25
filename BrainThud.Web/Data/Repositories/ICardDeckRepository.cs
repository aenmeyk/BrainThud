using BrainThud.Core.Models;

namespace BrainThud.Web.Data.Repositories
{
    public interface ICardDeckRepository : ICardEntityRepository<CardDeck>
    {
        void AddCardToCardDeck(Card card);
        void RemoveCardFromCardDeck(Card card);
    }
}