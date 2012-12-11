using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardKeyGenerator : CardEntityKeyGenerator
    {
        public CardKeyGenerator(
            IAuthenticationHelper authenticationHelper, 
            ITableStorageContext tableStorageContext, 
            IUserHelper userHelper, 
            IIdentityQueueManager identityQueueManager)
            : base(authenticationHelper, tableStorageContext, userHelper, identityQueueManager, CardRowTypes.CARD) { }
    }
}