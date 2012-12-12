using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IIdentityQueueManager identityQueueManager;

        public UserHelper(
            ITableStorageContextFactory tableStorageContextFactory, 
            IAuthenticationHelper authenticationHelper, 
            IIdentityQueueManager identityQueueManager)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.authenticationHelper = authenticationHelper;
            this.identityQueueManager = identityQueueManager;
        }

        public UserConfiguration CreateUserConfiguration()
        {
            var userId = this.identityQueueManager.GetNextIdentity();
            var configurationId = this.identityQueueManager.GetNextIdentity();
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);

            var configuration = new UserConfiguration
            {
                PartitionKey = string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userId),
                RowKey = string.Format("{0}-{1}", CardRowTypes.CONFIGURATION, configurationId),
                UserId = userId
            };

            tableStorageContext.UserConfigurations.Add(configuration);
            tableStorageContext.Commit();

            return configuration;
        }
    }
}
