using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.RepositoryFactoryTest
{
    [TestFixture]
    public abstract class Given_a_new_RepositoryFactory : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext>();
            this.CardEntityKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.RepositoryFactory = new RepositoryFactory(
                new Mock<IAuthenticationHelper>().Object,
                new Mock<IIdentityQueueManager>().Object);
        }

        protected Mock<ICardEntityKeyGenerator> CardEntityKeyGenerator { get; set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; set; }
        protected RepositoryFactory RepositoryFactory { get; set; }
    }
}