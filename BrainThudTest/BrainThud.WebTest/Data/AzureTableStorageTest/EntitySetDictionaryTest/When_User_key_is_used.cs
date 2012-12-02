using BrainThud.Web.Data;
using BrainThud.Web.Model;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.EntitySetDictionaryTest
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