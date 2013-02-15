using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ICloudStorageServices 
    {
        CloudStorageAccount CloudStorageAccount { get; }
        CloudQueueClient CloudQueueClient { get; }
        void CreateTablesIfNotCreated();
        void CreateQueusIfNotCreated();
    }
}