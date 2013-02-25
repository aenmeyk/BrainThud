using BrainThud.Core;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardKeyGenerator : CardEntityKeyGenerator
    {
        public CardKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            IIdentityQueueManager identityQueueManager)
            : base(authenticationHelper, identityQueueManager, CardRowTypes.CARD) { }
    }
}