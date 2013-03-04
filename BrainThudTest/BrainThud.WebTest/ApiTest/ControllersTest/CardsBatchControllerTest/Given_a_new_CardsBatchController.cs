using System.Net.Http;
using BrainThud.Web.Api.Controllers;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsBatchControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_CardsBatchController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            var tableStorageContextFactory = new TableStorageContextFactoryMockBuilder()
                .SetTableStorageContext(this.TableStorageContext)
                .Build();

            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            var cardsBatchController = new CardsBatchController(tableStorageContextFactory.Object, authenticationHelper.Object);
            var apiControllerBuilder = new ApiControllerBuilder<CardsBatchController>(cardsBatchController);

            this.CardsBatchController = apiControllerBuilder
                .CreateRequest(HttpMethod.Put, TestUrls.CARDS)
                .Build(); 
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected CardsBatchController CardsBatchController { get; set; }
    }
}