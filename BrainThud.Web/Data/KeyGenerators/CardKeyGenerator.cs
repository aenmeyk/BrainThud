using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardKeyGenerator : ITableStorageKeyGenerator
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITableStorageContext tableStorageContext;

        public CardKeyGenerator(IAuthenticationHelper authenticationHelper, ITableStorageContext tableStorageContext)
        {
            this.authenticationHelper = authenticationHelper;
            this.tableStorageContext = tableStorageContext;
        }

        public string GenerateRowKey()
        {
            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
            var configuration = this.tableStorageContext.Configurations.Get(this.authenticationHelper.NameIdentifier, EntityNames.CONFIGURATION);
            var cardId = ++configuration.LastUsedId;

            return cardId.ToString();
        }

        public string GeneratePartitionKey()
        {
            return this.authenticationHelper.NameIdentifier;
        }
    }
}