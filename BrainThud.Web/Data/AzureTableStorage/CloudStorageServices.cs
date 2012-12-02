using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class CloudStorageServices : ICloudStorageServices
    {
        private readonly Lazy<CloudStorageAccount> lazyCloudStorageAccount;
        private readonly Lazy<CloudTableClient> lazyCloudTableClient;

        public CloudStorageServices()
        {
            this.lazyCloudStorageAccount = new Lazy<CloudStorageAccount>(() => CloudStorageAccount.FromConfigurationSetting(ConfigurationSettings.AZURE_STORAGE));
            this.lazyCloudTableClient = new Lazy<CloudTableClient>(() => this.CloudStorageAccount.CreateCloudTableClient());
        }

        public CloudStorageAccount CloudStorageAccount
        {
            get { return this.lazyCloudStorageAccount.Value; }
        }

        public void CreateTableIfNotExist(string entitySetName)
        {
            this.lazyCloudTableClient.Value.CreateTableIfNotExist(entitySetName);
        }
    }
}