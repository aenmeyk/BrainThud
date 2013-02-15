using Microsoft.WindowsAzure.Storage.Queue;

namespace BrainThud.Web.Data.AzureQueues
{
    public interface ICloudQueueMessageWrapper 
    {
        string AsString { get; }
        CloudQueueMessage Message { get; }
    }
}