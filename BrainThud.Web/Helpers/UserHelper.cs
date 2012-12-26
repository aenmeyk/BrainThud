using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Model;

namespace BrainThud.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IIdentityQueueManager identityQueueManager;

        public UserHelper(
            IAuthenticationHelper authenticationHelper, 
            IIdentityQueueManager identityQueueManager)
        {
            this.authenticationHelper = authenticationHelper;
            this.identityQueueManager = identityQueueManager;
        }

        public UserConfiguration CreateUserConfiguration()
        {
            var userId = this.identityQueueManager.GetNextIdentity();
            var configurationId = this.identityQueueManager.GetNextIdentity();

            var configuration = new UserConfiguration
            {
                PartitionKey = string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userId),
                RowKey = string.Format("{0}-{1}", CardRowTypes.CONFIGURATION, configurationId),
                UserId = userId
            };

            return configuration;
        }
    }
}
