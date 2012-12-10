using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BrainThud.Web;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest
{
    public class When_Seed_is_called : Given_a_new_IdentityQueueManager
    {
        private const int MESSAGES_IN_QUEUE = 5;
        private readonly ICollection<long> expectedGeneratedIdentities = new Collection<long>();

        public override void When()
        {
            for (long i = CURRENT_MAX_IDENTITY + 1; i <= CURRENT_MAX_IDENTITY + ConfigurationSettings.SEED_IDENTITIES - MESSAGES_IN_QUEUE; i++)
            {
                this.expectedGeneratedIdentities.Add(i);
            }

            this.IdentityQueueManager.MessagesInQueue = MESSAGES_IN_QUEUE;
            this.IdentityQueueManager.Seed();
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
        public void Then_GetQueueReference_should_be_called()
        {
            this.IdentityQueueManager.TimesGetQueueReferenceCalled.Should().Be(1);
        }

        [Test]
        public void Then_the_new_SeedIdentities_should_be_added_to_the_queue()
        {
            this.IdentityQueueManager.ItemsAddedToQueue.Should().BeEquivalentTo(this.expectedGeneratedIdentities);
            this.IdentityQueueManager.ItemsAddedToQueue.Max().Should().Be(CURRENT_MAX_IDENTITY + ConfigurationSettings.SEED_IDENTITIES - MESSAGES_IN_QUEUE);
            this.IdentityQueueManager.ItemsAddedToQueue.Min().Should().Be(CURRENT_MAX_IDENTITY + 1);
        }
    }
}