using System;
using System.Globalization;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureQueues
{
    public class IdentityQueueManager : IIdentityQueueManager
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly ICloudStorageServices cloudStorageServices;

        public IdentityQueueManager(ITableStorageContextFactory tableStorageContextFactory, ICloudStorageServices cloudStorageServices)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.cloudStorageServices = cloudStorageServices;
        }

        public void Seed()
        {
            var cloudQueue = this.GetQueueReference();
            var identitiesInQueue = this.GetApproximateMessageCount(cloudQueue);
            var identitiesToAdd = ConfigurationSettings.SEED_IDENTITIES - identitiesInQueue;

            if (identitiesToAdd > 0)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CONFIGURATION);
                var masterConfiguration = tableStorageContext.MasterConfigurations.GetOrCreate(Keys.MASTER, Keys.CONFIGURATION);
                var currentMaxIdentity = masterConfiguration.CurrentMaxIdentity;

                for (long i = currentMaxIdentity + 1; i <= currentMaxIdentity + identitiesToAdd; i++)
                {
                    this.AddIdentityToQueue(cloudQueue, i);
                }

                masterConfiguration.CurrentMaxIdentity += identitiesToAdd;
                tableStorageContext.UpdateObject(masterConfiguration);
                tableStorageContext.Commit();
            }
        }

        // TODO: Write wrappers for these classes so they can be tested: 
        //      CloudQueue, CloudQueueMessage, CloudQueueClient

        // TODO: Test this
        public int GetNextIdentity()
        {
            var cloudQueue = this.GetQueueReference();
            var queueMessage = cloudQueue.GetMessage(TimeSpan.FromSeconds(ConfigurationSettings.IDENTITY_QUEUE_VISIBILITY_TIMEOUT_SECONDS));

            // TODO: If the queue is empty, fill it then retry
            if (queueMessage == null) throw new Exception("No identity values remaining in the identity queue.");

            var queueValue = int.Parse(queueMessage.AsString);
            cloudQueue.DeleteMessage(queueMessage);

            return queueValue;
        }

        // Allow these virtual methods to be overridden by a fake IdentityQueueManager for testing
        protected virtual CloudQueue GetQueueReference()
        {
            return this.cloudStorageServices.CloudQueueClient.GetQueueReference(AzureQueueNames.IDENTITY);
        }

        protected virtual void AddIdentityToQueue(CloudQueue cloudQueue, long value)
        {
            cloudQueue.AddMessage(new CloudQueueMessage(value.ToString(CultureInfo.InvariantCulture)));
        }

        protected virtual int GetApproximateMessageCount(CloudQueue cloudQueue)
        {
            cloudQueue.RetrieveApproximateMessageCount();
            return cloudQueue.ApproximateMessageCount ?? 0;
        }
    }
}