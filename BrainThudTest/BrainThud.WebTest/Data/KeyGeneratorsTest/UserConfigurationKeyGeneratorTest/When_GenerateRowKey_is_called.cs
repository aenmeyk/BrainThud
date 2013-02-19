using BrainThud.Core;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.UserConfigurationKeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called : Given_a_new_UserConfigurationKeyGenerator
    {
        private string result;

        public override void When()
        {
            this.IdentityQueueManager.Setup(x => x.GetNextIdentity()).Returns(TestValues.INT);
            this.result = this.UserConfigurationKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_QuizDate_should_be_included_in_the_RowKey()
        {
            this.result.Should().Be(string.Format("{0}-{1}", CardRowTypes.CONFIGURATION, TestValues.INT));
        }
    }
}