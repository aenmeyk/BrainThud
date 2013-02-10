using BrainThud.Core.Models;
using BrainThud.Web.Data.Repositories;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_CardRepository
    {
        [Test]
        public void Then_the_CardRepository_should_derive_from_TableStorageRepository_of_type_Card()
        {
            this.CardRepository.Should().BeAssignableTo<TableStorageRepository<Card>>();
        }
    }
}