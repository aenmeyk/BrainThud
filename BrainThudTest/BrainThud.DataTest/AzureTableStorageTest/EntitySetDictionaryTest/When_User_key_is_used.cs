using BrainThud.Model;
using NUnit.Framework;
using FluentAssertions;
using BrainThud.Data;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.EntitySetDictionaryTest
{
    [TestFixture]
    public class When_User_key_is_used : Given_a_new_EntitySetDictionary
    {
        private string result;

        public override void When()
        {
            this.result = this.EntitySetDictionary[typeof(User)];
        }

        [Test]
        public void Then_User_is_returned()
        {
            this.result.Should().Be(EntitySetNames.USER);
        }
    }
}