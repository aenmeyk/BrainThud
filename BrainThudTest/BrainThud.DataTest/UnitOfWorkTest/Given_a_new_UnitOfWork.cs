using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.UnitOfWorkTest
{
    [TestFixture]
    public abstract class Given_a_new_UnitOfWork : Gwt
    {
        public override void Given()
        {
            this.NuggetTableStorageRepository = new Mock<ITableStorageRepository<Nugget>>();
            var repositoryFactory = new Mock<IRepositoryFactory>();
            repositoryFactory.Setup(x => x.CreateTableStorageRepository<Nugget>(true)).Returns(this.NuggetTableStorageRepository.Object);
            this.UnitOfWork = new UnitOfWork(repositoryFactory.Object);
        }

        protected Mock<ITableStorageRepository<Nugget>>  NuggetTableStorageRepository { get; private set; }
        protected UnitOfWork UnitOfWork { get; private set; }
    }
}