using System;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureQueues
{
    public interface IIdentityCloudQueue 
    {
        ICloudQueueMessageWrapper GetMessage(TimeSpan visibilityTimeout);
        void DeleteMessage(ICloudQueueMessageWrapper message);
        int RetrieveApproximateMessageCount();
        void AddMessage(CloudQueueMessage message);
    }
}