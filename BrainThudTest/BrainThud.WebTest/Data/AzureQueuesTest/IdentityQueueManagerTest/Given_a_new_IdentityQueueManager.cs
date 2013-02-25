using BrainThud.Core;
using BrainThud.Web.Data.AzureQueues;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest.IdentityQueueManagerTest
{
    public abstract class Given_a_new_IdentityQueueManager : Gwt
    {
        protected const int CURRENT_MAX_IDENTITY = 1000;

        public override void Given()
        {
            // Set the refresh interval to 0 so that the test thread doesn't sleep
            ConfigurationSettings.SeedRefreshIntervalSeconds = 0;
            this.IdentityCloudQueue = new Mock<IIdentityCloudQueue> { DefaultValue = DefaultValue.Mock };

            this.IdentityQueueManager = new IdentityQueueManager(this.IdentityCloudQueue.Object);
        }

        protected Mock<IIdentityCloudQueue> IdentityCloudQueue { get; private set; }
        protected IdentityQueueManager IdentityQueueManager { get; private set; }
    }
}