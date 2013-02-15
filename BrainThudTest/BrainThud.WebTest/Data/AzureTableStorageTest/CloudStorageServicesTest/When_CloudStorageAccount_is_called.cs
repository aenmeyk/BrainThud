using FluentAssertions;
using Microsoft.WindowsAzure.Storage;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CloudStorageServicesTest
{
    [TestFixture]
    public class When_CloudStorageAccount_is_called : Given_a_new_CloudStorageServices
    {
        private CloudStorageAccount result;

        public override void When()
        {
            this.result = this.CloudStorageServices.CloudStorageAccount;
        }

        [Test]
        [Category(TestTypes.LONG_RUNNING)]
        public void Then_the_DataConnectionString_is_returned()
        {
            this.result.Credentials.AccountName.Should().Be("brainthud");
        }
    }
}