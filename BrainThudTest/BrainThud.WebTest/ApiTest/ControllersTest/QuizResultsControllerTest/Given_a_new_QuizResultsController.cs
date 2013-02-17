using System.Net.Http;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Handlers;
using BrainThudTest.BrainThud.WebTest.Fakes;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultsController : Gwt
    {
        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };

            var tableStorageContextFactory = new TableStorageContextFactoryMockBuilder()
                .SetTableStorageContext(this.TableStorageContext)
                .Build();

            this.QuizResultHandler = new Mock<IQuizResultHandler>();
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);
            this.Request = new HttpRequestMessage(HttpMethod.Post, TestUrls.QUIZ_RESULTS);

            var quizResultsController = new QuizResultsControllerFake(
                tableStorageContextFactory.Object, 
                this.QuizResultHandler.Object,
                authenticationHelper.Object);

            this.QuizResultsController = new ApiControllerBuilder<QuizResultsControllerFake>(quizResultsController)
               .CreateRequest(this.Request)
               .Build();
        }

        protected HttpRequestMessage Request { get; private set; }
        protected Mock<IQuizResultHandler> QuizResultHandler { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected QuizResultsControllerFake QuizResultsController { get; private set; }
    }
}