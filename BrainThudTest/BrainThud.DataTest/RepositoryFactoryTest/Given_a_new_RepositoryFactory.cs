using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.RepositoryFactoryTest
{
    [TestFixture]
    public abstract class Given_a_new_RepositoryFactory : Gwt
    {
        public override void Given()
        {
            var cloudStorageServices = new MockCloudStorageServicesBuilder().Build();
            this.RepositoryFactory = new RepositoryFactory(cloudStorageServices.Object, new Mock<ITableStorageKeyGenerator>().Object);
        }

        protected RepositoryFactory RepositoryFactory { get; private set; }
    }
}