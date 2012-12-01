using BrainThud.Data;
using BrainThud.Data.KeyGenerators;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.KeyGeneratorFactoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_KeyGeneratorFactory
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_KeyGeneratorFactory_should_implement_IKeyGeneratorFactory()
        {
            this.KeyGeneratorFactory.Should().BeAssignableTo<IKeyGeneratorFactory>();
        }
    }
}