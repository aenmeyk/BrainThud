using BrainThud.Web;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public abstract class Given_a_new_CardKeyGenerator : Gwt
    {
        protected const string USER_ROW_KEY = TestValues.ROW_KEY;
        protected const int LAST_USED_ID = 8;
        protected const int USER_ID = 53;
        protected const int NEXT_IDENTITY_VALUE = 15;

        public override void Given()
        {
            this.UserConfiguration = new UserConfiguration { UserId = USER_ID };
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);
            this.TableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            this.TableStorageContextFactory
                .Setup(x => x.CreateTableStorageContext(AzureTableNames.CARD, TestValues.NAME_IDENTIFIER).UserConfigurations.GetByNameIdentifier())
                .Returns(this.UserConfiguration);

            this.IdentityQueueManager = new Mock<IIdentityQueueManager>();
            this.IdentityQueueManager.Setup(x => x.GetNextIdentity()).Returns(NEXT_IDENTITY_VALUE);

            this.CardKeyGenerator = new CardKeyGenerator(
                authenticationHelper.Object,
                this.IdentityQueueManager.Object);
        }

        protected Mock<ITableStorageContextFactory> TableStorageContextFactory { get; set; }
        protected Mock<IIdentityQueueManager> IdentityQueueManager { get; set; }
        protected UserConfiguration UserConfiguration { get; set; }
        protected CardKeyGenerator CardKeyGenerator { get; set; }
    }
}