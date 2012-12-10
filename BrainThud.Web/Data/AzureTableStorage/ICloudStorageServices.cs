using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ICloudStorageServices 
    {
        CloudStorageAccount CloudStorageAccount { get; }
        CloudQueueClient CloudQueueClient { get; }
        void CreateTablesIfNotCreated();
        void SetConfigurationSettingPublisher();
        void CreateQueusIfNotCreated();
    }
}