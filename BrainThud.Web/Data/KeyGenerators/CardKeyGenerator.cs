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

            return string.Format("{0}-{1}", CardRowTypes.CARD, cardId.ToString());
        }

        public string GeneratePartitionKey()
        {
            var userConfiguration = this.GetUserConfiguration();
            return string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userConfiguration.UserId);
        }

        private UserConfiguration GetUserConfiguration()
        {
            var nameIdentifier = this.authenticationHelper.NameIdentifier;
            var configuration = this.tableStorageContext.UserConfigurations.Get(nameIdentifier, EntityNames.CONFIGURATION)
                                ?? this.userHelper.CreateUserConfiguration(nameIdentifier);

            return configuration;
        }
    }
}