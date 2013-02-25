using BrainThud.Core;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardDeckKeyGenerator : CardEntityKeyGenerator
    {
        public CardDeckKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            IIdentityQueueManager identityQueueManager)
            : base(authenticationHelper, identityQueueManager, CardRowTypes.CARD_DECK) { }
    }
}