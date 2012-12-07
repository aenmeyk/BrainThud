using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest
{
    [TestFixture]
    public abstract class Given_a_new_UserHelper : Gwt
    {
        public override void Given()
        {
            this.TableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            this.TableStorageContext = Mock.Get(this.TableStorageContextFactory.Object.CreateTableStorageContext(EntitySetNames.CARD));
            this.UserHelper = new UserHelper(this.TableStorageContextFactory.Object);
        }

        protected UserHelper UserHelper { get; private set; }
        protected Mock<ITableStorageContextFactory> TableStorageContextFactory { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
    }
}