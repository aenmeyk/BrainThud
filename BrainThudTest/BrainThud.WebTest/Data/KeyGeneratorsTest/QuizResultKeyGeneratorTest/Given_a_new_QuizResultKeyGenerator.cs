using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.QuizResultKeyGeneratorTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultKeyGenerator : Gwt
    {

        public override void Given()
        {
            this.IdentityQueueManager = new Mock<IIdentityQueueManager>();
            this.QuizResultKeyGenerator = new QuizResultKeyGenerator(
                new Mock<IAuthenticationHelper>().Object,
                this.IdentityQueueManager.Object);
        }

        protected Mock<IIdentityQueueManager> IdentityQueueManager { get; private set; }
        protected QuizResultKeyGenerator QuizResultKeyGenerator { get; private set; }
    }
}