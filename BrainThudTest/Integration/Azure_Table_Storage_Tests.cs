using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThud.Web.App_Start;
using BrainThud.Web.DependencyResolution;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.Integration
{
    [TestFixture]
    [Category(TestTypes.INTEGRATION)]
    public class Azure_Table_Storage_Tests
    {
//        [Test]
//        public void Card_is_saved_and_retrieved_from_Azure_Table_Storage()
//        {
//            // Given
//            var cloudStorageServices = new CloudStorageServices();
//            var keyGenerator = new TableStorageKeyGenerator();
//            var tableServiceContext = new TableStorageContext<Card>(EntitySetNames.CARD, cloudStorageServices);
//            TableStorageHelper.ClearTable<Card>(tableServiceContext);
//
//            var repositoryFactory = new RepositoryFactory(cloudStorageServices, keyGenerator);
//
//            var createdCard = new Card
//            {
//                PartitionKey = TestValues.PARTITION_KEY,
//                RowKey = TestValues.ROW_KEY,
//                Question = "QuestionText",
//                Answer = "AnswerText",
//                AdditionalInformation = "AdditionalInformationText"
//            };
//
//            // When
//            var unitOfWork = new UnitOfWork(repositoryFactory);
//            unitOfWork.Cards.Add(createdCard);
//            unitOfWork.Commit();
//            var returnedCard = unitOfWork.Cards.GetAll().Single();
//
//            // Then
//            returnedCard.ShouldHave().AllProperties().EqualTo(createdCard);
//        }

        [Test]
        public void Then_CardsController_CRUD()
        {
            const string POST_QUESTION_TEXT = "Created From Unit Test";
            const string PUT_QUESTION_TEXT = "Updated From Unit Test";
            
            var container = IoC.Initialize();
            var config = new HttpConfiguration { DependencyResolver = new StructureMapWebApiResolver(container) };
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("xml", "application/xml"));

            WebApiConfig.Configure(config);

            var server = new HttpServer(config);
            var client = new HttpClient(server);
            var card = new Card { Question = POST_QUESTION_TEXT };

            // Test Post
            var postResponse = client.PostAsJsonAsync(TestUrls.CARDS, card).Result;
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
            cardUrl.AbsoluteUri.Should().BeEquivalentTo(string.Format("{0}/{1}", TestUrls.CARDS, postCard.RowKey));


            // Test PUT
            postCard.Question = PUT_QUESTION_TEXT;
            var putResponse = client.PutAsJsonAsync(TestUrls.CARDS, postCard).Result;

            // Assert that the PUT succeeded
            putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);


            // Test GET
            var getResponse = client.GetAsync(cardUrl).Result;
            var getCard = getResponse.Content.ReadAsAsync<Card>().Result;

            // Assert that the correct Card was returned
            getCard.RowKey.Should().Be(postCard.RowKey);

            // Assert that the PUT did actually update the Card
            getCard.Question.Should().Be(PUT_QUESTION_TEXT);


            // Test DELETE
            var deleteResponse = client.DeleteAsync(cardUrl).Result;

            // Assert that the DELETE succeeded
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Assert that the Card is no longer in storage
            getResponse = client.GetAsync(cardUrl).Result;
            getResponse.IsSuccessStatusCode.Should().Be(false);
        }
    }
}