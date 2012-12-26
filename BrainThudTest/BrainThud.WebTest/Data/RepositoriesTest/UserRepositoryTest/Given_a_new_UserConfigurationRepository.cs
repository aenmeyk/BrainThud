using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_UserConfigurationRepository : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext>();
            this.CardKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.UserHelper = new Mock<IUserHelper>();

            this.UserConfigurationRepository = new UserConfigurationRepository(
                this.TableStorageContext.Object,
                this.CardKeyGenerator.Object,
                this.UserHelper.Object,
                TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ICardEntityKeyGenerator> CardKeyGenerator { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected Mock<IUserHelper> UserHelper { get; private set; }
        protected UserConfigurationRepository UserConfigurationRepository { get; set; }
    }
}