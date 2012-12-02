using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorFactoryTest
{
    [TestFixture]
    public abstract class Given_a_new_KeyGeneratorFactory : Gwt
    {
        public override void Given()
        {
            this.KeyGeneratorFactory = new KeyGeneratorFactory(new Mock<IAuthenticationHelper>().Object);
        }

        protected KeyGeneratorFactory KeyGeneratorFactory { get; private set; }
    }
}