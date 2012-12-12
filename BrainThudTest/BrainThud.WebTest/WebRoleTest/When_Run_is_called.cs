using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.WebRoleTest
{
    [TestFixture]
    public class When_Run_is_called : Given_a_new_WebRole
    {
        public override void When()
        {
            this.WebRole.Run();
        }

        [Test]
        public void Then_SetConfigurationSettingPublisher_is_called_on_the_CloudStorageServices()
        {
            this.CloudStorageServices.Verify(x => x.SetConfigurationSettingPublisher(), Times.Once());
        }

        [Test]
        public void Then_CreateTablesIfNotCreated_is_called_on_the_CloudStorageServices()
        {
            this.CloudStorageServices.Verify(x => x.CreateTablesIfNotCreated(), Times.Once());
        }

        [Test]
        public void Then_CreateQueusIfNotCreated_is_called_on_the_CloudStorageServices()
        {
            this.CloudStorageServices.Verify(x => x.CreateQueusIfNotCreated(), Times.Once());
        }

        [Test]
        public void Then_Seed_is_called_on_IdentityQueueManager()
        {
            this.IdentityQueueManager.Verify(x => x.Seed(), Times.Once());
        }
    }
}