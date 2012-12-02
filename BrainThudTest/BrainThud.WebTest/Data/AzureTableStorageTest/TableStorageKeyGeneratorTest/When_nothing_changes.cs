using BrainThud.Web.Data.KeyGenerators;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageKeyGeneratorTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_TableStorageKeyGenerator
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_the_class_should_implement_ITableStorageGenerator()
        {
            this.TableStorageKeyGenerator.Should().BeAssignableTo<ITableStorageKeyGenerator>();
        }
    }
}