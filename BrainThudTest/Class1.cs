using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void ShoudlPass()
        {
            2.Should().Be(2);
        }

        [Test]
        public void ShoudlFail()
        {
            2.Should().Be(1);
        }
    }
}
