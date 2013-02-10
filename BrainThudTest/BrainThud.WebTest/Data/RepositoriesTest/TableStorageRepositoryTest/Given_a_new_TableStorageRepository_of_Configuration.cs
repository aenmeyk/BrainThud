using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using BrainThud.Core.Models;
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
            this.TableStorageRepository = new TableStorageRepository<UserConfiguration>(this.TableStorageContext.Object);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected TableStorageRepository<UserConfiguration> TableStorageRepository { get; private set; }
    }
}