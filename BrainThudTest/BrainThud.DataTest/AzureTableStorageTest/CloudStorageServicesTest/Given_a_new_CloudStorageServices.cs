using BrainThud.Data.AzureTableStorage;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.CloudStorageServicesTest
{
    [TestFixture]
    public abstract class Given_a_new_CloudStorageServices : Gwt
    {
        public override void Given()
        {
            this.CloudStorageServices = new CloudStorageServices();
        }

        protected CloudStorageServices CloudStorageServices { get; private set; }
    }
}