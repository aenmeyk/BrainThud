using BrainThud.Data;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.UnitOfWorkTest
{
    [TestFixture]
    public abstract class Given_a_new_UnitOfWork : Gwt
    {
        public override void Given()
        {
            this.UnitOfWork = new UnitOfWork();
        }

        protected UnitOfWork UnitOfWork { get; private set; }
    }
}