using System;
using BrainThud.Core;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace BrainThud.Web.Data.AzureQueues
{
    public class IdentityCloudQueue : IIdentityCloudQueue
    {
        private readonly Lazy<CloudQueue> lazyCloudQueue;

        public IdentityCloudQueue(ICloudStorageServices cloudStorageServices)
        {
            this.lazyCloudQueue = new Lazy<CloudQueue>(() => cloudStorageServices.CloudQueueClient.GetQueueReference(AzureQueueNames.IDENTITY));
        }

        public void AddMessage(CloudQueueMessage message)
        {
            this.lazyCloudQueue.Value.AddMessage(message);
        }
        
        public ICloudQueueMessageWrapper GetMessage(TimeSpan visibilityTimeout)
        {
            var cloudQueueMessage = this.lazyCloudQueue.Value.GetMessage(visibilityTimeout);
            return new CloudQueueMessageWrapper(cloudQueueMessage);
        }

        public void DeleteMessage(ICloudQueueMessageWrapper message)
        {
            this.lazyCloudQueue.Value.DeleteMessage(message.Message);
        }

        public int RetrieveApproximateMessageCount()
        {
            this.lazyCloudQueue.Value.FetchAttributes();
            var approximateMessageCount = this.lazyCloudQueue.Value.ApproximateMessageCount;

            return approximateMessageCount != null
                ? approximateMessageCount.Value
                : 0;
        }
    }
}