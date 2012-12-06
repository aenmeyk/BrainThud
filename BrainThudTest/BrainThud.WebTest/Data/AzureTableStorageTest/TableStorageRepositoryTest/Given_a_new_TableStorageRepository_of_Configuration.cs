using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Configuration : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            this.TableStorageRepository = new TableStorageRepository<Configuration>(this.TableStorageContext.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected TableStorageRepository<Configuration> TableStorageRepository { get; private set; }
    }
}