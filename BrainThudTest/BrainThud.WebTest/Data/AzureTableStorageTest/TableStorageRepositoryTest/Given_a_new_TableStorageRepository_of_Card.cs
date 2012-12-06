using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Card : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.PARTITION_KEY);
            this.TableStorageRepository = new TableStorageRepository<Card>(this.TableStorageContext.Object, authenticationHelper.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected TableStorageRepository<Card> TableStorageRepository { get; private set; }
    }
}