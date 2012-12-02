using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoryFactoryTest
{
    [TestFixture]
    public class When_CreateTableStorageRepository_of_User_is_called : Given_a_new_RepositoryFactory
    {
        private ITableStorageRepository<User> repository;

        public override void When()
        {
            this.repository = this.RepositoryFactory.CreateTableStorageRepository<User>();
        }

        [Test]
        public void Then_the_result_should_implement_ITableStorageRepository_of_User()
        {
            this.repository.Should().BeAssignableTo<ITableStorageRepository<User>>();
        }

        [Test]
        public void Then_the_KeyGenerator_should_be_retrieved_from_the_KeyGeneratorFactory()
        {
            this.KeyGeneratorFactory.Verify(x => x.GetTableStorageKeyGenerator<User>(), Times.Once());
        }
    }
}