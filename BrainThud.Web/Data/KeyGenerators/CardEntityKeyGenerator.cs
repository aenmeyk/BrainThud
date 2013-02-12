using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.KeyGenerators
{
    public abstract class CardEntityKeyGenerator : ICardEntityKeyGenerator
    {
        private readonly IAuthenticationHelper authenticationHelper;

        protected CardEntityKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            IIdentityQueueManager identityQueueManager,
            string rowType)
        {
            this.authenticationHelper = authenticationHelper;
            this.IdentityQueueManager = identityQueueManager;
            this.RowType = rowType;
        }

        protected IIdentityQueueManager IdentityQueueManager { get; private set; }
        protected string RowType { get; private set; }
        public int GeneratedEntityId { get; protected set; }

        public string GenerateRowKey()
        {
            this.GeneratedEntityId = this.IdentityQueueManager.GetNextIdentity();
            return this.GetRowKey(this.GeneratedEntityId);
        }

        public string GeneratePartitionKey(int userId)
        {
            return this.GetPartitionKey(userId);
        }

        public string GetPartitionKey(int userId)
        {
            return string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userId);
        }

        public string GetRowKey(int entityId)
        {
            return string.Format("{0}-{1}", this.RowType, entityId);
        }
    }
}