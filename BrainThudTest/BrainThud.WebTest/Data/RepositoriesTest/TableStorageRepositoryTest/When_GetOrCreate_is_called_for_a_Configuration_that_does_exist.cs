using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_GetOrCreate_is_called_for_a_Configuration_that_does_exist : Given_a_new_TableStorageRepository_of_Configuration
    {
        private UserConfiguration actualResult;
        private UserConfiguration expectedResult;

        public override void When()
        {
            this.expectedResult = new UserConfiguration { PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = TestValues.CARD_ROW_KEY };
            var cards = new HashSet<UserConfiguration> { this.expectedResult, new UserConfiguration() }.AsQueryable();
            this.TableStorageContext.Setup(x => x.CreateQuery<UserConfiguration>()).Returns(cards);
            this.actualResult = this.TableStorageRepository.GetOrCreate(TestValues.NAME_IDENTIFIER, TestValues.CARD_ROW_KEY);
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