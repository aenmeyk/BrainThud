﻿using System;
using System.Linq;
using System.Web.Http;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Dtos;

namespace BrainThud.Web.Controllers
{
    [Authorize]
    public class QuizzesController : ApiControllerBase
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;

        public QuizzesController(ITableStorageContextFactory tableStorageContextFactory)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
        }

        public Quiz Get(int year, int month, int day)
        {
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);

            var quizDate = new DateTime(year, month, day)
                .AddDays(1).Date
                .AddMilliseconds(-1);

            var routeValues = new { year, month, day };

            var quiz = new Quiz
            {
                Cards = tableStorageContext.Cards.GetAll().Where(x => x.QuizDate <= quizDate),
                ResultsUri = this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues)
            };

            return quiz;
        }
    }
}