using BrainThud.Data;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.RepositoryFactoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_RepositoryFactory
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_IRepositoryFactory_should_be_implemented()
        {
            this.RepositoryFactory.Should().BeAssignableTo<IRepositoryFactory>();
        }
    }
}