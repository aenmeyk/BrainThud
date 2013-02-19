using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.CardKeyGeneratorTest
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
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            this.UserConfiguration = new UserConfiguration { UserId = USER_ID };
            var tableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            tableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns(this.UserConfiguration);

            this.TableStorageContextFactory = new TableStorageContextFactoryMockBuilder()
                .SetTableStorageContext(tableStorageContext)
                .Build();

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