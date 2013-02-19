using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserConfigurationRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_UserConfigurationRepository : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext>();
            this.UserConfigurationKeyGenerator = new Mock<ICardEntityKeyGenerator>();

            this.UserConfigurationRepository = new UserConfigurationRepository(
                this.TableStorageContext.Object,
                this.UserConfigurationKeyGenerator.Object,
                TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ICardEntityKeyGenerator> UserConfigurationKeyGenerator { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected UserConfigurationRepository UserConfigurationRepository { get; set; }
    }
}