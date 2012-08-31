using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Nugget : Gwt
    {
        public override void Given()
        {
            this.Context = new Mock<ITableStorageContext<Nugget>>();
            this.TableStorageRepository = new TableStorageRepository<Nugget>(this.Context.Object);
        }

        protected Mock<ITableStorageContext<Nugget>> Context { get; private set; }
        protected TableStorageRepository<Nugget> TableStorageRepository { get; private set; }
    }
}