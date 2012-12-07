using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called_for_a_user_that_does_not_exist : Given_a_new_CardKeyGenerator
    {
        public override void When()
        {
            this.CardKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_UserConfiguration_is_updated_in_the_context()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.UserConfiguration), Times.Once());
        }
    }
}