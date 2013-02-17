using System.Net.Http;
using BrainThud.Web.Api.Controllers;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.ConfigControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_ConfigController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            var tableStorageContextFactory = new TableStorageContextFactoryMockBuilder()
                .SetTableStorageContext(this.TableStorageContext)
                .Build();

            this.AuthenticationHelper = new Mock<IAuthenticationHelper> { DefaultValue = DefaultValue.Mock };

            this.AuthenticationHelper
                .SetupGet(x => x.NameIdentifier)
                .Returns(TestValues.NAME_IDENTIFIER);

            this.ConfigController = new ConfigController(tableStorageContextFactory.Object, this.AuthenticationHelper.Object);
            this.ConfigController = new ApiControllerBuilder<ConfigController>(this.ConfigController)
                .CreateRequest(HttpMethod.Put, TestUrls.CONFIG)
                .Build();
        }

        protected Mock<IAuthenticationHelper> AuthenticationHelper { get; private set; }
        protected ConfigController ConfigController { get; private set; }
        public Mock<ITableStorageContext> TableStorageContext { get; private set; }
    }
}