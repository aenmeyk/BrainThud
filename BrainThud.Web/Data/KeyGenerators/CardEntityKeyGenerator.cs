using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.KeyGenerators
{
    public abstract class CardEntityKeyGenerator : ICardEntityKeyGenerator
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITableStorageContext tableStorageContext;
        private readonly IUserHelper userHelper;
        private readonly IIdentityQueueManager identityQueueManager;
        private readonly string rowType;

        protected CardEntityKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            ITableStorageContext tableStorageContext,
            IUserHelper userHelper,
            IIdentityQueueManager identityQueueManager,
            string rowType)
        {
            this.authenticationHelper = authenticationHelper;
            this.tableStorageContext = tableStorageContext;
            this.userHelper = userHelper;
            this.identityQueueManager = identityQueueManager;
            this.rowType = rowType;
        }

        public int GeneratedUserId { get; private set; }
        public int GeneratedEntityId { get; private set; }

        public string GenerateRowKey()
        {
            this.GeneratedEntityId = this.identityQueueManager.GetNextIdentity();
            return this.GetRowKey(this.GeneratedEntityId);
        }

        public string GeneratePartitionKey()
        {
            var userConfiguration = this.GetUserConfiguration();
            this.GeneratedUserId = userConfiguration.UserId;
            return string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userConfiguration.UserId);
        }

        private UserConfiguration GetUserConfiguration()
        {
            return this.tableStorageContext.UserConfigurations.GetByNameIdentifier()
                ?? this.userHelper.CreateUserConfiguration();
        }

        public string GetRowKey(int entityId)
        {
            return string.Format("{0}-{1}", this.rowType, entityId);
        }
    }
}