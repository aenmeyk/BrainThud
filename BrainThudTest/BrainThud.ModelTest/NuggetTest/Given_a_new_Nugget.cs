using BrainThud.Model;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.ModelTest.NuggetTest
{
    [TestFixture]
    public abstract class Given_a_new_Nugget : Gwt
    {
        public override void Given()
        {
            this.Nugget = new Nugget();
        }

        protected Nugget Nugget { get; private set; }
    }
}