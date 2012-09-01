using Microsoft.WindowsAzure;

namespace BrainThud.Data.AzureTableStorage
{
    public interface ICloudStorageServices 
    {
        CloudStorageAccount CloudStorageAccount { get; }
        void CreateTableIfNotExist(string entitySetName);
    }
}