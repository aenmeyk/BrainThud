﻿using BrainThud.Web.Controllers;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Handlers;
using BrainThud.Web.Helpers;
using BrainThudTest.Tools;

namespace BrainThudTest.BrainThud.WebTest.Fakes
{
    public class QuizResultsControllerFake : QuizResultsController
    {
        public QuizResultsControllerFake( 
            ITableStorageContextFactory tableStorageContextFactory, 
            IQuizResultHandler quizResultHandler,
            IAuthenticationHelper authenticationHelper,
            IKeyGeneratorFactory keyGeneratorFactory)
            : base(tableStorageContextFactory, quizResultHandler, authenticationHelper, keyGeneratorFactory) { }

        public string RouteName { get; set; }
        public object RouteValues { get; set; }

        public override string GetLink(string routeName, object routeValues)
        {
            this.RouteValues = routeValues;
            this.RouteName = routeName;
            return TestUrls.LOCALHOST;
        }
    }
}