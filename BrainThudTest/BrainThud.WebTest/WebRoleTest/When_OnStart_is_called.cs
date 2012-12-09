using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.WebRoleTest
{
    [TestFixture]
    public class When_OnStart_is_called : Given_a_new_WebRole
    {
        public override void When()
        {
            this.WebRole.OnStart();
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
    }
}