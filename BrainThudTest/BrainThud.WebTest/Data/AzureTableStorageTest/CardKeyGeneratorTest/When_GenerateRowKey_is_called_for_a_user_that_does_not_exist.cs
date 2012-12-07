using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called_for_a_user_that_does_not_exist : Given_a_new_CardKeyGenerator
    {
        private string rowKey;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Configurations.Get(TestValues.NAME_IDENTIFIER, EntityNames.CONFIGURATION)).Returns((Configuration)null);
            this.rowKey = this.CardKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_CardId_should_be_one_more_than_the_LastUsedId()
        {
            this.rowKey.Should().Be((LAST_USED_ID + 1).ToString());
        }
        
        [Test]
        public void Then_the_LastUsedId_is_incremented()
        {
            this.Configuration.LastUsedId.Should().Be(LAST_USED_ID + 1);
        }

        [Test]
        public void Then_the_Configuration_is_updated_in_the_context()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.Configuration), Times.Once());
        }
    }
}