﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using BrainThud.Core.Models;
using BrainThud.Web.Api;
using BrainThud.Web.DependencyResolution;
using FluentAssertions;
using NUnit.Framework;
using Newtonsoft.Json;
using HttpHeaders = BrainThud.Core.HttpHeaders;

namespace BrainThudTest.Integration
{
    public class XmlMediaTypeFormatterWrapper : XmlMediaTypeFormatter
    {
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var ret = base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            if(null != ret.Exception)
            {
                var x = 1;
            }
            return ret;
        }
    }

    public class JsonMediaTypeFormatterWrapper : JsonMediaTypeFormatter
    {
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var ret = base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            if(null != ret.Exception)
            {
                var x = 1;
            }
            return ret;
        }
    }

    [TestFixture]
    public class Azure_Table_Storage_Tests
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new AzureInitializer().Initialize();
        }

        [Test]
        [Category(TestTypes.INTEGRATION)]
        public void CardsController_CRUD()
        {
            const string POST_QUESTION_TEXT = "Created From Unit Test";
            const string PUT_QUESTION_TEXT = "Updated From Unit Test";

            var container = IoC.Initialize();
            var config = new HttpConfiguration { DependencyResolver = new StructureMapWebApiResolver(container),  };
            config.Formatters.Add(new XmlMediaTypeFormatterWrapper());
            config.Formatters.Add(new JsonMediaTypeFormatterWrapper());
//            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", "application/json"));
//            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("xml", "application/xml"));

            WebApiConfig.Configure(config);

            var server = new HttpServer(config);
            var client = new HttpClient(server);
            client.DefaultRequestHeaders.Add(HttpHeaders.X_CLIENT_DATE, DateTime.Now.ToLongDateString());
            client.DefaultRequestHeaders.Add(HttpHeaders.X_TEST, "true");

            var card = new Card { Question = POST_QUESTION_TEXT, QuizDate = DateTime.Now };

            // Test POST
            // -------------------------------------------------------------------------------------
            var postResponse = client.PostAsJsonAsync(TestUrls.CARDS, card).Result;
            var xx = postResponse.Content.ReadAsStringAsync().Result;
            var postCard = postResponse.Content.ReadAsAsync<Card>().Result;

            // Assert that the POST succeeded
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Assert that the posted Card was returned in the response
            postCard.Question.Should().Be(POST_QUESTION_TEXT);

            // Assert that the relevant keys were set on the Card
            postCard.PartitionKey.Should().NotBeEmpty();
            postCard.RowKey.Should().NotBeEmpty();

            // Assert that the location of the new Card was returned in the Location header
            var cardUrl = postResponse.Headers.Location;
            cardUrl.AbsoluteUri.Should().BeEquivalentTo(string.Format("{0}/{1}/{2}", TestUrls.CARDS, postCard.UserId, postCard.EntityId));


            // Test PUT
            // -------------------------------------------------------------------------------------
            postCard.Question = PUT_QUESTION_TEXT;
            var putResponse = client.PutAsJsonAsync(TestUrls.CARDS, postCard).Result;

            // Assert that the PUT succeeded
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);


            // Test GET
            // -------------------------------------------------------------------------------------
            var getResponse = client.GetAsync(cardUrl).Result;
            var getCard = getResponse.Content.ReadAsAsync<Card>().Result;

            // Assert that the correct Card was returned
            getCard.RowKey.Should().Be(postCard.RowKey);

            // Assert that the PUT did actually update the Card
            getCard.Question.Should().Be(PUT_QUESTION_TEXT);


            // Test DELETE
            // -------------------------------------------------------------------------------------
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, cardUrl);
            var cardJson = JsonConvert.SerializeObject(new[] { getCard });
            requestMessage.Content = new StringContent(cardJson);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            var deleteResponse = client.SendAsync(requestMessage).Result;

            // Assert that the DELETE succeeded
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Assert that the Card is no longer in storage
            getResponse = client.GetAsync(cardUrl).Result;
            getResponse.IsSuccessStatusCode.Should().Be(false);
        }

        [Test]
        [Category(TestTypes.INTEGRATION)]
        public void QuizResultsController_CRUD()
        {
            var quizDate = new DateTime(2012, 6, 30);

            var container = IoC.Initialize();
            var config = new HttpConfiguration { DependencyResolver = new StructureMapWebApiResolver(container) };
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("xml", "application/xml"));

            WebApiConfig.Configure(config);

            var server = new HttpServer(config);
            var client = new HttpClient(server);
            client.DefaultRequestHeaders.Add(HttpHeaders.X_CLIENT_DATE, DateTime.Now.ToLongDateString());
            client.DefaultRequestHeaders.Add(HttpHeaders.X_TEST, "true");

            // Create the card
            var card = new Card { Question = "Created from QuizResultsController_CRUD test.", QuizDate = DateTime.Now };
            var postCardResponse = client.PostAsJsonAsync(TestUrls.CARDS, card).Result;
            var postCard = postCardResponse.Content.ReadAsAsync<Card>().Result;
            var cardUrl = postCardResponse.Headers.Location;

            // Create the QuizResult
            var quizResult = new QuizResult { IsCorrect = true };


            // Test POST
            // -------------------------------------------------------------------------------------
            var testUrl = string.Format(TestUrls.QUIZ_RESULTS, postCard.UserId, quizDate.Year, quizDate.Month, quizDate.Day, postCard.EntityId);
            var postResponse = client.PostAsJsonAsync(testUrl, quizResult).Result;
            var postQuizResult = postResponse.Content.ReadAsAsync<QuizResult>().Result;

            // Assert that the POST succeeded
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Assert that the posted QuizResult was returned in the response
            postQuizResult.QuizDate.Should().Be(quizDate);

            // Assert that the relevant fields were set on the QuizResult
            postQuizResult.PartitionKey.Should().NotBeEmpty();
            postQuizResult.RowKey.Should().NotBeEmpty();
            postQuizResult.QuizDate.Should().Be(quizDate);
            postQuizResult.CardId.Should().Be(postCard.EntityId);

            // Assert that the location of the new QuizResult was returned in the Location header
            var quizResultUrl = postResponse.Headers.Location;
            quizResultUrl.AbsoluteUri.Should().BeEquivalentTo(testUrl);


            // Test DELETE
            // -------------------------------------------------------------------------------------
            var deleteResponse = client.DeleteAsync(quizResultUrl).Result;

            // Assert that the DELETE succeeded
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Assert that the Card is no longer in storage
            var getResponse = client.GetAsync(quizResultUrl).Result;
            getResponse.IsSuccessStatusCode.Should().Be(false);

            // Delete the card
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, cardUrl);
            var cardJson = JsonConvert.SerializeObject(card);
            requestMessage.Content = new StringContent(cardJson);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            client.SendAsync(requestMessage);

//            client.DeleteAsync(cardUrl);
        }
    }
}