using System.Net.Http;
using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_CardController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var tableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            tableStorageContextFactory.Setup(x => x.CreateTableStorageContext(EntitySetNames.CARD)).Returns(this.TableStorageContext.Object);

            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.PARTITION_KEY);
            var cardsControllerFake = new CardsControllerFake(
                tableStorageContextFactory.Object, 
                authenticationHelper.Object,
                new Mock<IKeyGeneratorFactory>().Object);

            this.CardsController = new ApiControllerBuilder<CardsControllerFake>(cardsControllerFake)
                .CreateRequest(HttpMethod.Post, TestUrls.CARDS)
                .Build();
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected CardsControllerFake CardsController { get; private set; }
    }
}