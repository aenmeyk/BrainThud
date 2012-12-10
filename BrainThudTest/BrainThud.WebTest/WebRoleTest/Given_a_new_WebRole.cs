using BrainThud.Web;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BrainThudTest.BrainThud.WebTest.WebRoleTest
{
    [TestFixture]
    public abstract class Given_a_new_WebRole : Gwt
    {
        public override void Given()
        {
            this.CloudStorageServices = new Mock<ICloudStorageServices>();
            this.IdentityQueueManager = new Mock<IIdentityQueueManager>();

            var ioc = new Mock<IContainer>();
            ioc.Setup(x => x.GetInstance<ICloudStorageServices>()).Returns(this.CloudStorageServices.Object);
            ioc.Setup(x => x.GetInstance<IIdentityQueueManager>()).Returns(this.IdentityQueueManager.Object);

            this.WebRole = new WebRole(enableContinuousExecution: false) {IoCContainer = ioc.Object};
        }

        protected Mock<ICloudStorageServices> CloudStorageServices { get; private set; }
        protected Mock<IIdentityQueueManager> IdentityQueueManager { get; private set; }
        protected WebRole WebRole { get; private set; }
    }
}