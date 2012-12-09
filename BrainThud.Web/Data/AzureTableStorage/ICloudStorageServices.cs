using Microsoft.WindowsAzure;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ICloudStorageServices 
    {
        CloudStorageAccount CloudStorageAccount { get; }
        void CreateTablesIfNotCreated();
        void SetConfigurationSettingPublisher();
    }
}