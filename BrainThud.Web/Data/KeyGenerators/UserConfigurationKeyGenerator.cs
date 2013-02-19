using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class UserConfigurationKeyGenerator : CardEntityKeyGenerator
    {
        public UserConfigurationKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            IIdentityQueueManager identityQueueManager)
            : base(authenticationHelper, identityQueueManager, CardRowTypes.CONFIGURATION){}
    }
}