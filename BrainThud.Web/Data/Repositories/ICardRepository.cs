using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public interface ICardRepository : ITableStorageRepository<Card>
    {
        Card GetCard(int userId, int cardId);
        void DeleteCard(int userId, int cardId);
    }
}