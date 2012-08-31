using BrainThud.Data.AzureTableStorage;
using BrainThudTest.Tools;
using Microsoft.WindowsAzure;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.NuggetTableStorageContextTest
{
    [TestFixture]
    public abstract class Given_a_new_NuggetTableStorageContext : Gwt
    {
        public override void Given()
        {
            var credentials = new Mock<StorageCredentials>();
            credentials.SetupGet(x => x.CanSignRequest).Returns(true);
            credentials.SetupGet(x => x.CanSignRequestLite).Returns(true);
            this.NuggetTableStorageContext = new NuggetTableStorageContext("http://brainthud.table.core.windows.net/", credentials.Object);
        }

        protected NuggetTableStorageContext NuggetTableStorageContext { get; private set; }
    }
}