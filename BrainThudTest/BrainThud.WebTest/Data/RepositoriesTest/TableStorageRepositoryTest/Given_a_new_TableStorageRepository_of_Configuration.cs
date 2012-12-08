using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Configuration : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.SetupGet(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);
            this.TableStorageRepository = new TableStorageRepository<UserConfiguration>(this.TableStorageContext.Object, authenticationHelper.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected TableStorageRepository<UserConfiguration> TableStorageRepository { get; private set; }
    }
}