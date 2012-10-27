using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageContext_of_Card : Gwt
    {

        public override void Given()
        {
            this.CloudStorageServices = new MockCloudStorageServicesBuilder().Build();
            this.TableStorageContext = new TableStorageContext<Card>(EntitySetNames.CARD, this.CloudStorageServices.Object);
        }

        protected TableStorageContext<Card> TableStorageContext { get; private set; }
        protected Mock<ICloudStorageServices> CloudStorageServices { get; private set; }
    }
}