using BrainThud.Web;
using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.LibraryControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_LibraryController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            var tableStorageContextFactory = new TableStorageContextFactoryMockBuilder()
                .SetTableStorageContext(this.TableStorageContext, AzureTableNames.CARD, NameIdentifiers.MASTER)
                .Build();

            this.LibraryController = new LibraryController(tableStorageContextFactory.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected LibraryController LibraryController { get; set; }
    }
}