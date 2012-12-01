using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Data.KeyGenerators;
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
            this.KeyGeneratorFactory = new Mock<IKeyGeneratorFactory> { DefaultValue = DefaultValue.Mock };
            this.RepositoryFactory = new RepositoryFactory(cloudStorageServices.Object, this.KeyGeneratorFactory.Object);
        }

        protected Mock<IKeyGeneratorFactory> KeyGeneratorFactory { get; private set; }
        protected RepositoryFactory RepositoryFactory { get; private set; }
    }
}