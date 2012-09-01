using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.RepositoryFactoryTest
{
    [TestFixture]
    public class When_CreateTableStorageRepository_of_Nugget_is_called : Given_a_new_RepositoryFactory
    {
        private ITableStorageRepository<Nugget> repository;

        public override void When()
        {
            this.repository = this.RepositoryFactory.CreateTableStorageRepository<Nugget>();
        }

        [Test]
        public void Then_the_result_should_implement_ITableStorageRepository_of_Nugget()
        {
            this.repository.Should().BeAssignableTo<ITableStorageRepository<Nugget>>();
        }
    }
}