using System;
using BrainThud.Core;
using BrainThud.Web.Data.AzureQueues;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest.IdentityQueueManagerTest
{
    [TestFixture]
    public class When_GetNextIdentity_is_called : Given_a_new_IdentityQueueManager
    {
        private const int EXPECTED_NEXT_IDENTITY = 8234;
        private int nextIdentity;
        private Mock<ICloudQueueMessageWrapper> cloudQueueMessage;

        public override void When()
        {
            var visibilityTimeout = TimeSpan.FromSeconds(ConfigurationSettings.IDENTITY_QUEUE_VISIBILITY_TIMEOUT_SECONDS);
            this.cloudQueueMessage = Mock.Get(this.IdentityCloudQueue.Object.GetMessage(visibilityTimeout));
            this.cloudQueueMessage.Setup(x => x.AsString).Returns(EXPECTED_NEXT_IDENTITY.ToString());
            this.nextIdentity = this.IdentityQueueManager.GetNextIdentity();
        }

        [Test]
        public void Then_the_next_identity_in_the_queue_is_returned()
        {
            this.nextIdentity.Should().Be(EXPECTED_NEXT_IDENTITY);
        }

        [Test]
        public void Then_the_message_is_deleted_from_the_queue()
        {
            this.IdentityCloudQueue.Verify(x => x.DeleteMessage(this.cloudQueueMessage.Object));
        }
    }
}