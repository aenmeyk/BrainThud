using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private Card actualResult;
        private Card expectedResult;

        public override void When()
        {
            this.expectedResult = new Card { PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = TestValues.CARD_ROW_KEY };
            var cards = new HashSet<Card> { new Card { PartitionKey = TestValues.CARD_PARTITION_KEY }, this.expectedResult, new Card() }.AsQueryable();
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards);
            this.actualResult = this.TableStorageRepository.Get(TestValues.NAME_IDENTIFIER, TestValues.CARD_ROW_KEY);
        }

        [Test]
        public void Then_the_card_should_be_returned_from_the_StorageContext()
        {
            this.actualResult.Should().Be(this.expectedResult);
        }
    }
}