using BrainThud.Web;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.QuizResultKeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called : Given_a_new_QuizResultKeyGenerator
    {
        private string result;

        public override void When()
        {
            this.IdentityQueueManager.Setup(x => x.GetNextIdentity()).Returns(TestValues.INT);
            this.result = this.QuizResultKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_QuizDate_should_be_included_in_the_RowKey()
        {
            this.result.Should().Be("qr-5");
        }
    }
}