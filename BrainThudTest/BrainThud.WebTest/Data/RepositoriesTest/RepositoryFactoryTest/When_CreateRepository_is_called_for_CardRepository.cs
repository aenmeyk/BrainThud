using BrainThud.Core;
using BrainThud.Web.Data.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.RepositoryFactoryTest
{
    [TestFixture]
    public class When_CreateRepository_is_called_for_CardRepository : Given_a_new_RepositoryFactory
    {
        private CardRepository respository;

        public override void When()
        {
            this.respository = this.RepositoryFactory.CreateRepository<CardRepository>(
                this.TableStorageContext.Object,
                CardRowTypes.CARD,
                TestValues.NAME_IDENTIFIER);
        }

        [Test]
        public void Then_a_CardRepository_should_be_returned()
        {
            this.respository.Should().BeAssignableTo<CardRepository>();
        }
    }
}