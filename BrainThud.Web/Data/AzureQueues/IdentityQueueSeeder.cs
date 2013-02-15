using System.Globalization;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace BrainThud.Web.Data.AzureQueues
{
    public class IdentityQueueSeeder : IIdentityQueueSeeder
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly IIdentityCloudQueue queue;

        public IdentityQueueSeeder(ITableStorageContextFactory tableStorageContextFactory, IIdentityCloudQueue queue)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.queue = queue;
        }

        public void Seed()
        {
            var identitiesInQueue = this.queue.RetrieveApproximateMessageCount();
            var identitiesToAdd = ConfigurationSettings.SEED_IDENTITIES - identitiesInQueue;

            if(identitiesToAdd > 0)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CONFIGURATION);
                var masterConfiguration = tableStorageContext.MasterConfigurations.GetOrCreate(Keys.MASTER, Keys.CONFIGURATION);
                var currentMaxIdentity = masterConfiguration.CurrentMaxIdentity;

                for(var i = currentMaxIdentity + 1; i <= currentMaxIdentity + identitiesToAdd; i++)
                {
                    var identityValue = i.ToString(CultureInfo.InvariantCulture);
                    this.queue.AddMessage(new CloudQueueMessage(identityValue));
                }

                masterConfiguration.CurrentMaxIdentity += identitiesToAdd;
                tableStorageContext.UpdateObject(masterConfiguration);
                tableStorageContext.Commit();
            }
        }
    }
}