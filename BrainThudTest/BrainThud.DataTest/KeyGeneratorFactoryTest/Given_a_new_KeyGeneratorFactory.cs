using BrainThud.Data;
using BrainThud.Data.KeyGenerators;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.KeyGeneratorFactoryTest
{
    [TestFixture]
    public abstract class Given_a_new_KeyGeneratorFactory : Gwt
    {
        public override void Given()
        {
            this.KeyGeneratorFactory = new KeyGeneratorFactory();
        }

        protected KeyGeneratorFactory KeyGeneratorFactory { get; private set; }
    }
}