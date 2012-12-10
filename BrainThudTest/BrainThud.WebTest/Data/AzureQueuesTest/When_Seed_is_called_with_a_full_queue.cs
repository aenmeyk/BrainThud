using BrainThud.Web;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest
{
    public class When_Seed_is_called_with_a_full_queue : Given_a_new_IdentityQueueManager
    {
        private const int MESSAGES_IN_QUEUE = ConfigurationSettings.SEED_IDENTITIES;

        public override void When()
        {
            this.IdentityQueueManager.MessagesInQueue = MESSAGES_IN_QUEUE;
            this.IdentityQueueManager.Seed();
        }

        [Test]
        public void Then_no_messages_should_be_added_to_the_queue()
        {
            this.IdentityQueueManager.ItemsAddedToQueue.Count.Should().Be(0);
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