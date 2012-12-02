using BrainThud.Web.Data.AzureTableStorage;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CloudStorageServicesTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_CloudStorageServices
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_ICloudStorageAccountFactory_should_be_implemented()
        {
            this.CloudStorageServices.Should().BeAssignableTo<ICloudStorageServices>();
        }
    }
}