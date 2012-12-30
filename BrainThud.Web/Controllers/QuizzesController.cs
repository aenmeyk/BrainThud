﻿using System;
using System.Linq;
using System.Web.Http;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Dtos;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Controllers
{
    [Authorize]
    public class QuizzesController : ApiControllerBase
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly IAuthenticationHelper authenticationHelper;

        public QuizzesController(ITableStorageContextFactory tableStorageContextFactory, IAuthenticationHelper authenticationHelper)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.authenticationHelper = authenticationHelper;
        }

        public Quiz Get(int year, int month, int day)
        {
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
            var routeValues = new { year, month, day };
            var userConfiguration = tableStorageContext.UserConfigurations.GetByNameIdentifier();

            var quizDate = new DateTime(year, month, day)
                .AddDays(1).Date
                .AddMilliseconds(-1);

            var quizResults = tableStorageContext.QuizResults.GetForQuiz(year, month, day).ToList();
            var userCards = tableStorageContext.Cards.GetForUser().Where(x => x.QuizDate <= quizDate).ToList().Select(x => x.EntityId);
            var quizResultCards = tableStorageContext.Cards.GetForQuizResults(quizResults).Select(x => x.EntityId);

            var quiz = new Quiz
            {
                CardIds =  userCards.Union(quizResultCards),
                ResultsUri = this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues),
                UserId = userConfiguration != null ? userConfiguration.UserId : 0,
                QuizDate = quizDate.Date,
                QuizResults = quizResults
            };

            return quiz;
        }
    }
}