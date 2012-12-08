using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardKeyGenerator : CardEntityKeyGenerator
    {
        public CardKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            ITableStorageContext tableStorageContext,
            IUserHelper userHelper)
            : base(authenticationHelper, tableStorageContext, userHelper, CardRowTypes.CARD) {}
    }
}