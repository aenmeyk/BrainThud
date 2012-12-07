using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

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
            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
            var nameIdentifier = this.authenticationHelper.NameIdentifier;
            var configuration = this.tableStorageContext.Configurations.Get(nameIdentifier, EntityNames.CONFIGURATION) 
                ?? this.userHelper.CreateUserConfiguration(nameIdentifier);

            var cardId = ++configuration.LastUsedId;
            this.tableStorageContext.UpdateObject(configuration);

            return cardId.ToString();
        }

        public string GeneratePartitionKey()
        {
            return this.authenticationHelper.NameIdentifier;
        }
    }
}