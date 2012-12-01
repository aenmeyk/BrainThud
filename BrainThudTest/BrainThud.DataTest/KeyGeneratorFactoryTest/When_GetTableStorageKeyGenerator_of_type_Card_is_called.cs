using BrainThud.Data.AzureTableStorage;
using BrainThud.Data.KeyGenerators;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.KeyGeneratorFactoryTest
{
    [TestFixture]
    public class When_GetTableStorageKeyGenerator_of_type_Card_is_called : Given_a_new_KeyGeneratorFactory
    {
        private ITableStorageKeyGenerator keyGenerator;

        public override void When()
        {
            this.keyGenerator = this.KeyGeneratorFactory.GetTableStorageKeyGenerator<Card>();
        }

        [Test]
        public void Then_the_KeyGenerator_should_implement_ITableStorageKeyGenerator()
        {
            this.keyGenerator.Should().BeAssignableTo<ITableStorageKeyGenerator>();
        }
    }
}