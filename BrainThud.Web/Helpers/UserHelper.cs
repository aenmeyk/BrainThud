using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;

        public UserHelper(ITableStorageContextFactory tableStorageContextFactory)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
        }

        public UserConfiguration CreateUserConfiguration(string nameIdentifier)
        {
            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
            var masterConfiguration = tableStorageContext.MasterConfigurations
                .GetOrCreate(PartitionKeys.MASTER, EntityNames.CONFIGURATION);

            var configuration = new UserConfiguration
            {
                PartitionKey = nameIdentifier, 
                RowKey = EntityNames.CONFIGURATION,
                UserId = ++masterConfiguration.LastUsedUserId
            };

            tableStorageContext.Configurations.Add(configuration);
            tableStorageContext.MasterConfigurations.Update(masterConfiguration);
            tableStorageContext.Commit();

            return configuration;
        }
    }
}
