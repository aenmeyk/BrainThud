using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserConfigurationRepositoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_UserConfigurationRepository
    {
        [Test]
        public void Then_UserRepository_should_derive_from_CardEntityRepository()
        {
            this.UserConfigurationRepository.Should().BeAssignableTo<CardEntityRepository<UserConfiguration>>();
        }
    }
}