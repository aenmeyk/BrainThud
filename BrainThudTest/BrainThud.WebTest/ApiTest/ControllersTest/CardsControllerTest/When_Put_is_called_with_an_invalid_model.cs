﻿using System.Net;
using System.Web.Http;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Put_is_called_with_an_invalid_model : Given_a_new_CardsController
    {
        public override void When()
        {
            this.CardsController.ModelState.AddModelError("Error Key", "Error Message");
            this.CardsController.Put(new Card());
        }

        [Test]
        public void Then_the_returned_status_code_should_be_400()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest));
        }
    }
}