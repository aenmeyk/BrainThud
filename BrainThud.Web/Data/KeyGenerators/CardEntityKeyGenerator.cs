using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.KeyGenerators
{
    public abstract class CardEntityKeyGenerator : ITableStorageKeyGenerator
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITableStorageContext tableStorageContext;
        private readonly IUserHelper userHelper;
        private readonly IIdentityQueueManager identityQueueManager;
        private readonly string rowType;

        // TODO: Test that these are set
        public int UserId { get; private set; }
        public int EntityId { get; private set; }

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

        public string GenerateRowKey()
        {
//            var userConfiguration = this.GetUserConfiguration();
//            this.EntityId = ++userConfiguration.LastUsedId;
//            this.tableStorageContext.UpdateObject(userConfiguration);

            this.EntityId = this.identityQueueManager.GetNextIdentity();

            return this.GetRowKey(this.EntityId);
        }

        public string GeneratePartitionKey()
        {
            var userConfiguration = this.GetUserConfiguration();
            this.UserId = userConfiguration.UserId;
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