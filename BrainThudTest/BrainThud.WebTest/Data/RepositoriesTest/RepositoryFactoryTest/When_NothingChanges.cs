using BrainThud.Web.Data.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.RepositoryFactoryTest
{
    [TestFixture]
    public class When_NothingChanges : Given_a_new_RepositoryFactory
    {
        [Test]
        public void Then_the_RepositoryFactory_should_implement_IRepositoryFactory()
        {
            this.RepositoryFactory.Should().BeAssignableTo<IRepositoryFactory>();
        }
    }
}