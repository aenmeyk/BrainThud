using System;

namespace BrainThud.Web.Data.AzureQueues
{
    public class IdentityQueueManager : IIdentityQueueManager
    {
        private readonly IIdentityCloudQueue queue;

        public IdentityQueueManager(IIdentityCloudQueue queue)
        {
            this.queue = queue;
        }

        public int GetNextIdentity()
        {
            var visibilityTimeout = TimeSpan.FromSeconds(ConfigurationSettings.IDENTITY_QUEUE_VISIBILITY_TIMEOUT_SECONDS);
            var queueMessage = this.queue.GetMessage(visibilityTimeout);

            // TODO: If the queue is empty, fill it then retry
            if (queueMessage == null) throw new Exception("No identity values remaining in the identity queue.");

            var queueValue = int.Parse(queueMessage.AsString);
            this.queue.DeleteMessage(queueMessage);

            return queueValue;
        }
    }
}