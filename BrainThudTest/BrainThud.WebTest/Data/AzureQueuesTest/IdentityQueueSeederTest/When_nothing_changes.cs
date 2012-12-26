using BrainThud.Web.Data.AzureQueues;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest.IdentityQueueSeederTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_IdentityQueueSeeder
    {
        [Test]
        public void Then_IdentityQueueManager_should_implement_IIdentityQueueManager()
        {
            this.IdentityQueueSeeder.Should().BeAssignableTo<IIdentityQueueSeeder>();
        }
    }
}