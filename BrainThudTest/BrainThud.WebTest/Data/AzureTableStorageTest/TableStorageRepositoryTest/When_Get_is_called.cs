using System.Collections.Generic;
using BrainThud.Web.Data;
using BrainThud.Web.Model;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private Card actualResult;
        private Card expectedResult;

        public override void When()
        {
            this.expectedResult = new Card { PartitionKey = TestValues.PARTITION_KEY, RowKey = TestValues.ROW_KEY };
            var cards = new HashSet<Card> { this.expectedResult, new Card() }.AsQueryable();
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