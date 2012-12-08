using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserRepositoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_UserRepository
    {
        [Test]
        public void Then_UserRepository_should_derive_from_CardEntityRepository()
        {
            this.UserRepository.Should().BeAssignableTo<CardEntityRepository<UserConfiguration>>();
        }
    }
}