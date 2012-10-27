using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.RepositoryFactoryTest
{
    [TestFixture]
    public class When_CreateTableStorageRepository_of_Card_is_called : Given_a_new_RepositoryFactory
    {
        private ITableStorageRepository<Card> repository;

        public override void When()
        {
            this.repository = this.RepositoryFactory.CreateTableStorageRepository<Card>();
        }

        [Test]
        public void Then_the_result_should_implement_ITableStorageRepository_of_Card()
        {
            this.repository.Should().BeAssignableTo<ITableStorageRepository<Card>>();
        }
    }
}