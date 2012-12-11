using System;
using System.Globalization;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureQueues
{
    public class IdentityQueueManager : IIdentityQueueManager
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly IIdentityCloudQueue queue;

        public IdentityQueueManager(ITableStorageContextFactory tableStorageContextFactory, IIdentityCloudQueue queue)
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

        public int GetNextIdentity()
        {
            var visibilityTimeout = TimeSpan.FromSeconds(ConfigurationSettings.IDENTITY_QUEUE_VISIBILITY_TIMEOUT_SECONDS);
            var queueMessage = this.queue.GetMessage(visibilityTimeout);

            // TODO: If the queue is empty, fill it then retry
            if (queueMessage == null) throw new Exception("No identity values remaining in the identity queue.");

            var queueValue = int.Parse(queueMessage.AsString);
            this.queue.DeleteMessage(queueMessage);

            return queueValue;
        }
    }
}