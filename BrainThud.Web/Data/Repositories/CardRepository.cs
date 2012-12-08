using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Data.Repositories
{
    public class CardRepository : CardEntityRepository
    {
        public CardRepository(ITableStorageContext tableStorageContext, string nameIdentifier) 
            : base(tableStorageContext, nameIdentifier, CardRowTypes.CARD) {}
    }
}