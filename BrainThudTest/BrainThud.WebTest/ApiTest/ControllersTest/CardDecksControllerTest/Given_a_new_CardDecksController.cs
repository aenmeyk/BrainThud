using BrainThud.Core;
using BrainThud.Web.Api.Controllers;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardDecksControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_CardDecksController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext>();
            var tableStorageContextFactory = new Mock<ITableStorageContextFactory>();
            tableStorageContextFactory
                .Setup(x => x.CreateTableStorageContext(AzureTableNames.CARD, It.IsAny<string>()))
                .Returns(this.TableStorageContext.Object);

            this.CardDecksController = new CardDecksController(tableStorageContextFactory.Object, new Mock<IAuthenticationHelper>().Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; set; }
        protected CardDecksController CardDecksController { get; set; }
    }
}