using BrainThud.Model;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.ModelTest.CardTest
{
    [TestFixture]
    public abstract class Given_a_new_Card : Gwt
    {
        public override void Given()
        {
            this.Card = new Card();
        }

        protected Card Card { get; private set; }
    }
}