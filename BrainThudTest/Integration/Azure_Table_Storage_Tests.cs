using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Tools;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace BrainThudTest.Integration
{
    [TestFixture]
    [Category(TestTypes.INTEGRATION)]
    public class Azure_Table_Storage_Tests
    {
        [Test]
        public void Nugget_is_saved_to_Azure_Table_Storage()
        {
            // Given
            var cloudStorageServices = new CloudStorageServices();
            var tableServiceContext = new TableStorageContext<Nugget>(EntitySetNames.NUGGET, cloudStorageServices);
            TableStorageHelper.ClearTable<Nugget>(tableServiceContext);

            var repositoryFactory = new RepositoryFactory(cloudStorageServices);

            var createdNugget = new Nugget
            {
                PartitionKey = TestValues.PARTITION_KEY,
                RowKey = TestValues.ROW_KEY,
                Question = "QuestionText",
                Answer = "AnswerText",
                AdditionalInformation = "AdditionalInformationText"
            };

            // When
            var unitOfWork = new UnitOfWork(repositoryFactory);
            unitOfWork.Nuggets.Add(createdNugget);
            unitOfWork.Commit();
            var returnedNugget = TableStorageHelper.GetAll<Nugget>(tableServiceContext).Single();

            // Then
            returnedNugget.ShouldHave().AllProperties().EqualTo(createdNugget);
        }
    }
}