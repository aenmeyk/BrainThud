using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_UserRepository : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext>();
            this.UserRepository = new UserRepository(this.TableStorageContext.Object, TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected UserRepository UserRepository { get; set; }
    }
}