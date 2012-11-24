using BrainThud.Model;
using NUnit.Framework;
using FluentAssertions;
using BrainThud.Data;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.EntitySetDictionaryTest
{
    [TestFixture]
    public class When_Card_key_is_used : Given_a_new_EntitySetDictionary
    {
        private string result;

        public override void When()
        {
            this.result = this.EntitySetDictionary[typeof(Card)];
        }

        [Test]
        public void Then_Card_is_returned()
        {
            this.result.Should().Be(EntitySetNames.CARD);
        }
    }
}