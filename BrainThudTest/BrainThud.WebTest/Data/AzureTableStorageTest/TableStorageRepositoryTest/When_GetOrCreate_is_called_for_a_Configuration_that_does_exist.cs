using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_GetOrCreate_is_called_for_a_Configuration_that_does_exist : Given_a_new_TableStorageRepository_of_Configuration
    {
        private Configuration actualResult;
        private Configuration expectedResult;

        public override void When()
        {
            this.expectedResult = new Configuration { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY };
            var cards = new HashSet<Configuration> { this.expectedResult, new Configuration() }.AsQueryable();
            this.TableStorageContext.Setup(x => x.CreateQuery<Configuration>()).Returns(cards);
            this.actualResult = this.TableStorageRepository.GetOrCreate(TestValues.PARTITION_KEY, TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_Configuration_should_be_returned_from_the_StorageContext()
        {
            this.actualResult.Should().Be(this.expectedResult);
        }

        [Test]
        public void Then_the_new_Configuration_should_not_be_added_to_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.actualResult), Times.Never());
        }
    }
}