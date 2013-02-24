using BrainThud.Core.Models;
using BrainThud.Web;
using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;
using CardRowTypes = BrainThud.Core.CardRowTypes;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_CardsController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            var tableStorageContextFactory = new TableStorageContextFactoryMockBuilder()
                .SetTableStorageContext(this.TableStorageContext, AzureTableNames.CARD, NameIdentifiers.MASTER)
                .Build();

            var userConfiguration = new UserConfiguration
            {
                PartitionKey = TestValues.PARTITION_KEY,
                RowKey = string.Format("{0}-{1}", CardRowTypes.CONFIGURATION, TestValues.USER_ID)
            };

            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByUserId(TestValues.USER_ID)).Returns(userConfiguration);
            this.LibraryController = new CardsController(tableStorageContextFactory.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected CardsController LibraryController { get; set; }
    }
}