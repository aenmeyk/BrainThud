using BrainThud.Data;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.RepositoryFactoryTest
{
    [TestFixture]
    public abstract class Given_a_new_RepositoryFactory : Gwt
    {
        public override void Given()
        {
            var cloudStorageAccount = new CloudStorageAccountBuilder().Build();
            this.RepositoryFactory = new RepositoryFactory(cloudStorageAccount);
        }

        protected RepositoryFactory RepositoryFactory { get; private set; }
    }
}