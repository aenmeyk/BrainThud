using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardKeyGenerator : ITableStorageKeyGenerator
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITableStorageContext tableStorageContext;
        private readonly IUserHelper userHelper;

        public CardKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            ITableStorageContext tableStorageContext,
            IUserHelper userHelper)
        {
            this.authenticationHelper = authenticationHelper;
            this.tableStorageContext = tableStorageContext;
            this.userHelper = userHelper;
        }

        public string GenerateRowKey()
        {
            var userConfiguration = this.GetUserConfiguration();
            var cardId = ++userConfiguration.LastUsedId;
            this.tableStorageContext.UpdateObject(userConfiguration);

            return this.GetRowKey(cardId);
        }

        public string GeneratePartitionKey()
        {
            var userConfiguration = this.GetUserConfiguration();
            return string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userConfiguration.UserId);
        }

        private UserConfiguration GetUserConfiguration()
        {
            return this.tableStorageContext.UserConfigurations.GetByNameIdentifier()
                ?? this.userHelper.CreateUserConfiguration();
        }

        public string GetRowKey(int cardId)
        {
            return string.Format("{0}-{1}", CardRowTypes.CARD, cardId);
        }
    }
}