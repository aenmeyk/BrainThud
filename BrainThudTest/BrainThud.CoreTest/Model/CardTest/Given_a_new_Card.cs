using BrainThud.Core.Models;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.CoreTest.Model.CardTest
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