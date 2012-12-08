using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : CardEntityRepository<Card>
    {
        public CardRepository(ITableStorageContext tableStorageContext, string nameIdentifier) 
            : base(tableStorageContext, nameIdentifier, CardRowTypes.CARD) {}
    }
}