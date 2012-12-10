using System.Collections.Generic;
using System.Collections.ObjectModel;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class FakeIdentityQueueManager : IdentityQueueManager
    {
        public ICollection<long> ItemsAddedToQueue { get; private set; }
        public int MessagesInQueue { get; set; }
        public int TimesGetQueueReferenceCalled { get; private set; }

        public FakeIdentityQueueManager(ITableStorageContextFactory tableStorageContextFactory, ICloudStorageServices cloudStorageServices)
            : base(tableStorageContextFactory, cloudStorageServices)
        {
            this.ItemsAddedToQueue = new Collection<long>();
        }

        protected override CloudQueue GetQueueReference()
        {
            this.TimesGetQueueReferenceCalled++;
            return null;
        }

        protected override void AddIdentityToQueue(CloudQueue cloudQueue, long value)
        {
            this.ItemsAddedToQueue.Add(value);
        }

        protected override int GetApproximateMessageCount(CloudQueue cloudQueue)
        {
            return this.MessagesInQueue;
        }
    }
}