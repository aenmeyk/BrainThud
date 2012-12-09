using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
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
            tableStorageContextFactory.Setup(x => x.CreateTableStorageContext(AzureTableNames.CARD, TestValues.NAME_IDENTIFIER)).Returns(this.TableStorageContext.Object);

            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.SetupGet(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            this.QuizzesController = new QuizzesControllerFake(tableStorageContextFactory.Object, authenticationHelper.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected QuizzesControllerFake QuizzesController { get; private set; }
    }
}