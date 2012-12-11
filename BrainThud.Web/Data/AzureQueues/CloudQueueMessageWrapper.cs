using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureQueues
{
    public class CloudQueueMessageWrapper : ICloudQueueMessageWrapper
    {
        private readonly CloudQueueMessage message;

        public CloudQueueMessageWrapper(CloudQueueMessage message)
        {
            this.message = message;
        }

        public CloudQueueMessage Message { get { return this.message; } }
        public string AsString { get { return this.message.AsString; } }
    }
}