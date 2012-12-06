﻿using System.Net.Http;
using BrainThud.Web;
using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
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
            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var tableStorageContextFactory = new Mock<ITableStorageContextFactory> { DefaultValue = DefaultValue.Mock };
            tableStorageContextFactory.Setup(x => x.CreateTableStorageContext(EntitySetNames.CARD)).Returns(this.TableStorageContext.Object);

            this.QuizResultHandler = new Mock<IQuizResultHandler>();
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.Setup(x => x.NameIdentifier).Returns(TestValues.PARTITION_KEY);
            var quizResultsController = new QuizResultsControllerFake(
                tableStorageContextFactory.Object, 
                this.QuizResultHandler.Object,
                authenticationHelper.Object);

            this.QuizResultsController = new ApiControllerBuilder<QuizResultsControllerFake>(quizResultsController)
               .CreateRequest(HttpMethod.Post, TestUrls.QUIZ_RESULTS)
               .Build();
        }

        protected Mock<IQuizResultHandler> QuizResultHandler { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected QuizResultsControllerFake QuizResultsController { get; private set; }
    }
}