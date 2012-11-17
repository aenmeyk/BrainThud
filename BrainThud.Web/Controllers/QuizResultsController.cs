﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Data;
using BrainThud.Model;

namespace BrainThud.Web.Controllers
{
    public class QuizResultsController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public QuizResultsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public HttpResponseMessage Post(int year, int month, int day, QuizResult quizResult)
        {
            quizResult.QuizDate = new DateTime(year, month, day);
            this.unitOfWork.QuizResults.Add(quizResult);
            this.unitOfWork.Commit();
            var response = this.Request.CreateResponse(HttpStatusCode.Created, quizResult);

            var routeValues = new
            {
                year,
                month,
                day,
                id = quizResult.RowKey
            };

            response.Headers.Location = new Uri(this.GetLink(RouteNames.API_QUIZ_RESULTS, routeValues));
            return response;
        }
    }
}