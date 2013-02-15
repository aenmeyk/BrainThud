using BrainThud.Web;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest.IdentityQueueSeederTest
{
    public class When_Seed_is_called : Given_a_new_IdentityQueueSeeder
    {
        private const int MESSAGES_IN_QUEUE = 5;

        public override void When()
        {
            this.IdentityCloudQueue.Setup(x => x.RetrieveApproximateMessageCount()).Returns(MESSAGES_IN_QUEUE);
            this.IdentityQueueSeeder.Seed();
        }

        [Test]
        public void Then_the_CurrentMaxIdentity_should_be_incremented_by_the_SeedIdentities_setting()
        {
            this.MasterConfiguration.CurrentMaxIdentity.Should().Be(CURRENT_MAX_IDENTITY + ConfigurationSettings.SEED_IDENTITIES - MESSAGES_IN_QUEUE);
        }

        [Test]
        public void Then_the_MasterConfiguration_should_be_saved()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.MasterConfiguration), Times.Once());
        }

        [Test]
        public void Then_the_MasterConfiguration_changes_should_be_committed()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_the_new_SeedIdentities_should_be_added_to_the_queue()
        {
            var callCount = ConfigurationSettings.SEED_IDENTITIES - MESSAGES_IN_QUEUE;
            this.IdentityCloudQueue.Verify(x => x.AddMessage(It.IsAny<CloudQueueMessage>()), Times.Exactly(callCount));
        }
    }
}