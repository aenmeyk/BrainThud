using System.Net.Http;
using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetsControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_NuggetController : Gwt
    {
        public override void Given()
        {
            this.NuggetRepository = new Mock<ITableStorageRepository<Nugget>>();
            this.UnitOfWork = new Mock<IUnitOfWork>();
            this.UnitOfWork.SetupGet(x => x.Nuggets).Returns(this.NuggetRepository.Object);
            this.NuggetsController = new ApiControllerBuilder<NuggetsControllerFake>(new NuggetsControllerFake(this.UnitOfWork.Object))
                .CreateRequest(HttpMethod.Post, TestUrls.NUGGETS)
                .Build();
        }

        protected Mock<ITableStorageRepository<Nugget>> NuggetRepository { get; private set; }
        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected NuggetsControllerFake NuggetsController { get; private set; }
    }
}