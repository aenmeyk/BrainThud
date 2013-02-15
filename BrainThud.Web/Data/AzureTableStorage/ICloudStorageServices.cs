using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ICloudStorageServices 
    {
        CloudStorageAccount CloudStorageAccount { get; }
        CloudQueueClient CloudQueueClient { get; }
        CloudTableClient CloudTableClient { get; }
        void CreateTablesIfNotCreated();
        void CreateQueusIfNotCreated();
    }
}