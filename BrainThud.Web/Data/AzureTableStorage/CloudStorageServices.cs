using System;
using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
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

        public CloudStorageAccount CloudStorageAccount { get { return this.lazyCloudStorageAccount.Value; } }

        public void CreateTablesIfNotCreated()
        {
            var cloudTableClient = this.lazyCloudTableClient.Value;
            cloudTableClient.CreateTableIfNotExist(AzureTableNames.CARD);
            cloudTableClient.CreateTableIfNotExist(AzureTableNames.CONFIGURATION);
        }

        public void SetConfigurationSettingPublisher()
        {
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                string connectionString;

                if (RoleEnvironment.IsAvailable)
                {
                    connectionString = RoleEnvironment.GetConfigurationSettingValue(configName);
                }
                else
                {
                    connectionString = ConfigurationManager.AppSettings[configName];
                }

                configSetter(connectionString);
            });
        }
    }
}