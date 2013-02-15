using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class CloudStorageServices : ICloudStorageServices
    {
        private readonly Lazy<CloudStorageAccount> lazyCloudStorageAccount;
        private readonly Lazy<CloudTableClient> lazyCloudTableClient;
        private readonly Lazy<CloudQueueClient> lazyCloudQueueClient;

        public CloudStorageServices()
        {
            this.lazyCloudStorageAccount = new Lazy<CloudStorageAccount>(() =>
            {
                var connectionString = ConfigurationManager.AppSettings[ConfigurationSettings.AZURE_STORAGE];
                return CloudStorageAccount.Parse(connectionString);
            });

            this.lazyCloudTableClient = new Lazy<CloudTableClient>(() => this.CloudStorageAccount.CreateCloudTableClient());
            this.lazyCloudQueueClient = new Lazy<CloudQueueClient>(() => this.CloudStorageAccount.CreateCloudQueueClient());
        }

        public CloudStorageAccount CloudStorageAccount { get { return this.lazyCloudStorageAccount.Value; } }
        public CloudTableClient CloudTableClient { get { return this.lazyCloudTableClient.Value; } }
        public CloudQueueClient CloudQueueClient { get { return this.lazyCloudQueueClient.Value; } }

        public void CreateTablesIfNotCreated()
        {
            var cardTable = this.CloudTableClient.GetTableReference(AzureTableNames.CARD);
            cardTable.CreateIfNotExists();

            var configurationTable = this.CloudTableClient.GetTableReference(AzureTableNames.CONFIGURATION);
            configurationTable.CreateIfNotExists();
        }

        public void CreateQueusIfNotCreated()
        {
            var cloudQueue = lazyCloudQueueClient.Value.GetQueueReference(AzureQueueNames.IDENTITY);
            cloudQueue.CreateIfNotExists();
        }
    }
}