using System.Net.Http;
using BrainThud.Web.Data;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
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
            this.QuizResultHandler = new Mock<IQuizResultHandler>();
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.PARTITION_KEY);
            var quizResultsController = new QuizResultsControllerFake(
                this.UnitOfWork.Object, 
                this.QuizResultHandler.Object,
                authenticationHelper.Object);

            this.QuizResultsController = new ApiControllerBuilder<QuizResultsControllerFake>(quizResultsController)
               .CreateRequest(HttpMethod.Post, TestUrls.QUIZ_RESULTS)
               .Build();
        }

        protected Mock<IQuizResultHandler> QuizResultHandler { get; private set; }
        protected Mock<IUnitOfWork> UnitOfWork { get; private set; }
        protected QuizResultsControllerFake QuizResultsController { get; private set; }
    }
}