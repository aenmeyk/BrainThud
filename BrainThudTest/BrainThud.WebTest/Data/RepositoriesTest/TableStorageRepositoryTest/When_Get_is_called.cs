using System.Collections.Generic;
using System.Linq;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private Card actualResult;
        private Card expectedResult;

        public override void When()
        {
            this.expectedResult = new Card { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY };
            var cards = new HashSet<Card> { new Card { PartitionKey = TestValues.PARTITION_KEY }, this.expectedResult, new Card() }.AsQueryable();
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards);
            this.actualResult = this.TableStorageRepository.Get(TestValues.PARTITION_KEY, TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_card_should_be_returned_from_the_StorageContext()
        {
            this.actualResult.Should().Be(this.expectedResult);
        }
    }
}