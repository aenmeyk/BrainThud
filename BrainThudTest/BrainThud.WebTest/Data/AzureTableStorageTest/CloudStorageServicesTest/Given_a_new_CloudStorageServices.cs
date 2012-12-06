using BrainThud.Web.Data.AzureTableStorage;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CloudStorageServicesTest
{
    [TestFixture]
    public abstract class Given_a_new_CloudStorageServices : GwtAzureEmulator
    {
        public override void Given()
        {
            this.CloudStorageServices = new CloudStorageServices();
        }

        protected CloudStorageServices CloudStorageServices { get; private set; }
    }
}