﻿using System.Net;
using System.Web.Http;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_a_Put_is_made_and_the_related_card_does_not_exist : Given_a_new_QuizResultsController
    {
        private QuizResult quizResult;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetById(TestValues.USER_ID, TestValues.CARD_ID)).Returns((Card)null);
            this.quizResult = new QuizResult();
            this.QuizResultsController.Put(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID, 
                this.quizResult);
        }
        
        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_with_a_422_status_code()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be((HttpStatusCode)422));
        }
    }
}