using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.ConfigControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_ConfigController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            this.AuthenticationHelper = new Mock<IAuthenticationHelper> { DefaultValue = DefaultValue.Mock };
            this.ConfigController = new ConfigController(this.TableStorageContextFactory.Object, this.AuthenticationHelper.Object);
        }

        protected Mock<IAuthenticationHelper> AuthenticationHelper { get; private set; }
        protected ConfigController ConfigController { get; private set; }
        public Mock<ITableStorageContextFactory> TableStorageContextFactory { get; private set; }
    }
}