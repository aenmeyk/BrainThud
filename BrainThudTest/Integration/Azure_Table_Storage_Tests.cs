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
//        public void Nugget_is_saved_and_retrieved_from_Azure_Table_Storage()
//        {
//            // Given
//            var cloudStorageServices = new CloudStorageServices();
//            var keyGenerator = new TableStorageKeyGenerator();
//            var tableServiceContext = new TableStorageContext<Nugget>(EntitySetNames.NUGGET, cloudStorageServices);
//            TableStorageHelper.ClearTable<Nugget>(tableServiceContext);
//
//            var repositoryFactory = new RepositoryFactory(cloudStorageServices, keyGenerator);
//
//            var createdNugget = new Nugget
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
//            unitOfWork.Nuggets.Add(createdNugget);
//            unitOfWork.Commit();
//            var returnedNugget = unitOfWork.Nuggets.GetAll().Single();
//
//            // Then
//            returnedNugget.ShouldHave().AllProperties().EqualTo(createdNugget);
//        }

        [Test]
        public void Then_NuggetsController_CRUD()
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
            var nugget = new Nugget { Question = POST_QUESTION_TEXT };

            // Test Post
            var postResponse = client.PostAsJsonAsync(TestUrls.NUGGETS, nugget).Result;
            var postNugget = postResponse.Content.ReadAsAsync<Nugget>().Result;

            // Assert that the POST succeeded
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Assert that the posted nugget was returned in the response
            postNugget.Question.Should().Be(POST_QUESTION_TEXT);

            // Assert that the relevant keys were set on the nugget
            postNugget.PartitionKey.Should().NotBeEmpty();
            postNugget.RowKey.Should().NotBeEmpty();

            // Assert that the location of the new nugget was returned in the Location header
            var nuggetUrl = postResponse.Headers.Location;
            nuggetUrl.AbsoluteUri.Should().BeEquivalentTo(string.Format("{0}/{1}", TestUrls.NUGGETS, postNugget.RowKey));


            // Test PUT
            postNugget.Question = PUT_QUESTION_TEXT;
            var putResponse = client.PutAsJsonAsync(TestUrls.NUGGETS, postNugget).Result;

            // Assert that the PUT succeeded
            putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);


            // Test GET
            var getResponse = client.GetAsync(nuggetUrl).Result;
            var getNugget = getResponse.Content.ReadAsAsync<Nugget>().Result;

            // Assert that the correct nugget was returned
            getNugget.RowKey.Should().Be(postNugget.RowKey);

            // Assert that the PUT did actually update the nugget
            getNugget.Question.Should().Be(PUT_QUESTION_TEXT);


            // Test DELETE
            var deleteResponse = client.DeleteAsync(nuggetUrl).Result;

            // Assert that the DELETE succeeded
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Assert that the nugget is no longer in storage
            getResponse = client.GetAsync(nuggetUrl).Result;
            getResponse.IsSuccessStatusCode.Should().Be(false);
        }
    }
}