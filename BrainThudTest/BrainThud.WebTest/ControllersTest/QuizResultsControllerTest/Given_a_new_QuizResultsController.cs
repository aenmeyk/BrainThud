using System.Net.Http;
using BrainThud.Data;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultsController : Gwt
    {
        public override void Given()
        {
            this.UnitOfWork = new Mock<IUnitOfWork> { DefaultValue = DefaultValue.Mock };

            this.QuizResultsController = new ApiControllerBuilder<QuizResultsControllerFake>(new QuizResultsControllerFake(this.UnitOfWork.Object))
               .CreateRequest(HttpMethod.Post, TestUrls.QUIZ_RESULTS)
               .Build();
        }

        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected QuizResultsControllerFake QuizResultsController { get; private set; }
    }
}