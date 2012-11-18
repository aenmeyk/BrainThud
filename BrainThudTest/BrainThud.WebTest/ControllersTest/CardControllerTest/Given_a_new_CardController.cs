using System.Net.Http;
using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_CardController : Gwt
    {
        public override void Given()
        {
            this.UnitOfWork = new Mock<IUnitOfWork> { DefaultValue = DefaultValue.Mock };
            this.CardsController = new ApiControllerBuilder<CardsControllerFake>(new CardsControllerFake(this.UnitOfWork.Object))
                .CreateRequest(HttpMethod.Post, TestUrls.CARDS)
                .Build();
        }

        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected CardsControllerFake CardsController { get; private set; }
    }
}