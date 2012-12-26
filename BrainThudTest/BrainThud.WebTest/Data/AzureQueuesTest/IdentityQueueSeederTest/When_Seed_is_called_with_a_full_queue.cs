using BrainThud.Web;
using FluentAssertions;
using Microsoft.WindowsAzure.StorageClient;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest.IdentityQueueSeederTest
{
    public class When_Seed_is_called_with_a_full_queue : Given_a_new_IdentityQueueSeeder
    {
        public override void When()
        {
            this.IdentityCloudQueue.Setup(x => x.RetrieveApproximateMessageCount()).Returns(ConfigurationSettings.SEED_IDENTITIES);
            this.IdentityQueueSeeder.Seed();
        }

        [Test]
        public void Then_no_messages_should_be_added_to_the_queue()
        {
            this.IdentityCloudQueue.Verify(x => x.AddMessage(It.IsAny<CloudQueueMessage>()), Times.Never());
        }

        [Test]
        public void Then_the_CurrentMaxIdentity_should_remain_the_same()
        {
            this.MasterConfiguration.CurrentMaxIdentity.Should().Be(CURRENT_MAX_IDENTITY);
        }

        [Test]
        public void Then_the_MasterConfiguration_should_not_be_saved()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.MasterConfiguration), Times.Never());
        }

        [Test]
        public void Then_the_MasterConfiguration_changes_should_not_be_committed()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Never());
        }
    }
}