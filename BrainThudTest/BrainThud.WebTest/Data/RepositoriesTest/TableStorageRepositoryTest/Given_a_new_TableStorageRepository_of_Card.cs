using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Card : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var userConfiguration = new UserConfiguration { UserId = TestValues.USER_ID };
            this.TableStorageContext
                .Setup(x => x.UserConfigurations.GetByNameIdentifier())
                .Returns(userConfiguration);

            this.TableStorageRepository = new TableStorageRepository<Card>(this.TableStorageContext.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected TableStorageRepository<Card> TableStorageRepository { get; private set; }
    }
}