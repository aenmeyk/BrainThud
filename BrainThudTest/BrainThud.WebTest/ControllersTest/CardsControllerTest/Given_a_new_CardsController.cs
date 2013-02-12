using System.Net.Http;
using BrainThud.Web;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_CardsController : Gwt
    {

        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            var tableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            tableStorageContextFactory.Setup(x => x.CreateTableStorageContext(AzureTableNames.CARD, TestValues.NAME_IDENTIFIER)).Returns(this.TableStorageContext.Object);

            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            var cardsControllerFake = new CardsControllerFake(tableStorageContextFactory.Object, authenticationHelper.Object);
            var apiControllerBuilder = new ApiControllerBuilder<CardsControllerFake>(cardsControllerFake);

            this.CardsController = apiControllerBuilder
                .CreateRequest(HttpMethod.Post, TestUrls.CARDS)
                .Build();

            this.Request = apiControllerBuilder.Request;
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected CardsControllerFake CardsController { get; private set; }
        protected HttpRequestMessage Request { get; set; }
    }
}