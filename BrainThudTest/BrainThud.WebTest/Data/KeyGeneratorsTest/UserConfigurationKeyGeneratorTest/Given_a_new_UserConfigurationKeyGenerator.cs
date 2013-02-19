using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.KeyGenerators;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.KeyGeneratorsTest.UserConfigurationKeyGeneratorTest
{
    [TestFixture]
    public abstract class Given_a_new_UserConfigurationKeyGenerator : Gwt
    {
        public override void Given()
        {
            this.IdentityQueueManager = new Mock<IIdentityQueueManager>();
            this.UserConfigurationKeyGenerator = new UserConfigurationKeyGenerator(
                new Mock<IAuthenticationHelper>().Object,
                this.IdentityQueueManager.Object);
        }

        protected Mock<IIdentityQueueManager> IdentityQueueManager { get; private set; }
        protected UserConfigurationKeyGenerator UserConfigurationKeyGenerator { get; private set; }
    }
}