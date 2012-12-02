using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Card : Gwt
    {
        public override void Given()
        {
            var tableStorageGenerator = new Mock<ITableStorageKeyGenerator>();
            tableStorageGenerator.Setup(x => x.GeneratePartitionKey()).Returns(TestValues.PARTITION_KEY);
            tableStorageGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            this.TableStorageRepository = new TableStorageRepository<Card>(this.TableStorageContext.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected TableStorageRepository<Card> TableStorageRepository { get; private set; }
    }
}