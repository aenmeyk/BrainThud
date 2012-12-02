using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThudTest.BrainThud.WebTest.Fakes;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizzesController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var tableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            tableStorageContextFactory.Setup(x => x.CreateTableStorageContext(EntitySetNames.CARD)).Returns(this.TableStorageContext.Object);

            this.QuizzesController = new QuizzesControllerFake(tableStorageContextFactory.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected QuizzesControllerFake QuizzesController { get; private set; }
    }
}